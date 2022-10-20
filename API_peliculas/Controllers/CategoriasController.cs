using API_peliculas.Models;
using API_peliculas.Models.DTOs;
using API_peliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_peliculas.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            var listaCategoriasDTO = new List<CategoriaDTO>();

            foreach (var categoria in listaCategorias)
            {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDTO>(categoria));
            }

            return Ok(listaCategoriasDTO);
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        public IActionResult GetCategoria(int categoriaId)
        {

            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            if (itemCategoria == null)
            {
                return NotFound();
            }
            //convierto categoria en categoriaDTO
            var itemCategoriaDTO = _mapper.Map<CategoriaDTO>(itemCategoria);

            return Ok(itemCategoriaDTO);
        }

        //recibe un DTO como parametro, lo transforma en modelo para insertarlo en la base de datos
        [HttpPost]
        public IActionResult CrearCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteCategoria(categoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe");
                return StatusCode(404, ModelState);
            }

            //guardo en variable categoria el resultado de transformar categoriaDTO a categoria model
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal al insertar el registro{categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            //devuelvo created 201, y recupero el registro creado recien llamando al metodo getCategoria
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }

        [HttpPatch("{categoriaId:int}", Name = "Actualizar Categoria")]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody] CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null || categoriaId != categoriaDTO.Id)
            {
                return BadRequest(ModelState);
            }

            //transformo categoriaDTO en categoria
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal al actualizar el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        //recibe un id como parametro, se fija si existe el registro, lo recupera por id, llama al metodo de borrar categoria del repositorio
        
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            if (!_ctRepo.ExisteCategoria(categoriaId))
            {
                return NotFound();
            }

            var categoria = _ctRepo.GetCategoria(categoriaId);

            if (!_ctRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{categoria.Nombre} ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        

    }
}
