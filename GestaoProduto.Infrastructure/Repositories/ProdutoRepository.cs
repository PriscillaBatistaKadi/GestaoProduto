using GestaoProduto.Core.Entities;
using GestaoProduto.Core.Enums;
using GestaoProduto.Infrastructure.Context;
using GestaoProduto.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GestaoProduto.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext dbContext;

        public ProdutoRepository(ProdutoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Inserir(Produto produto)
        {
            dbContext.Produtos.Add(produto);
            dbContext.SaveChanges();
        }

        public (IList<Produto>, int) ObterTodos(int pagina, int tamanhoPagina)
        {
            var produtos = dbContext.Produtos
                .Include(x => x.Fornecedor)
                .Where(x => x.Situacao == Situacao.Ativo);

            var totalProdutos = produtos.Count();
            var produtosPaginados = produtos
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return (produtosPaginados, totalProdutos);
        }

        public Produto ObterPorCodigo(int codigo)
        {
            return dbContext.Produtos
                .Include(x => x.Fornecedor)
                .SingleOrDefault(x => x.Codigo == codigo && x.Situacao == Situacao.Ativo);
        }

        public void Atualizar(Produto produto)
        {
            dbContext.Produtos.Update(produto);
            dbContext.SaveChanges();
        }
    }
}