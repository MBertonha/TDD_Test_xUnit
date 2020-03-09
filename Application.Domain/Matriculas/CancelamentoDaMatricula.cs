using System;
using System.Collections.Generic;
using System.Text;
using Application.Domain._Base;

namespace Application.Domain.Matriculas
{
    public class CancelamentoDaMatricula
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Cancelar(int matriculaId)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
                .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            matricula.Cancelar();
        }
    }
}
