using Xunit;

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
