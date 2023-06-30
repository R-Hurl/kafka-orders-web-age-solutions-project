using System;
using System.Collections.Generic;
using kafka_project_dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace kafka_project_dal;

public partial class KafkaLabContext : DbContext
{
    public KafkaLabContext()
    {
    }

    public KafkaLabContext(DbContextOptions<KafkaLabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order08> Order08s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:azuresql-server-706703.database.windows.net,1433;Initial Catalog=KafkaLab;Persist Security Info=False;User ID=dba;Password=Passw0rD-706703;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order08>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order08__C3905BCF85C4F0BB");

            entity.ToTable("Order08");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OrderAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDescription)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
