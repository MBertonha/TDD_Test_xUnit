using System;
using Application.Domain.Tests;

namespace Application.Domain.ValueObjects
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
            {
                throw new ArgumentException("Nome do curso existente");
            }

            System.Enum.TryParse(typeof(Application.Domain.Enum.PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
            {
                throw new ArgumentException("Publico Alvo Invalido");
            }

            var curso =
                new CursoObj(cursoDto.Nome,
                                cursoDto.Descricao,
                                cursoDto.CargaHoraria,
                                (Application.Domain.Enum.PublicoAlvo)publicoAlvo,
                                cursoDto.Valor
                            );
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(CursoObj curso);
        CursoObj ObterPeloNome(string nome);
    }
    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
