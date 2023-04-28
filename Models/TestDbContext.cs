using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VentasDetalle> VentasDetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TestDB;Uid=sa; password=admin;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProd);

            entity.ToTable("PRODUCTOS");

            entity.Property(e => e.IdProd)
                .ValueGeneratedNever()
                .HasColumnName("ID_PROD");
            entity.Property(e => e.EstadoProd).HasColumnName("ESTADO_PROD");
            entity.Property(e => e.NombreProd)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE_PROD");
            entity.Property(e => e.PrecioProd)
                .HasColumnType("money")
                .HasColumnName("PRECIO_PROD");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("ID_USER");
            entity.Property(e => e.EstadoUser).HasColumnName("ESTADO_USER");
            entity.Property(e => e.NombreUser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NOMBRE_USER");
        });

        modelBuilder.Entity<VentasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVent);

            entity.ToTable("VENTAS_DETALLE");

            entity.Property(e => e.IdVent)
                .ValueGeneratedNever()
                .HasColumnName("ID_VENT");
            entity.Property(e => e.IdProd).HasColumnName("ID_PROD");
            entity.Property(e => e.IdUser).HasColumnName("ID_USER");
            entity.Property(e => e.PrecioVent)
                .HasColumnType("money")
                .HasColumnName("PRECIO_VENT");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdProd)
                .HasConstraintName("FK_VENTAS_D_RELATIONS_PRODUCTO");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_VENTAS_D_RELATIONS_USUARIO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
