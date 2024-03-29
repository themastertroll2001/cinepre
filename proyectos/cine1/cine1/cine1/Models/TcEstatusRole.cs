﻿using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class TcEstatusRole
{
    public int IdTcEstatusRoles { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<TdRolesUsuario> TdRolesUsuarios { get; set; } = new List<TdRolesUsuario>();
}
