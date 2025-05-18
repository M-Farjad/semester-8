using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEClassWeb.Models;

public partial class Se21Context : DbContext
{
    public Se21Context()
    {
    }
    public Se21Context(DbContextOptions<Se21Context> options)
        : base(options)
    {
    }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Uetuser> Uetusers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=SE21;Integrated Security=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3214EC079DBAD86F");

            entity.ToTable("product");

            entity.Property(e => e.Pname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pname");
        });

        modelBuilder.Entity<Uetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__uetusers__3214EC0738C3AD1F");

            entity.ToTable("uetusers");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
