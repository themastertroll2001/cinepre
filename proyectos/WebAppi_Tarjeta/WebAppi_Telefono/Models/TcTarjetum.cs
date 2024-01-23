using System;
using System.Collections.Generic;

namespace WebAppi_Telefono.Models;

public partial class TcTarjetum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string MesEx { get; set; } = null!;

    public string AñoEx { get; set; } = null!;

    public int? Cv { get; set; }
}
