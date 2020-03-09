using Application.Dados.Contexto;
using Application.Domain.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Data.Repositorio
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public Aluno ObterPeloCpf(string cpf)
        {
            var alunos = Context.Set<Aluno>().Where(a => a.Cpf == cpf);
            return alunos.Any() ? alunos.First() : null;
        }
    }
}
