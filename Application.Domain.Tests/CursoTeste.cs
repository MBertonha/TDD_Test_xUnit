using System.Xml.Linq;
using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;
using Xunit.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;

namespace Application.Domain.Tests
{
    public class CursoTeste
    {
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)90
            };

            //string nome = "Matheus";
            //double cargaHoraria = 80;
            //string publicoAlvo = "Estudantes";
            //double valor = 90;

            var curso = new Curso(cursoEsperado.Nome, 
                                  cursoEsperado.CargaHoraria,
                                  cursoEsperado.PublicoAlvo, 
                                  cursoEsperado.Valor
                                  );

            //Assert.AreEqual(nome, curso.Nome);
            //Assert.AreEqual(cargaHoraria, curso.CargaHoraria);
            //Assert.AreEqual(publicoAlvo, curso.PublicoAlvo);
            //Assert.AreEqual(valor, curso.Valor);

            //Curso esperado deve corresponder ao esperado
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        //Métodos separados
        //[Fact]
        //public void NaoDeveTerUmNomeVazio()
        //{
        //    var cursoEsperado = new
        //    {
        //        Nome = "Informatica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)90
        //    };

        //    Assert.ThrowsException<ArgumentException>(() => 
        //            new Curso(string.Empty,
        //                            cursoEsperado.CargaHoraria,
        //                            cursoEsperado.PublicoAlvo,
        //                            cursoEsperado.Valor
        //                      ));
        //}
        //[Fact]
        //public void NaoDeveTerUmNomeNulo()
        //{
        //    var cursoEsperado = new
        //    {
        //        Nome = "Informatica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)90
        //    };

        //    Assert.ThrowsException<ArgumentException>(() =>
        //            new Curso(null,
        //                            cursoEsperado.CargaHoraria,
        //                            cursoEsperado.PublicoAlvo,
        //                            cursoEsperado.Valor
        //                      ));
        //}
        
        //Junção dos métodos acima
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)90
            };

            var message = Assert.ThrowsException<ArgumentException>(() =>
                    new Curso(nomeInvalido,
                                    cursoEsperado.CargaHoraria,
                                    cursoEsperado.PublicoAlvo,
                                    cursoEsperado.Valor
                              )).Message;
            Assert.AreEqual("Nome Invalido", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveTerCargaHorariaMenorQue1(double cargaHoraria)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)90
            };

            var message = Assert.ThrowsException<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome,
                                cargaHoraria,
                                cursoEsperado.PublicoAlvo,
                                cursoEsperado.Valor
                          )).Message;
            Assert.AreEqual("Carga horária inválida", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)90
            };

            var message = Assert.ThrowsException<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome,
                                cursoEsperado.CargaHoraria,
                                cursoEsperado.PublicoAlvo,
                                valorInvalido
                          )).Message;
            Assert.AreEqual("Valor do curso inválido", message);
        }
    }

    public enum PublicoAlvo
    {
        Estudante, 
        Universitário,
        Empregado,
        Empreendedor
    }

    public enum NomeCurso
    {
        Informática,
        Manutenção,
        Musica,
        Adm
    }

    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            //Validações
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome Invalido");
            }
            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária inválida");
            }
            if(valor < 1)
            {
                throw new ArgumentException("Valor do curso inválido");
            }

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
