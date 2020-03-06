using System.Xml;
using System;
using Xunit;
using Moq;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Domain.Tests._Util;
using Application.Domain.Tests._Builders;

namespace Application.Domain.Tests.Curso
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
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.ThrowsException<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso existente");
        }

        public interface ICursoRepositorio
        {
            void Adicionar(CursoObj curso);
            CursoObj ObterPeloNome(string nome);
        }

        public class ArmazenadorDeCurso
        {
            private readonly ICursoRepositorio _cursoRepositorio;
            public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
            {
                _cursoRepositorio = cursoRepositorio;
            }

            public void Armazenar(CursoDto cursoDto)
            {
                var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

                if (cursoJaSalvo != null)
                {
                    throw new ArgumentException("Nome do curso existente");
                }

                System.Enum.TryParse(typeof(Application.Domain.Enum.PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

                if (publicoAlvo == null)
                {
                    throw new ArgumentException("Publico Alvo Invalido");
                }

                var curso =
                    new CursoObj(cursoDto.Nome,
                                    cursoDto.Descricao,
                                    cursoDto.CargaHoraria,
                                    (Application.Domain.Enum.PublicoAlvo)publicoAlvo,
                                    cursoDto.Valor
                                );
                _cursoRepositorio.Adicionar(curso);
            }
        }

        public class CursoDto
        {
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public double CargaHoraria { get; set; }
            public string PublicoAlvo { get; set; }
            public double Valor { get; set; }
        }
    }
}
