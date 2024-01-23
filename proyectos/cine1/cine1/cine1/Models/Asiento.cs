using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class Asiento
{
    public int Id { get; set; }

    public int? Fila { get; set; }

    public int? NumAsiento { get; set; }

    public int IdSala { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

    public virtual TmSala IdSalaNavigation { get; set; } = null!;
}
