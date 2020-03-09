﻿using Application.Domain._Base;
using Application.Domain.Alunos;
using Application.Domain.Cursos;
using Application.Domain.PublicosAlvo;
using Application.Domain.Tests._Builders;
using Application.Domain.Tests._Util;
using Application.Domain.Tests.Cursos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace Application.Domain.Matriculas
{
    public class CriacaoDaMatriculaTest
    {
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly CriacaoDaMatricula _criacaoDaMatricula;
        private readonly MatriculaDto _matriculaDto;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly Aluno _aluno;
        private readonly CursoTeste _curso;

        public CriacaoDaMatriculaTest()
        {
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _aluno = AlunoBuilder.Novo().ComId(23).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(45).ComPublicoAlvo(PublicoAlvo.Universitário).Build();
            _cursoRepositorio.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);

            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };

            _criacaoDaMatricula = new CriacaoDaMatricula(_alunoRepositorio.Object, _cursoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            CursoTeste cursoInvalido = null;
            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.CursoId)).Returns(cursoInvalido);

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            Aluno alunoInvalido = null;
            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    _criacaoDaMatricula.Criar(_matriculaDto))
                .ComMensagem(Resource.AlunoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _criacaoDaMatricula.Criar(_matriculaDto);

            _matriculaRepositorio.Verify(r => r.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
        }
    }
}