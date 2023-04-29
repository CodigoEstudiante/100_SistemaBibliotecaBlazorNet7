using System;
using System.Collections.Generic;

namespace SistemaBiblioteca.Server.Models;

public partial class NumeroCorrelativo
{
    public int IdNumeroCorrelativo { get; set; }

    public string Prefijo { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int UltimoNumero { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
