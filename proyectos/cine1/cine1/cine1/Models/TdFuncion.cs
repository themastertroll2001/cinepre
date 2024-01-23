using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class TdFuncion
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public int IdPeli { get; set; }

    public int IdSala { get; set; }

    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

    public virtual TmPelicula? IdPeliNavigation { get; set; }

    public virtual TmSala? IdSalaNavigation { get; set; }
}
