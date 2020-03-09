﻿using Bogus;
using Bogus.Extensions.Brazil;
using Application.Domain.Alunos;
using Application.Domain.Cursos;
using Application.Domain._Base;
using ExpectedObjects;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Domain.Tests._Builders;
using Application.Domain.Tests._Util;

namespace Application.Domain.Tests.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicosAlvo.PublicoAlvo.Empreendedor
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var novoNomeEsperado = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(novoNomeEsperado);

            Assert.Equals(novoNomeEsperado, aluno.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNomeInvalido(string nomeInvalido)
        {
            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveCriarComEmailInvalido(string emailInvalido)
        {
            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ComMensagem(Resource.EmailInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("CPF Invalido")]
        [InlineData("0000000000")]
        public void NaoDeveCriarComCpfInvalido(string cpfInvalido)
        {
            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
                .ComMensagem(Resource.CpfInvalido);
        }
    }
}