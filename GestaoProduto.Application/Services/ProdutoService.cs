using GestaoProduto.Application.DTOs;
using GestaoProduto.Application.Services.Interfaces;
using GestaoProduto.Core.Entities;
using GestaoProduto.Core.Enums;
using GestaoProduto.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GestaoProduto.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Inserir(Produto produto)
        {
            _produtoRepository.Inserir(produto);
        }

        public (IList<Produto>, int) ObterTodos(int pagina, int tamanhoPagina)
        {
            return _produtoRepository.ObterTodos(pagina, tamanhoPagina);
        }

        public Produto ObterPorCodigo(int codigo)
        {
            return _produtoRepository.ObterPorCodigo(codigo);
        }

        public void Atualizar(Produto produto, ProdutoDto produtoDto)
        {
            produto.Descricao = produtoDto.Descricao;
            produto.DataFabricacao = produto.DataFabricacao;
            produto.DataValidade = produtoDto.DataValidade;
            produto.Fornecedor.Descricao = produtoDto.Fornecedor.Descricao;
            produto.Fornecedor.Cnpj = produtoDto.Fornecedor.Cnpj;

            _produtoRepository.Atualizar(produto);
        }

        public void Excluir(Produto produto)
        {
            produto.Situacao = Situacao.Inativo;
            _produtoRepository.Atualizar(produto);
        }
    }
}