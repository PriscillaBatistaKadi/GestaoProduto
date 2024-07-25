using System;
using GestaoProduto.Application.Services;
using GestaoProduto.Application.Services.Interfaces;
using Xunit;

namespace GestaoProduto.Testes
{
    public class ValidacaoServiceTeste
    {
        private readonly IValidacaoService _validacaoService;
        
        public ValidacaoServiceTeste()
        {
            _validacaoService = new ValidacaoService();
        }
        
        [Fact]
        public void Validar_DataFabricacaoIgualDataValidade_RetornoMensagemErro()
        {
            var dataFabricacao = DateTime.Now.Date;
            var dataValidade = DateTime.Now.Date;

            var result = _validacaoService.Validar(dataFabricacao, dataValidade);
            
            Assert.NotNull(result);
        }
        
        [Fact]
        public void Validar_DataFabricacaoMenorQueDataValidade_RetornoNull()
        {
            var dataFabricacao = DateTime.Now.Date;
            var dataValidade = DateTime.Now.Date.AddDays(5);

            var result = _validacaoService.Validar(dataFabricacao, dataValidade);
            
            Assert.Null(result);
        }
    }
}