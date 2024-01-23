using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class Boleto
{
    public int Id { get; set; }

    public decimal Costo { get; set; }

    public int IdAsiento { get; set; }

    public int IdUsuario { get; set; }

    public int IdFuncion { get; set; }

    public int IdVenta { get; set; }

    public virtual Asiento IdAsientoNavigation { get; set; } = null!;

    public virtual TdFuncion IdFuncionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
