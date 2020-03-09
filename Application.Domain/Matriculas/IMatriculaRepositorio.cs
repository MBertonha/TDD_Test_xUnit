using Application.Domain._Base;

namespace Application.Domain.Matriculas
{
    interface IMatriculaRepositorio : IRepositorio<Matricula>
    {
        Matricula ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio);

        Matricula CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio);
    }
}
