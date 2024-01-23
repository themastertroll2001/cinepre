using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cine1.Models;

public partial class TmSala
{
    public int Id { get; set; }
    //especifica un rango de valores permitidos para una propiedad numérica.
    [Range(1, 10, ErrorMessage = "El número de sala debe estar entre 1 a 10")]
    public int NumeroSala { get; set; }
    [Range(40, 50, ErrorMessage = "La capacidad de asientos debe ser 40 o 50")]
    public int CapacidadAsiento { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

    public virtual ICollection<TdFuncion> TdFuncions { get; set; } = new List<TdFuncion>();
}
