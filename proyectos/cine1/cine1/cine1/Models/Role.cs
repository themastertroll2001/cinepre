﻿using System;
using System.Collections.Generic;

namespace cine1.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<TdRolesUsuario> TdRolesUsuarios { get; set; } = new List<TdRolesUsuario>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
