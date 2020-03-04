using System;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using Application.Domain.Tests._Util;
using Xunit.Abstractions;
using Application.Domain.Enum;

namespace Application.Domain.Tests
{
    public class CursoTeste : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor Executado");

            _nome = "informatica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 90;
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose Executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor);

            //Curso esperado deve corresponder ao esperado
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            Assert.ThrowsException<ArgumentException>(() =>
                    new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome Invalido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveTerCargaHorariaMenorQue1(double cargaHoraria)
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            Assert.ThrowsException<ArgumentException>(() =>
                new Curso(_nome, cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Carga hor�ria inv�lida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            Assert.ThrowsException<ArgumentException>(() =>
                new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido)).ComMensagem("Valor do curso inv�lido");
        }
    }
}
