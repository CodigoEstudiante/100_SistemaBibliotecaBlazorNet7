using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Server.Models;

public partial class Prestamo
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

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual EstadoPrestamo? IdEstadoPrestamoNavigation { get; set; }

    public virtual Lector? IdLectorNavigation { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }
}
