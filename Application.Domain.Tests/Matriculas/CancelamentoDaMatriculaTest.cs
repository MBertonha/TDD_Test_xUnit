using Application.Domain._Base;
using Application.Domain.Matriculas;
using Application.Domain.Tests._Builders;
using Application.Domain.Tests._Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Application.Domain.Tests.Matriculas
{
    public class CancelamentoDaMatriculaTest
    {
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly CancelamentoDaMatricula _cancelamentoDaMatricula;

        public CancelamentoDaMatriculaTest()
        {
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _cancelamentoDaMatricula = new CancelamentoDaMatricula(_matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositorio.Setup(r => r.ObterPorId(matricula.Id)).Returns(matricula);

            _cancelamentoDaMatricula.Cancelar(matricula.Id);

            Assert.IsTrue(matricula.Cancelada);
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            Matricula matriculaInvalida = null;
            const int matriculaIdInvalida = 1;
            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    _cancelamentoDaMatricula.Cancelar(matriculaIdInvalida))
                .ComMensagem(Resource.MatriculaNaoEncontrada);
        }
    }
}
