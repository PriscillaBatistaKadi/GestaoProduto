using GestaoProduto.Application.Services.Interfaces;
using System;


namespace GestaoProduto.Application.Services
{
    public class ValidacaoService : IValidacaoService
    {
        public string Validar(DateTime dataFabricacao, DateTime dataValidade)
        {
            if (dataFabricacao >= dataValidade)
            {
                return ("A Data de Fabricação deve ser anterior à Data de Validade");
            }

            return null;
        }
    }
}
