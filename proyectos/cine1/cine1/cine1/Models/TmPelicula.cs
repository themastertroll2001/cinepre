using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class TmPelicula
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Duracion { get; set; } = null!;

    public string Clasificacion { get; set; } = null!;

    public string RutaArchivo { get; set; } = "";

    public string? Director { get; set; }

    public string? Actor { get; set; }

    public string? Descripcion { get; set; }

    public int? Año { get; set; }

    public virtual ICollection<TdFuncion> TdFuncions { get; set; } = new List<TdFuncion>();
}
