using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMapper;
using GestaoProduto.API.Controllers;
using GestaoProduto.Application.DTOs;
using GestaoProduto.Application.Models;
using GestaoProduto.Application.Services.Interfaces;
using GestaoProduto.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GestaoProduto.Testes
{
    public class ProdutoControllerTeste
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IProdutoService> _produtoService;
        private readonly Mock<IValidacaoService> _validacaoService;
        private readonly ProdutoController _controller;
        
        public ProdutoControllerTeste()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _produtoService = new Mock<IProdutoService>();
            _validacaoService = new Mock<IValidacaoService>();
            _controller = new ProdutoController(_mapper.Object, _produtoService.Object, _validacaoService.Object);
        }
        
        [Fact]
        public void GetAll_Ok()
        {
            // Arrange
            var pagina = 1;
            var tamanhoPagina = 3;
            var produtos = _fixture.CreateMany<Produto>(tamanhoPagina).ToList();
            _produtoService.Setup(produto => produto.ObterTodos(pagina, tamanhoPagina))
                .Returns((produtos, 6));
            
            // Act
            var response = _controller.GetAll(pagina, tamanhoPagina);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }
        
        [Fact]
        public void GetAll_NoContent()
        {
            // Arrange
            var pagina = 1;
            var tamanhoPagina = 3;
            
            _produtoService.Setup(produto => produto.ObterTodos(pagina, tamanhoPagina));
            
            // Act
            var response = _controller.GetAll(pagina, tamanhoPagina);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }
        
        [Fact]
        public void GetByCodigo_Ok()
        {
            // Arrange
            var codigo = 1;
            var produto = _fixture.Create<Produto>();
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo))
                .Returns(produto);
            
            // Act
            var response = _controller.GetByCodigo(codigo);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }
        
        [Fact]
        public void GetByCodigo_NoContent()
        {
            // Arrange
            var codigo = 1;
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo));
            
            // Act
            var response = _controller.GetByCodigo(codigo);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }
        
        [Fact]
        public void Post_Ok()
        {
            // Arrange
            var produtoDto = new ProdutoDto()
            {
                Descricao = "ProdutoTeste",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(5)
            };

            var produto = _fixture.Create<Produto>();
            
            _mapper.Setup(map => map.Map<Produto>(produtoDto))
                .Returns(produto);
           
            _validacaoService.Setup(validacao => validacao.Validar(produtoDto.DataFabricacao, produtoDto.DataValidade));
            
            // Act
            var response = _controller.Post(produtoDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
            _produtoService.Verify(service => service.Inserir(produto), Times.Once);
        }
        
        [Fact]
        public void Post_BadRequest()
        {
            // Arrange
            var produto = _fixture.Create<Produto>();
            
            _mapper.Setup(map => map.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(produto);
           
            _validacaoService.Setup(validacao => validacao.Validar(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns("A Data de Fabricação deve ser anterior à Data de Validade");
            
            // Act
            var response = _controller.Post(It.IsAny<ProdutoDto>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            _produtoService.Verify(service => service.Inserir(produto), Times.Never);
        }
        
        [Fact]
        public void Update_Ok()
        {
            // Arrange
            var codigo = 1;
            var produto = _fixture.Create<Produto>();
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo))
                .Returns(produto);
            
            _validacaoService.Setup(validacao => validacao.Validar(It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            
            // Act
            var response = _controller.Update(codigo, It.IsAny<ProdutoDto>());

            // Assert
            Assert.IsType<OkResult>(response);
            _produtoService.Verify(service => service.Atualizar(It.IsAny<Produto>(), It.IsAny<ProdutoDto>()), Times.Once);
        }
        
        [Fact]
        public void Update_NoContent()
        {
            // Arrange
            var codigo = 1;
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo));
            
            // Act
            var response = _controller.Update(codigo, It.IsAny<ProdutoDto>());

            // Assert
            Assert.IsType<NoContentResult>(response);
            _produtoService.Verify(service => service.Atualizar(It.IsAny<Produto>(), It.IsAny<ProdutoDto>()), Times.Never);
        }
        
        [Fact]
        public void Update_BadRequest()
        {
            // Arrange
            var codigo = 1;
            var produto = _fixture.Create<Produto>();
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo))
                .Returns(produto);
            
            _validacaoService.Setup(validacao => validacao.Validar(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns("A Data de Fabricação deve ser anterior à Data de Validade");
            
            // Act
            var response = _controller.Update(codigo, It.IsAny<ProdutoDto>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            _produtoService.Verify(service => service.Atualizar(It.IsAny<Produto>(), It.IsAny<ProdutoDto>()), Times.Never);
        }
        
        [Fact]
        public void Delete_Ok()
        {
            // Arrange
            var codigo = 1;
            var produto = _fixture.Create<Produto>();
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo))
                .Returns(produto);
            
            // Act
            var response = _controller.Delete(codigo);

            // Assert
            Assert.IsType<OkResult>(response);
            _produtoService.Verify(service => service.Excluir(It.IsAny<Produto>()), Times.Once);
        }
        
        [Fact]
        public void Delete_NoContent()
        {
            // Arrange
            var codigo = 1;
            _produtoService.Setup(produto => produto.ObterPorCodigo(codigo));
            
            // Act
            var response = _controller.Delete(codigo);

            // Assert
            Assert.IsType<NoContentResult>(response);
            _produtoService.Verify(service => service.Excluir(It.IsAny<Produto>()), Times.Never);
        }
    }
}