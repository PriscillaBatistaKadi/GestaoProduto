using GestaoProduto.Core.Enums;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GestaoProduto.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class Produto
    {
        public Produto()
        {
            Situacao = Situacao.Ativo;
        }

        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}