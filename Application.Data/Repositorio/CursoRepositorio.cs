using Application.Dados.Contexto;
using Application.Domain;
using Application.Domain.Cursos;
using System;
using System.Linq;

namespace Application.Data.Repositorio
{
    public class CursoRepositorio : RepositorioBase<CursoObj>, ICursoRepositorio
    {
        public CursoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public CursoObj ObterPeloNome(string nome)
        {
            var entidade = Context.Set<CursoObj>().Where(c => c.Nome.Contains(nome));
            if (entidade.Any())
                return entidade.First();
            return null;
        }

        public void ObterPorId(object id)
        {
            throw new NotImplementedException();
        }
    }
}
