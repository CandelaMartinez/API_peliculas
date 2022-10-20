using API_peliculas.Data;
using API_peliculas.Models;
using API_peliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_peliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool ActualizarCategoria(Categoria categoria)
        {
            _db.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _db.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _db.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _db.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteCategoria(int Id)
        {
            return _db.Categoria.Any(c => c.Id == Id);
        }

        public Categoria GetCategoria(int CategoriaId)
        {
            return _db.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _db.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
