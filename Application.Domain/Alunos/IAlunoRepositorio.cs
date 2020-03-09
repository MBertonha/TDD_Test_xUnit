using Application.Domain._Base;

namespace Application.Domain.Alunos
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        Aluno ObterPeloCpf(string cpf);
    }
}
