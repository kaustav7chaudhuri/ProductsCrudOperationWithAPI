using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.ApiDbContextFile;

public partial class InterviewDbContext : DbContext
{
    public InterviewDbContext()
    {
    }

    public InterviewDbContext(DbContextOptions<InterviewDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductManufacturer> ProductManufacturers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KAUSTAV-CHAUDHU;Database=InterviewDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD68933260");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IsNewArrival).HasDefaultValue(false);
            entity.Property(e => e.IsOnSale).HasDefaultValue(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductAvailability)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECCE0BC58633");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductManufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__ProductM__357E5CC15F981E48");

            entity.ToTable("ProductManufacturer");

            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
