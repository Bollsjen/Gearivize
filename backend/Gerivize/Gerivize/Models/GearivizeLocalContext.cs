﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gerivize.Models;

public partial class GearivizeLocalContext : DbContext
{
    public GearivizeLocalContext()
    {
    }

    public GearivizeLocalContext(DbContextOptions<GearivizeLocalContext> options)
        : base(options)
    {
        // disable auto-transactions to ensure rollback on failure
        Database.AutoTransactionsEnabled = false;
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }
    public virtual DbSet<Instrument> Instruments { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=gearivize_local;user id=root;password=FiskerLars", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity
                .ToTable("__efmigrationshistory")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
