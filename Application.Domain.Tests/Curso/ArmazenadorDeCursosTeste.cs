using System.Xml;
using System;
using Xunit;
using Moq;
using Application.Domain.Enum;

namespace Application.Domain.Tests.Curso
{
    public class ArmazenadorDeCursosTeste
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Nome = "Curso A",
                Descricao = "Descricao",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                Valor = 850.00
            };

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

            armazenadorDeCurso.Armazenar(cursoDto);

            cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<CursoObj>()));
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
            public int CargaHoraria { get; set; }
            public int PublicoAlvoId { get; set; }
            public double Valor { get; set; }
        }
    }
}
