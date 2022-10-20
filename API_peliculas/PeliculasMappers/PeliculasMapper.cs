using API_peliculas.Models;
using API_peliculas.Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_peliculas.PeliculasMappers
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
