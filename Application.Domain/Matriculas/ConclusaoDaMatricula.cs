using System;
using System.Collections.Generic;
using System.Text;
using Application.Domain._Base;

namespace Application.Domain.Matriculas
{
    public class ConclusaoDaMatricula
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Concluir(int matriculaId, double notaDoAluno)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
                .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            matricula.InformarNota(notaDoAluno);
        }
    }
}
