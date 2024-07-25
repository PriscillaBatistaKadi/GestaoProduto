using GestaoProduto.Application.DTOs;
using GestaoProduto.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProduto.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        void Inserir(Produto produto);
        (IList<Produto>, int) ObterTodos(int pagina, int tamanhoPagina);

        Produto ObterPorCodigo(int codigo);

        void Atualizar(Produto produto, ProdutoDto produtoDto);

        void Excluir(Produto produto);
    }
}