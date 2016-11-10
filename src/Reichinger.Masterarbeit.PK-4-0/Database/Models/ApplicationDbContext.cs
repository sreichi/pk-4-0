using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Asignee> Asignee { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Conference> Conference { get; set; }
        public virtual DbSet<FieldType> FieldType { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormField> FormField { get; set; }
        public virtual DbSet<FormHasField> FormHasField { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<UserHasRole> UserHasRole { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.MatNr)
                    .HasName("app_user_mat_nr_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasDefaultValueSql("('now'::text)::date");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasDefaultValueSql("('now'::text)::date");
            });

            modelBuilder.Entity<Asignee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasDefaultValueSql("('now'::text)::date");

                entity.Property(e => e.IsPrivate).HasDefaultValueSql("false");
            });

            modelBuilder.Entity<Conference>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FieldType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FormField>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FormHasField>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Required).HasDefaultValueSql("false");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserHasRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}