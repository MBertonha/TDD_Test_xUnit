using System;
using Application.Domain._Base;
using Application.Domain.PublicosAlvo;

namespace Application.Domain
{
    public class CursoObj : Entidade
    {

        private CursoObj() { }

        public CursoObj(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            //Validações
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome Invalido");
            }
            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horaria invalida");
            }
            if (valor < 1)
            {
                throw new ArgumentException("Valor do curso invalido");
            }

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .DispararExcecaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcecaoSeExistir();

            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
