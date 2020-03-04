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
                PublicoAlvo = "Estudantes",
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
    }

    public class Curso
    {
        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public string PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
