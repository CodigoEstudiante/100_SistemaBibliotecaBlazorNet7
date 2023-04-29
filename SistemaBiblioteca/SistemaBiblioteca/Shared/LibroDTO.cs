using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Shared
{
    public class LibroDTO
    {
        public int IdLibro { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Autor { get; set; }
        public int? IdCategoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Editorial { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int? Ejemplares { get; set; }
        public byte[]? Portada { get; set; }
        public CategoriaDTO? IdCategoriaNavigation { get; set; }
    }
}
