using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Reservas.Models
{
    public partial class DbReservasContext : DbContext
    {
        public DbReservasContext()
        {
        }

        public DbReservasContext(DbContextOptions<DbReservasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbLab> TbLabs { get; set; } = null!;
        public virtual DbSet<TbModulo> TbModulos { get; set; } = null!;
        public virtual DbSet<TbReserva> TbReservas { get; set; } = null!;
        public virtual DbSet<TbRol> TbRols { get; set; } = null!;
        public virtual DbSet<TbUsr> TbUsrs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-FT1TUUM\\SQLEXPRESS; Database=DbReservas; Trusted_Connection= True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbLab>(entity =>
            {
                entity.HasKey(e => e.IdLab);

                entity.ToTable("TB_Lab");

                entity.Property(e => e.DescripcionLab)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreLab)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbModulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo);

                entity.ToTable("TB_Modulo");
            });

            modelBuilder.Entity<TbReserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.ToTable("TB_Reservas");

                entity.Property(e => e.Curso)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Docente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaReserva).HasColumnType("date");

                entity.Property(e => e.FinReserva).HasColumnType("date");

                entity.HasOne(d => d.IdLabNavigation)
                    .WithMany(p => p.TbReservas)
                    .HasForeignKey(d => d.IdLab)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_Reservas_TB_Lab");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.TbReservas)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_Reservas_TB_Modulo");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.TbReservas)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_Reservas_TB_Usr");
            });

            modelBuilder.Entity<TbRol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("TB_Rol");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbUsr>(entity =>
            {
                entity.HasKey(e => e.IdUsr);

                entity.ToTable("TB_Usr");

                entity.Property(e => e.ApellidoUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsernameUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RolUsrNavigation)
                    .WithMany(p => p.TbUsrs)
                    .HasForeignKey(d => d.RolUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_Usr_TB_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
