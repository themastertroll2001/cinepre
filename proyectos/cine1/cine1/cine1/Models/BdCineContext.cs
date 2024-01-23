using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cine1.Models;

public partial class BdCineContext : DbContext
{
    public BdCineContext()
    {
    }

    public BdCineContext(DbContextOptions<BdCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<Boleto> Boletos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TcEstatusRole> TcEstatusRoles { get; set; }

    public virtual DbSet<TcTarjetum> TcTarjeta { get; set; }

    public virtual DbSet<TdFuncion> TdFuncions { get; set; }

    public virtual DbSet<TdRolesUsuario> TdRolesUsuarios { get; set; }

    public virtual DbSet<TmPelicula> TmPeliculas { get; set; }

    public virtual DbSet<TmSala> TmSalas { get; set; }

   
    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Bd_Cine;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asiento__3214EC0782AF0F5B");

            entity.ToTable("Asiento");

            entity.Property(e => e.NumAsiento).HasColumnName("Num_Asiento");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asiento__IdSala__693CA210");
        });

        modelBuilder.Entity<Boleto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Boleto__3214EC07BA9B260C");

            entity.ToTable("Boleto");

            entity.Property(e => e.Costo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costo");

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.IdAsiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boleto__IdAsient__6C190EBB");

            entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.IdFuncion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boleto__IdFuncio__6E01572D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boleto__IdUsuari__6D0D32F4");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Boletos)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Boleto__IdVenta__6EF57B66");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0711B20F03");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TcEstatusRole>(entity =>
        {
            entity.HasKey(e => e.IdTcEstatusRoles).HasName("PK__TC_Estat__135B99E39D32E104");

            entity.ToTable("TC_Estatus_Roles");

            entity.Property(e => e.IdTcEstatusRoles).HasColumnName("id_tc_estatusRoles");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TcTarjetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TC_TARJE__3214EC07A7DA8172");

            entity.ToTable("TC_TARJETA");

            entity.Property(e => e.AñoEx)
                .HasMaxLength(255)
                .HasColumnName("año_ex");
            entity.Property(e => e.MesEx)
                .HasMaxLength(255)
                .HasColumnName("mes_ex");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .HasColumnName("numero");
        });

        modelBuilder.Entity<TdFuncion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TD_Funci__3214EC07A7B927DD");

            entity.ToTable("TD_Funcion");

            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.IdPeliNavigation).WithMany(p => p.TdFuncions)
                .HasForeignKey(d => d.IdPeli)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TD_Funcio__IdPel__5FB337D6");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.TdFuncions)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TD_Funcio__IdSal__60A75C0F");
        });

        modelBuilder.Entity<TdRolesUsuario>(entity =>
        {
            entity.HasKey(e => e.IdRolesTd).HasName("PK__TD_Roles__849739D7643F9266");

            entity.ToTable("TD_RolesUsuarios");

            entity.Property(e => e.IdRolesTd).HasColumnName("id_roles_td");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEstatus).HasColumnName("id_estatus");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.TdRolesUsuarios)
                .HasForeignKey(d => d.IdEstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TD_RolesU__id_es__59063A47");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.TdRolesUsuarios)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TD_RolesU__id_ro__5812160E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TdRolesUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TD_RolesU__usuar__571DF1D5");
        });

        modelBuilder.Entity<TmPelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TM_Pelic__3214EC0789DE4846");

            entity.ToTable("TM_Pelicula");

            entity.Property(e => e.Actor)
                .HasMaxLength(100)
                .HasColumnName("actor");
            entity.Property(e => e.Año).HasColumnName("año");
            entity.Property(e => e.Clasificacion)
                .HasMaxLength(15)
                .HasColumnName("clasificacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .HasColumnName("director");
            entity.Property(e => e.Duracion)
                .HasMaxLength(30)
                .HasColumnName("duracion");
            entity.Property(e => e.RutaArchivo)
                .HasMaxLength(4000)
                .HasColumnName("ruta_archivo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<TmSala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TM_Sala__3214EC07788E087B");

            entity.ToTable("TM_Sala");

            entity.Property(e => e.CapacidadAsiento).HasColumnName("Capacidad_asiento");
            entity.Property(e => e.NumeroSala).HasColumnName("Numero_sala");
        });

      
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC071BDA8FC2");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A1904A57ADD").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE060930A41").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.CuentaVerificada).HasDefaultValueSql("((0))");
            entity.Property(e => e.FechaTokenRecuperacionContrasena).HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.RolId).HasDefaultValueSql("((4))");
            entity.Property(e => e.TokenRecuperacionContrasena).HasMaxLength(255);
            entity.Property(e => e.TokenVerificacionCorreo).HasMaxLength(255);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolId__4E88ABD4");
        });

      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
