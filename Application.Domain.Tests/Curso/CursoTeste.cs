using System;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using Application.Domain.Tests._Util;
using Xunit.Abstractions;
using Application.Domain.Enum;
using Application.Domain.Tests._Builders;

namespace Application.Domain.Tests
{
    public class CursoTeste : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor Executado");


        }

        public void Dispose()
        {
            _output.WriteLine("Dispose Executado");
        }

        //[Fact]
        //public void DeveCriarCurso()
        //{
        //    var cursoEsperado = new
        //    {
        //        Nome = _nome,
        //        CargaHoraria = _cargaHoraria,
        //        PublicoAlvo = _publicoAlvo,
        //        Valor = _valor,
        //        Descricao = _descricao
        //    };

        //    var curso = CursoBuilder.Novo().ComNome().Build();

        //    //Curso esperado deve corresponder ao esperado
        //    cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        //}

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

            Assert.ThrowsException<ArgumentException>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem("Nome Invalido");
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

            Assert.ThrowsException<ArgumentException>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHoraria).Build()).ComMensagem("Carga horária inválida");
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

            Assert.ThrowsException<ArgumentException>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build()).ComMensagem("Valor do curso inválido");
        }
    }
}
