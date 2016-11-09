using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161109110246_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int?>("ConferenceId")
                        .HasColumnName("conference_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created")
                        .HasDefaultValueSql("('now'::text)::date");

                    b.Property<string>("FilledForm")
                        .HasColumnName("filled_form")
                        .HasColumnType("json");

                    b.Property<int>("FormId")
                        .HasColumnName("form_id");

                    b.Property<bool>("IsCurrent")
                        .HasColumnName("is_current");

                    b.Property<DateTime>("LastModified")
                        .HasColumnName("last_modified");

                    b.Property<int>("PreviousVersion")
                        .HasColumnName("previous_version");

                    b.Property<int>("StatusId")
                        .HasColumnName("status_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("Version")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("FormId");

                    b.HasIndex("PreviousVersion");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("application");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<bool?>("Active")
                        .HasColumnName("active");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created")
                        .HasDefaultValueSql("('now'::text)::date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnName("firstname")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnName("lastname")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<int>("LdapId")
                        .HasColumnName("ldap_id");

                    b.Property<int>("MatNr")
                        .HasColumnName("mat_nr");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("bpchar")
                        .HasMaxLength(128);

                    b.Property<string>("SaltString")
                        .IsRequired()
                        .HasColumnName("salt_string")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("MatNr")
                        .IsUnique()
                        .HasName("app_user_mat_nr_key");

                    b.ToTable("app_user");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Asignee", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("ApplicationId")
                        .HasColumnName("application_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("asignee");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("ApplicationId")
                        .HasColumnName("application_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created")
                        .HasDefaultValueSql("('now'::text)::date");

                    b.Property<bool>("IsPrivate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_private")
                        .HasDefaultValueSql("false");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("text");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Conference", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnName("date_of_event");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("conference");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("field_type");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("form");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormField", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("FieldType")
                        .HasColumnName("field_type");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FieldType");

                    b.ToTable("form_field");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormHasField", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("FormFieldId")
                        .HasColumnName("form_field_id");

                    b.Property<int>("FormId")
                        .HasColumnName("form_id");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnName("label")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<int>("PositionIndex")
                        .HasColumnName("position_index");

                    b.Property<bool>("Required")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("required")
                        .HasDefaultValueSql("false");

                    b.Property<string>("Styling")
                        .IsRequired()
                        .HasColumnName("styling");

                    b.HasKey("Id");

                    b.HasIndex("FormFieldId");

                    b.HasIndex("FormId");

                    b.ToTable("form_has_field");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("permission");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("PermissionId")
                        .HasColumnName("permission_id");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("role_permission");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.UserHasRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_has_role");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Conference", "Conference")
                        .WithMany("Application")
                        .HasForeignKey("ConferenceId");

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", "Form")
                        .WithMany("Application")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", "PreviousVersionNavigation")
                        .WithMany("InversePreviousVersionNavigation")
                        .HasForeignKey("PreviousVersion")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Status", "Status")
                        .WithMany("Application")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Application")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Asignee", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", "Application")
                        .WithMany("Asignee")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Asignee")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Comment", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", "Application")
                        .WithMany("Comment")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Comment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormField", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", "FieldTypeNavigation")
                        .WithMany("FormField")
                        .HasForeignKey("FieldType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormHasField", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormField", "FormField")
                        .WithMany("FormHasField")
                        .HasForeignKey("FormFieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", "Form")
                        .WithMany("FormHasField")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.RolePermission", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Permission", "Permission")
                        .WithMany("RolePermission")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Role", "Role")
                        .WithMany("RolePermission")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.UserHasRole", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Role", "Role")
                        .WithMany("UserHasRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("UserHasRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
