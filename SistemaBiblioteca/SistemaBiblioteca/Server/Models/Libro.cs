using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Server.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public int? IdCategoria { get; set; }

    public string? Editorial { get; set; }

    public string? Ubicacion { get; set; }

    public int? Ejemplares { get; set; }

    public byte[]? Portada { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
