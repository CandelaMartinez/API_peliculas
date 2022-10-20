using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static API_peliculas.Models.Pelicula;

namespace API_peliculas.Models.DTOs
{
    public class PeliculaUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ruta es obligatoria")]
        public string RutaImagen { get; set; }


        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duracion es obligatoria")]
        public string Duracion { get; set; }

        public TipoClasificacion Clasificacion { get; set; }

        public int categoriaId { get; set; }
    }
}
