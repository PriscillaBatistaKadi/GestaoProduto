using GestaoProduto.Core.Entities;
using GestaoProduto.Core.Enums;
using GestaoProduto.Infrastructure.Context;
using GestaoProduto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GestaoProduto.Testes
{
    public class ProdutoRepositoryTeste
    {
        private DbContextOptions<ProdutoDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ProdutoDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        private ProdutoDbContext GetContext(DbContextOptions<ProdutoDbContext> options)
        {
            return new ProdutoDbContext(options);
        }

        [Fact]
        public void Inserir_DeveInserirNovoProduto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProdutoDbContext>()
                .UseInMemoryDatabase(databaseName: "Inserir_DeveInserirNovoProduto")
                .Options;

            using (var dbContext = new ProdutoDbContext(options))
            {
                var repository = new ProdutoRepository(dbContext);
                var produto = new Produto { Codigo = 1, Descricao = "Produto Teste", Situacao = Situacao.Ativo };

                // Act
                repository.Inserir(produto);

                // Assert
                var produtoInserido = dbContext.Produtos.FirstOrDefault(p => p.Codigo == 1);
                Assert.NotNull(produtoInserido);
                Assert.Equal("Produto Teste", produtoInserido.Descricao);
            }
        }

        //[Fact]
        //public void ObterTodos_DeveRetornarProdutosAtivos()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<ProdutoDbContext>()
        //        .UseInMemoryDatabase(databaseName: "ObterTodos_DeveRetornarProdutosAtivos")
        //        .Options;

        //    using (var dbContext = new ProdutoDbContext(options))
        //    {
        //        dbContext.Produtos.AddRange(new List<Produto>
        //        {
        //            new Produto { Codigo = 1, Descricao = "Produto 1", Situacao = Situacao.Ativo },
        //            new Produto { Codigo = 2, Descricao = "Produto 2", Situacao = Situacao.Inativo },
        //            new Produto { Codigo = 3, Descricao = "Produto 3", Situacao = Situacao.Ativo }
        //        });
        //        dbContext.SaveChanges();

        //        var repository = new ProdutoRepository(dbContext);

        //        // Act
        //        var produtosAtivos = repository.ObterTodos().ToList();

        //        // Assert
        //        Assert.Equal(2, produtosAtivos.Count); // Deve retornar apenas 2 produtos ativos
        //        Assert.All(produtosAtivos, p => Assert.Equal(Situacao.Ativo, p.Situacao)); // Todos os produtos devem estar ativos
        //    }
        //}

        [Fact]
        public void ObterPorCodigo_DeveRetornarProdutoAtivoPorCodigo()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProdutoDbContext>()
                .UseInMemoryDatabase(databaseName: "ObterPorCodigo_DeveRetornarProdutoAtivoPorCodigo")
                .Options;

            using (var dbContext = new ProdutoDbContext(options))
            {
                dbContext.Produtos.AddRange(new List<Produto>
                {
                    new Produto { Codigo = 1, Descricao = "Produto 1", Situacao = Situacao.Ativo },
                    new Produto { Codigo = 2, Descricao = "Produto 2", Situacao = Situacao.Inativo },
                    new Produto { Codigo = 3, Descricao = "Produto 3", Situacao = Situacao.Ativo }
                });
                dbContext.SaveChanges();

                var repository = new ProdutoRepository(dbContext);

                // Act
                var produto = repository.ObterPorCodigo(1);

                // Assert
                Assert.NotNull(produto);
                Assert.Equal("Produto 1", produto.Descricao);
                Assert.Equal(Situacao.Ativo, produto.Situacao);
            }
        }

        [Fact]
        public void Atualizar_DeveAtualizarProdutoExistente()
        {
            // Arrange
            var options = GetOptions("Atualizar_DeveAtualizarProdutoExistente");

            using (var dbContext = GetContext(options))
            {
                var produtoInicial = new Produto { Codigo = 1, Descricao = "Produto Inicial", Situacao = Situacao.Ativo };
                dbContext.Produtos.Add(produtoInicial);
                dbContext.SaveChanges();

                var repository = new ProdutoRepository(dbContext);
                var produtoAtualizado = new Produto { Codigo = 1, Descricao = "Produto Atualizado", Situacao = Situacao.Inativo };

                // Act
                repository.Atualizar(produtoAtualizado);

                // Assert
                var produto = dbContext.Produtos.Find(1);
                Assert.NotNull(produto);
                Assert.Equal("Produto Atualizado", produto.Descricao);
                Assert.Equal(Situacao.Inativo, produto.Situacao);
            }
        }
    }
}
