using System;
using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ProdutoDto
    {
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public FornecedorDto Fornecedor { get; set; }
    }
}