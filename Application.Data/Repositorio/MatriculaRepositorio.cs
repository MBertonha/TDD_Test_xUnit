using Application.Dados.Contexto;
using Application.Domain.Matriculas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Data.Repositorio
{
    public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
    {
        public MatriculaRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public Matricula CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            throw new NotImplementedException();
        }

        public Matricula ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            throw new NotImplementedException();
        }

        public override List<Matricula> Consultar()
        {
            var query = Context.Set<Matricula>()
                .Include(i => i.Aluno)
                .Include(i => i.Curso)
                .ToList();

            return query;
        }
    }
}
