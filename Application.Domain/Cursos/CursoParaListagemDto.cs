using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Cursos
{
    public class CursoParaListagemDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
