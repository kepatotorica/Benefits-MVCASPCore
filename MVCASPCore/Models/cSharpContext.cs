using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Benefacts.Models
{
    public partial class cSharpContext : DbContext
    {
        public cSharpContext()
        {
        }

        public cSharpContext(DbContextOptions<cSharpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Relative> Relative { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AId);

                entity.ToTable("admin");

                entity.Property(e => e.AId).HasColumnName("a_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Relative>(entity =>
            {
                entity.HasKey(e => e.RelId);

                entity.ToTable("relative");

                entity.Property(e => e.RelId).HasColumnName("rel_id");

                entity.Property(e => e.FName)
                    .HasColumnName("f_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LName)
                    .HasColumnName("l_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Relation)
                    .HasColumnName("relation")
                    .HasMaxLength(8);

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Relative)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("relative_u_id_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.ToTable("users");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FName)
                    .HasColumnName("f_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50);

                entity.Property(e => e.LName)
                    .HasColumnName("l_name")
                    .HasMaxLength(50);
            });
        }
    }
}
