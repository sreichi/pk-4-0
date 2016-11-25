﻿using System;
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
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<EnumOptionsTable> EnumOptionsTable { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<FieldHasStyle> FieldHasStyle { get; set; }
        public virtual DbSet<FieldHasValidation> FieldHasValidation { get; set; }
        public virtual DbSet<FieldType> FieldType { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormHasField> FormHasField { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<TypeHasConfig> TypeHasConfig { get; set; }
        public virtual DbSet<TypeHasStyle> TypeHasStyle { get; set; }
        public virtual DbSet<TypeHasValidation> TypeHasValidation { get; set; }
        public virtual DbSet<UserHasRole> UserHasRole { get; set; }
        public virtual DbSet<Validation> Validation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseNpgsql(@"User ID=dbadmin;Password=psqldocker;Host=192.168.99.100;Port=5432;Database=pk-database;Pooling=true;");
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

            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<EnumOptionsTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FieldHasStyle>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FieldHasValidation>(entity =>
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

            modelBuilder.Entity<FormHasField>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
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

            modelBuilder.Entity<Style>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TypeHasConfig>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TypeHasStyle>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TypeHasValidation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserHasRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Validation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}