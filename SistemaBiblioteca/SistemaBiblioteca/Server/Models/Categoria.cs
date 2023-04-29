using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Server.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
