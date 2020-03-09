using System;
using Application.Domain.PublicosAlvo;
using Application.Domain.Tests._Builders;
using Application.Domain.Tests._Util;
using Bogus;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Xunit.Abstractions;

namespace Application.Domain.Tests.Cursos
{
    public class CursoTeste : IDisposable {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTeste (ITestOutputHelper output) {
            _output = output;
            _output.WriteLine ("Construtor Executado");

            var faker = new Faker ();

            _nome = faker.Random.Words ();
            _cargaHoraria = faker.Random.Double (50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double (100, 1000);;
            _descricao = faker.Lorem.Paragraph ();
        }

        public void Dispose () {
            _output.WriteLine ("Dispose Executado");
        }

        [Theory]
        [InlineData ("")]
        [InlineData (null)]
        public void NaoDeveTerUmNomeInvalido (string nomeInvalido) {

            Assert.ThrowsException<ArgumentException> (() =>
                CursoBuilder.Novo ().ComNome (nomeInvalido).Build ()
            ).ComMensagem ("Nome Invalido");
        }

        [Theory]
        [InlineData (0)]
        [InlineData (-2)]
        [InlineData (-100)]
        public void NaoDeveTerCargaHorariaMenorQue1 (double cargaHoraria) {

            Assert.ThrowsException<ArgumentException> (() =>
                CursoBuilder.Novo ().ComCargaHoraria (cargaHoraria).Build ()
            ).ComMensagem ("Carga horaria invalida");
        }

        [Theory]
        [InlineData (0)]
        [InlineData (-2)]
        [InlineData (-100)]
        public void CursoNaoDeveTerValorMenorQue1 (double valorInvalido) {

            Assert.ThrowsException<ArgumentException> (() =>
                CursoBuilder.Novo ().ComValor (valorInvalido).Build ()
            ).ComMensagem ("Valor do curso invalido");
        }
    }
}
