using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Alunos
{
    public class AlunoParaListagemDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
