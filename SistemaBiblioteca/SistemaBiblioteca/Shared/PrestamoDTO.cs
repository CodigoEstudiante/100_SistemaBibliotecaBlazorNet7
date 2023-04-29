using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Shared
{
    public class PrestamoDTO
    {
        public int IdPrestamo { get; set; }
        public string? Codigo { get; set; }
        public int? IdEstadoPrestamo { get; set; }
        public int? IdLector { get; set; }
        public int? IdLibro { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public DateTime? FechaConfirmacionDevolucion { get; set; }
        public string? EstadoEntregado { get; set; }
        public string? EstadoRecibido { get; set; }
        public virtual EstadoPrestamoDTO? IdEstadoPrestamoNavigation { get; set; }
        public virtual LectorDTO? IdLectorNavigation { get; set; }
        public virtual LibroDTO? IdLibroNavigation { get; set; }
    }
}
