using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProduto.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class PaginacaoViewModel
    {
        public PaginacaoViewModel(int paginaAtual, int tamanhoPagina, int totalItens, List<ProdutoViewModel> produtos)
        {
            PaginaAtual = paginaAtual;
            TamanhoPagina = tamanhoPagina;
            TotalItens = totalItens;
            TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina);
            Produtos = produtos;
        }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }
}
