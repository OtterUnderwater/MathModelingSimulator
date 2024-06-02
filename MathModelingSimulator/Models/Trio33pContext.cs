using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MathModelingSimulator.Models;

public partial class Trio33pContext : DbContext
{
    public Trio33pContext()
    {
    }

    public Trio33pContext(DbContextOptions<Trio33pContext> options)
        : base(options)
    {
    }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Simulator> Simulators { get; set; }

    public virtual DbSet<SimulatorTask> SimulatorTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(Program.HostNpgsql);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("history_pk");

            entity.ToTable("history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdSimulator).HasColumnName("id_simulator");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.PassageDateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("passage_date_time");
            entity.Property(e => e.Result).HasColumnName("result");

            entity.HasOne(d => d.IdSimulatorNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdSimulator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_fk_s");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_fk_u");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pk");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Simulator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("simulators_pk");

            entity.ToTable("simulators");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Theory).HasColumnName("theory");
        });

        modelBuilder.Entity<SimulatorTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("simulator_tasks_pk");

            entity.ToTable("simulator_tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.IdSimulator).HasColumnName("id_simulator");
            entity.Property(e => e.ZadanieMatrix).HasColumnName("zadanie_matrix");

            entity.HasOne(d => d.IdSimulatorNavigation).WithMany(p => p.SimulatorTasks)
                .HasForeignKey(d => d.IdSimulator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("simulator_tasks_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasColumnType("character varying")
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
