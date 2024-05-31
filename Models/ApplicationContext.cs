using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingSample.Models;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.PartitionKey);

            entity.ToTable("Application");

            entity.Property(e => e.PartitionKey).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Count_Child).HasColumnName("count_child");
            entity.Property(e => e.Count_Person).HasColumnName("count_person");
            entity.Property(e => e.Count_Veg).HasColumnName("count_veg");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
