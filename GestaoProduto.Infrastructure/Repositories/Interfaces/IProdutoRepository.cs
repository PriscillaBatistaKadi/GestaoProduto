using GestaoProduto.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GestaoProduto.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        void Inserir(Produto produto);

        (IList<Produto>, int) ObterTodos(int pagina, int tamanhoPagina);

        Produto ObterPorCodigo(int codigo);

        void Atualizar(Produto produto);
    }
}