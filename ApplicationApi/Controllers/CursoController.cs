using System.Linq;
using Application.Domain;
using Application.Domain._Base;
using Application.Domain.Cursos;
using ApplicationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers {
    public class CursoController : Controller {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<CursoObj> _cursoRepositorio;

        public CursoController (ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<CursoObj> cursoRepositorio) {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }

        public IActionResult Index () {
            var cursos = _cursoRepositorio.Consultar ();

            if (cursos.Any ()) {
                var dtos = cursos.Select (c => new CursoParaListagemDto {
                    Id = c.Id,
                        Nome = c.Nome,
                        CargaHoraria = c.CargaHoraria,
                        PublicoAlvo = c.PublicoAlvo.ToString (),
                        Valor = c.Valor
                });
                return View("Index", PaginatedList<CursoParaListagemDto>.Create(dtos, 1, dtos.Count()));
            }

            return View ("Index", PaginatedList<CursoParaListagemDto>.Create (null, 0, 0));
        }

        public IActionResult Editar (int id) {
            var curso = _cursoRepositorio.ObterPorId (id);
            var dto = new CursoDto {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria,
                Valor = curso.Valor
            };

            return View ("NovoOuEditar", dto);
        }

        public IActionResult Novo () {
            return View ("NovoOuEditar", new CursoDto ());
        }

        [HttpPost]
        public IActionResult Salvar (CursoDto model) {
            _armazenadorDeCurso.Armazenar (model);
            return Ok ();
        }
    }
}
