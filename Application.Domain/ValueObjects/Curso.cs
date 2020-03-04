using Application.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Tests
{
    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            //Validações
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome Invalido");
            }
            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária inválida");
            }
            if (valor < 1)
            {
                throw new ArgumentException("Valor do curso inválido");
            }

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
