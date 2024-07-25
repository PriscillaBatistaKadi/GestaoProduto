using System;
using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class ProdutoViewModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
    }
}