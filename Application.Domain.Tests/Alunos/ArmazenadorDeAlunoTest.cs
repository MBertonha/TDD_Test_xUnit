using Application.Domain._Base;
using Application.Domain.Alunos;
using Application.Domain.PublicosAlvo;
using Application.Domain.Tests._Builders;
using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Application.Domain.Tests.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly Faker _faker;
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;

        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empregado.ToString(),
            };
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            var conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorio.Object, conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.Is<Aluno>(a => a.Nome == _alunoDto.Nome)));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaFoiCadastrado()
        {
            var alunoComMesmoCpf = AlunoBuilder.Novo().ComId(34).Build();
            _alunoRepositorio.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoComMesmoCpf);

            Assert.ThrowsException<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CpfJaCadastrado);
        }

        [Fact]
        public void DeveEditarNomeDoAluno()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equals(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoesDoAluno()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;
            var emailEsperado = alunoJaSalvo.Email;
            var publicoAlvoEsperado = alunoJaSalvo.PublicoAlvo;
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equals(cpfEsperado, alunoJaSalvo.Cpf);
            Assert.Equals(emailEsperado, alunoJaSalvo.Email);
            Assert.Equals(publicoAlvoEsperado, alunoJaSalvo.PublicoAlvo);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
