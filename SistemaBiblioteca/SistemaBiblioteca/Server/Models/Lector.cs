using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Server.Models;

public partial class Lector
{
    public int IdLector { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
