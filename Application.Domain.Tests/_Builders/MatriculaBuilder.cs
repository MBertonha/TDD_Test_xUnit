﻿using Application.Domain.Alunos;
using Application.Domain.Matriculas;
using Application.Domain.PublicosAlvo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Tests._Builders
{
    public class MatriculaBuilder
    {
        protected Aluno Aluno;
        protected CursoObj Curso;
        protected double ValorPago;
        protected bool Cancelada;
        protected bool Concluido;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build();

            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(CursoObj curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }

        public MatriculaBuilder ComCancelada(bool cancelada)
        {
            Cancelada = cancelada;
            return this;
        }

        public MatriculaBuilder ComConcluido(bool concluido)
        {
            Concluido = concluido;
            return this;
        }

        public Matricula Build()
        {
            var matricula = new Matricula(Aluno, Curso, ValorPago);

            if (Cancelada)
                matricula.Cancelar();

            if (Concluido)
            {
                const double notaDoAluno = 7;
                matricula.InformarNota(notaDoAluno);
            }

            return matricula;
        }


    }
}
