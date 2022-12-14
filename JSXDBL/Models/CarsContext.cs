using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JSXDBL.Models;

public partial class CarsContext : DbContext
{
    public CarsContext()
    {
    }

    public CarsContext(DbContextOptions<CarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddCars> AddCars { get; set; }

    public virtual DbSet<Cars> Cars { get; set; }

    public virtual DbSet<Fuel> Fuel { get; set; }

    public virtual DbSet<Gear> Gear { get; set; }

    public virtual DbSet<Make> Make { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=soft-eng.database.windows.net;Initial Catalog=Student;Persist Security Info=True;User ID=hallgato;Password=Password123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddCars>(entity =>
        {
            entity.HasKey(e => e.AddCarsSk);

            entity.Property(e => e.AddCarsSk).HasColumnName("AddCarsSK");
            entity.Property(e => e.MakeName1234).HasMaxLength(50);
            entity.Property(e => e.ModelName1234).HasMaxLength(50);
        });

        modelBuilder.Entity<Cars>(entity =>
        {
            entity.HasKey(e => e.CarSk);

            entity.Property(e => e.CarSk).HasColumnName("carSK");
            entity.Property(e => e.FuelFk).HasColumnName("fuelFK");
            entity.Property(e => e.GearFk).HasColumnName("gearFK");
            entity.Property(e => e.MakeFk).HasColumnName("makeFK");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");

            entity.HasOne(d => d.FuelFkNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.FuelFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Fuel");

            entity.HasOne(d => d.GearFkNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.GearFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Gear");

            entity.HasOne(d => d.MakeFkNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.MakeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cars_Make");
        });

        modelBuilder.Entity<Fuel>(entity =>
        {
            entity.HasKey(e => e.FuelSk);

            entity.Property(e => e.FuelSk).HasColumnName("fuelSK");
            entity.Property(e => e.FuelName)
                .HasMaxLength(50)
                .HasColumnName("fuelName");
        });

        modelBuilder.Entity<Gear>(entity =>
        {
            entity.HasKey(e => e.GearSk);

            entity.Property(e => e.GearSk).HasColumnName("gearSK");
            entity.Property(e => e.GearName)
                .HasMaxLength(50)
                .HasColumnName("gearName");
        });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.HasKey(e => e.MakeSk);

            entity.Property(e => e.MakeSk).HasColumnName("makeSK");
            entity.Property(e => e.MakeName)
                .HasMaxLength(50)
                .HasColumnName("makeName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
