using Application.Domain.PublicosAlvo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Tests._Builders
{
    public class CursoBuilder
    {
        private string _nome = "informatica";
        private double _cargaHoraria = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 90;
        private string _descricao = "Lorem ipsum dolor";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }
        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }
        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }
        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }
        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoObj Build()
        {
            return new CursoObj(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }

        public CursoBuilder ComId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
