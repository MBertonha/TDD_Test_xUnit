using Application.Domain._Base;
using Application.Domain.Alunos;
using Application.Domain.PublicosAlvo;
using Application.Domain.Tests._Builders;
using Application.Domain.Tests.Cursos;
using Bogus;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Application.Domain.Matriculas
{
    public class MatriculasTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };

            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            CursoTeste cursoInvalido = null;

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            const double valorPagoInvalido = 0;

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoMaiorQueCurso = curso.Valor + 1;

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoMaiorQueCurso).Build())
                .ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            var curso = CursoBuilder.Novo().ComValor(100).ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();
            var valorPagoComDesconto = curso.Valor - 1;

            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(valorPagoComDesconto).Build();

            Assert.IsTrue(matricula.TemDesconto);
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Universitário).Build();

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    MatriculaBuilder.Novo().ComAluno(aluno).ComCurso(curso).Build())
                .ComMensagem(Resource.PublicosAlvoDiferentes);
        }

        [Fact]
        public void DeveInformarANotaDoAlunoParMatricula()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.Equals(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DeveIndicarQueCuroFoiConcluido()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.IsTrue(matricula.CursoConcluido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarComNotaInvalida(double notaDoAlunoInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Build();

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    matricula.InformarNota(notaDoAlunoInvalida))
                .ComMensagem(Resource.NotaDoAlunoInvalida);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancelar();

            Assert.IsTrue(matricula.Cancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
        {
            const double notaDoAluno = 3;
            var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    matricula.InformarNota(notaDoAluno))
                .ComMensagem(Resource.MatriculaCancelada);
        }

        [Fact]
        public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
        {
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();

            Assert.ThrowsException<ExcecaoDeDominio>(() =>
                    matricula.Cancelar())
                .ComMensagem(Resource.MatriculaConcluida);
        }
    }
}
}