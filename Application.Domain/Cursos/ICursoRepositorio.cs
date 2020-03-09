using Application.Domain._Base;

namespace Application.Domain.Cursos
{
    public interface ICursoRepositorio : IRepositorio<CursoObj>
    {
        CursoObj ObterPeloNome(string nome);
    }
}
