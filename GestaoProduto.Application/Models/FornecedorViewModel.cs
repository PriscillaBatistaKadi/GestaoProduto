using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class FornecedorViewModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
    }
}