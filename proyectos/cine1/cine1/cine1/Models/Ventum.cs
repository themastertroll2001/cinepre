using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class Ventum
{
    public int Id { get; set; }

 
    public DateTime FechaVenta { get; set; }

    public int Cantidad { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

   
}
