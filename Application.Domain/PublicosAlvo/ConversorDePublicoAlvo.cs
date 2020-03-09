using Application.Domain._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public PublicoAlvo Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!System.Enum.TryParse<PublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            return publicoAlvoConvertido;
        }
    }
}
