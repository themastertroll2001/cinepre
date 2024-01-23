using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class TcTarjetum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string MesEx { get; set; } = null!;

    public string AñoEx { get; set; } = null!;
}
