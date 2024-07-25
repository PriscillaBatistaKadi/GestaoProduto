using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class Fornecedor
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
        public int CodigoCliente { get; set; }
    }
}