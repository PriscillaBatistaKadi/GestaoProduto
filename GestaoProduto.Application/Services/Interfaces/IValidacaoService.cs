using System;


namespace GestaoProduto.Application.Services.Interfaces
{
    public interface IValidacaoService
    {
        string Validar(DateTime dataFabricacao, DateTime dataValidade);
    }
}
