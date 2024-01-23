using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppi_Telefono.Models;

public partial class BdCineContext : DbContext
{
    public BdCineContext()
    {
    }

    public BdCineContext(DbContextOptions<BdCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TcTarjetum> TcTarjeta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Bd_Cine;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TcTarjetum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TC_TARJE__3214EC07A7DA8172");

            entity.ToTable("TC_TARJETA");

            entity.Property(e => e.AñoEx)
                .HasMaxLength(255)
                .HasColumnName("año_ex");
            entity.Property(e => e.Cv).HasColumnName("cv");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
