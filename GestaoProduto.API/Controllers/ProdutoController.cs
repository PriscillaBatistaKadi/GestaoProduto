using AutoMapper;
using GestaoProduto.Application.DTOs;
using GestaoProduto.Application.Models;
using GestaoProduto.Application.Services.Interfaces;
using GestaoProduto.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GestaoProduto.API.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly IValidacaoService _validacaoService;

        public ProdutoController(IMapper mapper, IProdutoService produtoService, IValidacaoService validacaoService)
        {
            _mapper = mapper;
            _produtoService = produtoService;
            _validacaoService = validacaoService;
        }

        /// <summary>
        /// Listar todos por paginação
        /// </summary>
        /// <param name="pagina">Página</param>
        /// <param name="tamanhoPagina">Total de produtos por página</param>
        /// <response code="200">Success</response>
        /// <response code="204">NoContent</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginacaoViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 3)
        {
            var (produtos, totalItens) = _produtoService.ObterTodos(pagina, tamanhoPagina);

            if (produtos == null || !produtos.Any()) return NoContent();

            var produtosViewModel = _mapper.Map<List<ProdutoViewModel>>(produtos);

            var response = new PaginacaoViewModel(pagina, tamanhoPagina, totalItens, produtosViewModel);

            return Ok(response);
        }

        /// <summary>
        /// Recuperar por código
        /// </summary>
        /// <param name="codigo">Código do produto</param>
        /// <response code="200">Success</response>
        /// <response code="204">NoContent</response>
        [HttpGet("{codigo:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetByCodigo(int codigo)
        {
            var produto = _produtoService.ObterPorCodigo(codigo);

            if (produto is null) return NoContent();

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoViewModel);
        }

        /// <summary>
        /// Inserir
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="400">BadResquest</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            var validacao = _validacaoService.Validar(produto.DataFabricacao, produto.DataValidade);

            if (validacao is not null)
            {
                return BadRequest(validacao);
            }

            _produtoService.Inserir(produto);

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return CreatedAtAction(nameof(GetByCodigo), new { codigo = produto.Codigo }, produtoViewModel);
        }

        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="codigo">Código do produto</param>
        /// <response code="200">Success</response>
        /// <response code="400">BadRequest</response>
        [HttpPut("{codigo:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int codigo, ProdutoDto produtoDto)
        {
            var produto = _produtoService.ObterPorCodigo(codigo);

            if (produto is null) return NoContent();

            var validacao = _validacaoService.Validar(produto.DataFabricacao, produto.DataValidade);

            if (validacao is not null)
            {
                return BadRequest(validacao);
            }

            _produtoService.Atualizar(produto, produtoDto);

            return Ok();
        }

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="codigo">Código do produto</param>
        /// <response code="200">Success</response>
        /// <response code="204">NoContent</response>
        [HttpDelete("{codigo:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int codigo)
        {
            var produto = _produtoService.ObterPorCodigo(codigo);

            if (produto is null) return NoContent();

            _produtoService.Excluir(produto);

            return Ok();
        }
    }
}