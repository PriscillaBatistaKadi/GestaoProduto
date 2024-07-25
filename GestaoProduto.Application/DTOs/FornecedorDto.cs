using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class FornecedorDto
    {
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
    }
}