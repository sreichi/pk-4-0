using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Attendant> Attendant { get; set; }
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.RzName)
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

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationId, e.UserId })
                    .HasName("PK_assignment");

                entity.HasOne(assignment => assignment.User)
                    .WithMany(user => user.Assignment)
                    .HasForeignKey(assignment => assignment.UserId);

                entity.HasOne(assignment => assignment.Application)
                    .WithMany(application => application.Assignment)
                    .HasForeignKey(assignment => assignment.ApplicationId);
            });

            modelBuilder.Entity<Attendant>(entity =>
            {
                entity.HasKey(e => new { e.ConferenceId, e.UserId })
                    .HasName("PK_attendant");
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
                entity.HasKey(e => new { e.FieldId, e.StyleId })
                    .HasName("PK_field_has_style");

                entity.HasOne(fhs => fhs.Style)
                    .WithMany(styles => styles.FieldHasStyle)
                    .HasForeignKey(fhs => fhs.StyleId);

                entity.HasOne(fhs => fhs.Field)
                    .WithMany(field => field.FieldHasStyle)
                    .HasForeignKey(fhs => fhs.FieldId);
            });

            modelBuilder.Entity<FieldHasValidation>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.ValidationId })
                    .HasName("PK_field_has_validation");

                entity.HasOne(fhv => fhv.Validation)
                    .WithMany(validation => validation.FieldHasValidation)
                    .HasForeignKey(fhv => fhv.ValidationId);

                entity.HasOne(fhv => fhv.Field)
                    .WithMany(field => field.FieldHasValidation)
                    .HasForeignKey(fhv => fhv.FieldId);
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
                entity.HasKey(e => new { e.FormId, e.FieldId })
                    .HasName("PK_form_has_field");

                entity.HasOne(fhf => fhf.Form)
                    .WithMany(form => form.FormHasField)
                    .HasForeignKey(fhf => fhf.FormId);

                entity.HasOne(fhf => fhf.Field)
                    .WithMany(field => field.FormHasField)
                    .HasForeignKey(fhf => fhf.FieldId);
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
                entity.HasKey(e => new { e.RoleId, e.PermissionId })
                    .HasName("PK_role_permission");

                entity.HasOne(rp => rp.Role)
                    .WithMany(role => role.RolePermission)
                    .HasForeignKey(rp => rp.RoleId);

                entity.HasOne(rp => rp.Permission)
                    .WithMany(permission => permission.RolePermission)
                    .HasForeignKey(rp => rp.PermissionId);
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
                entity.HasKey(e => new { e.ConfigId, e.FieldTypeId })
                    .HasName("PK_type_has_config");

                entity.HasOne(thc => thc.Config)
                    .WithMany(config => config.TypeHasConfig)
                    .HasForeignKey(thc => thc.ConfigId);

                entity.HasOne(thc => thc.FieldType)
                    .WithMany(fieldType => fieldType.TypeHasConfig)
                    .HasForeignKey(thc => thc.FieldTypeId);
            });

            modelBuilder.Entity<TypeHasStyle>(entity =>
            {
                entity.HasKey(e => new { e.FieldTypeId, e.StyleId })
                    .HasName("PK_type_has_style");

                entity.HasOne(ths => ths.Style)
                    .WithMany(style => style.TypeHasStyle)
                    .HasForeignKey(ths => ths.StyleId);

                entity.HasOne(ths => ths.FieldType)
                    .WithMany(fieldType => fieldType.TypeHasStyle)
                    .HasForeignKey(ths => ths.FieldTypeId);
            });

            modelBuilder.Entity<TypeHasValidation>(entity =>
            {
                entity.HasKey(e => new { e.FieldTypeId, e.ValidationId })
                    .HasName("PK_type_has_validation");

                entity.HasOne(thv => thv.Validation)
                    .WithMany(validation => validation.TypeHasValidation)
                    .HasForeignKey(thv => thv.ValidationId);

                entity.HasOne(thv => thv.FieldType)
                    .WithMany(fieldType => fieldType.TypeHasValidation)
                    .HasForeignKey(thv => thv.FieldTypeId);
            });

            modelBuilder.Entity<UserHasRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId })
                    .HasName("PK_user_has_role");

                entity.HasOne(uhr => uhr.Role)
                    .WithMany(role => role.UserHasRole)
                    .HasForeignKey(uhr => uhr.RoleId);

                entity.HasOne(uhr => uhr.User)
                    .WithMany(user => user.UserHasRole)
                    .HasForeignKey(uhr => uhr.UserId);
            });

            modelBuilder.Entity<Validation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}