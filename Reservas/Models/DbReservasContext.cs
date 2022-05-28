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

        public virtual DbSet<TbRol> TbRols { get; set; } = null!;
        public virtual DbSet<TbUsr> TbUsrs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESARROLLO\\SQLEXPRESS; Database=DbReservas; Trusted_Connection= True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
