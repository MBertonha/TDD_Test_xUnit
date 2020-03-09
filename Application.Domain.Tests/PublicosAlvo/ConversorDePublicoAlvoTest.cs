using Application.Domain._Base;
using Application.Domain.PublicosAlvo;
using Application.Domain.Tests._Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Application.Domain.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Universitário, "Universitário")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvoEmString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);

            Assert.Equals(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoEhInvalido()
        {
            const string publicoAlvoInvalido = "Invalido";

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    _conversor.Converter(publicoAlvoInvalido))
                .ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }
}