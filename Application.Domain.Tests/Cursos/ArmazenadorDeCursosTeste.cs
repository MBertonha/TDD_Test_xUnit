using System.Xml;
using System;
using Xunit;
using Moq;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Domain.Tests._Util;
using Application.Domain.Tests._Builders;
using Application.Domain.Cursos;

namespace Application.Domain.Tests.Cursos
{
    public class ArmazenadorDeCursosTeste
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursosTeste()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(
                r => r.Adicionar(
                    It.Is<CursoObj>(
                        c => c.Nome == _cursoDto.Nome &&
                        c.Descricao == _cursoDto.Descricao
                    )
                )
            );
        }

        [Fact]
        public void NaoDevePublicoAlvoVazio()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.ThrowsException<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo Invalido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComNomeIgualAOutroSalvo()
        {
            //Simulando as ações dentro do banco de dados
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.ThrowsException<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso existente");
        }
    }
}
