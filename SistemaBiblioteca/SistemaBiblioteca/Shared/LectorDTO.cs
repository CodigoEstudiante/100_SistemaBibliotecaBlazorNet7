using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Shared
{
    public class LectorDTO
    {
        public int IdLector { get; set; }
        public string? Codigo { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
