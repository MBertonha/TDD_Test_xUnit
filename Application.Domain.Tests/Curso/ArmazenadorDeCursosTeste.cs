using System.Xml;
using System;
using Xunit;
using Moq;
using Application.Domain.Enum;
using Bogus;

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
                PublicoAlvoId = 1,
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

        public interface ICursoRepositorio
        {
            void Adicionar(CursoObj curso);
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
                var curso =
                    new CursoObj(cursoDto.Nome,
                                    cursoDto.Descricao,
                                    cursoDto.CargaHoraria,
                                    PublicoAlvo.Estudante,
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
            public int PublicoAlvoId { get; set; }
            public double Valor { get; set; }
        }
    }
}
