using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170227102526_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<Guid?>("ConferenceId")
                        .HasColumnName("conference_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created")
                        .HasDefaultValueSql("('now'::text)::date");

                    b.Property<string>("FilledForm")
                        .HasColumnName("filled_form")
                        .HasColumnType("json");

                    b.Property<Guid>("FormId")
                        .HasColumnName("form_id");

                    b.Property<bool>("IsCurrent")
                        .HasColumnName("is_current");

                    b.Property<DateTime>("LastModified")
                        .HasColumnName("last_modified");

                    b.Property<Guid?>("PreviousVersion")
                        .HasColumnName("previous_version");

                    b.Property<int>("StatusId")
                        .HasColumnName("status_id");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("Version")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("FormId");

                    b.HasIndex("PreviousVersion");

                    b.HasIndex("UserId");

                    b.ToTable("application");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
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

                    b.Property<string>("EmployeeType")
                        .IsRequired()
                        .HasColumnName("employee_type")
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("bpchar")
                        .HasMaxLength(128);

                    b.Property<string>("RzName")
                        .IsRequired()
                        .HasColumnName("rz_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("SaltString")
                        .IsRequired()
                        .HasColumnName("salt_string")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("RzName")
                        .IsUnique()
                        .HasName("app_user_mat_nr_key");

                    b.ToTable("app_user");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Assignment", b =>
                {
                    b.Property<Guid>("ApplicationId")
                        .HasColumnName("application_id");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("ApplicationId", "UserId")
                        .HasName("PK_assignment");

                    b.HasIndex("UserId");

                    b.ToTable("assignment");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Attendant", b =>
                {
                    b.Property<Guid>("ConferenceId")
                        .HasColumnName("conference_id");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("TypeOfAttendance")
                        .HasColumnName("type_of_attendance");

                    b.HasKey("ConferenceId", "UserId")
                        .HasName("PK_attendant");

                    b.HasIndex("UserId");

                    b.ToTable("attendant");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnName("application_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created")
                        .HasDefaultValueSql("('now'::text)::date");

                    b.Property<bool>("IsPrivate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_private")
                        .HasDefaultValueSql("false");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnName("message");

                    b.Property<bool>("RequiresChanges")
                        .HasColumnName("requires_changes");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("ConferenceConfiguration")
                        .HasColumnName("conference_configuration")
                        .HasColumnType("json");

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnName("date_of_event");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description");

                    b.Property<string>("EndOfEvent")
                        .IsRequired()
                        .HasColumnName("end_of_event")
                        .HasColumnType("varchar")
                        .HasMaxLength(5);

                    b.Property<int>("NumberOfConference")
                        .HasColumnName("number_of_conference");

                    b.Property<string>("RoomOfEvent")
                        .IsRequired()
                        .HasColumnName("room_of_event")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("StartOfEvent")
                        .IsRequired()
                        .HasColumnName("start_of_event")
                        .HasColumnType("varchar")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("conference");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Config", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("config");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.EnumOptionsTable", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("ReferenceTableName")
                        .IsRequired()
                        .HasColumnName("reference_table_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("enum_options_table");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("ContentType")
                        .HasColumnName("content_type")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<bool?>("Disabled")
                        .HasColumnName("disbaled");

                    b.Property<Guid?>("EnumOptionsTableId")
                        .HasColumnName("enum_options_table_id");

                    b.Property<Guid>("FieldType")
                        .HasColumnName("field_type");

                    b.Property<string>("Label")
                        .HasColumnName("label")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<bool?>("MultipleSelect")
                        .HasColumnName("multiple_select");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Options")
                        .HasColumnName("options")
                        .HasColumnType("json");

                    b.Property<string>("Placeholder")
                        .HasColumnName("placeholder")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<bool?>("Required")
                        .HasColumnName("required");

                    b.Property<string>("Value")
                        .HasColumnName("value")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("EnumOptionsTableId");

                    b.HasIndex("FieldType");

                    b.ToTable("field");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldHasStyle", b =>
                {
                    b.Property<Guid>("FieldId")
                        .HasColumnName("field_id");

                    b.Property<Guid>("StyleId")
                        .HasColumnName("style_id");

                    b.HasKey("FieldId", "StyleId")
                        .HasName("PK_field_has_style");

                    b.HasIndex("StyleId");

                    b.ToTable("field_has_style");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldHasValidation", b =>
                {
                    b.Property<Guid>("FieldId")
                        .HasColumnName("field_id");

                    b.Property<Guid>("ValidationId")
                        .HasColumnName("validation_id");

                    b.HasKey("FieldId", "ValidationId")
                        .HasName("PK_field_has_validation");

                    b.HasIndex("ValidationId");

                    b.ToTable("field_has_validation");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnName("label")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("field_type");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnName("created");

                    b.Property<bool>("Deprecated")
                        .HasColumnName("deprecated");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsPublic")
                        .HasColumnName("is_public");

                    b.Property<Guid?>("PreviousVersion")
                        .HasColumnName("previous_version");

                    b.Property<bool>("RestrictedAccess")
                        .HasColumnName("restricted_access");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("PreviousVersion");

                    b.ToTable("form");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormHasField", b =>
                {
                    b.Property<Guid>("FormId")
                        .HasColumnName("form_id");

                    b.Property<Guid>("FieldId")
                        .HasColumnName("field_id");

                    b.Property<int>("Position")
                        .HasColumnName("position");

                    b.HasKey("FormId", "FieldId")
                        .HasName("PK_form_has_field");

                    b.HasIndex("FieldId");

                    b.ToTable("form_has_field");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Permission", b =>
                {
                    b.Property<Guid>("Id")
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
                    b.Property<Guid>("Id")
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
                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<Guid>("PermissionId")
                        .HasColumnName("permission_id");

                    b.HasKey("RoleId", "PermissionId")
                        .HasName("PK_role_permission");

                    b.HasIndex("PermissionId");

                    b.ToTable("role_permission");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Style", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("StyleString")
                        .IsRequired()
                        .HasColumnName("style_string")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("style");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasConfig", b =>
                {
                    b.Property<Guid>("ConfigId")
                        .HasColumnName("config_id");

                    b.Property<Guid>("FieldTypeId")
                        .HasColumnName("field_type_id");

                    b.Property<int>("Position")
                        .HasColumnName("position");

                    b.HasKey("ConfigId", "FieldTypeId")
                        .HasName("PK_type_has_config");

                    b.HasIndex("FieldTypeId");

                    b.ToTable("type_has_config");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasStyle", b =>
                {
                    b.Property<Guid>("FieldTypeId")
                        .HasColumnName("field_type_id");

                    b.Property<Guid>("StyleId")
                        .HasColumnName("style_id");

                    b.HasKey("FieldTypeId", "StyleId")
                        .HasName("PK_type_has_style");

                    b.HasIndex("StyleId");

                    b.ToTable("type_has_style");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasValidation", b =>
                {
                    b.Property<Guid>("FieldTypeId")
                        .HasColumnName("field_type_id");

                    b.Property<Guid>("ValidationId")
                        .HasColumnName("validation_id");

                    b.HasKey("FieldTypeId", "ValidationId")
                        .HasName("PK_type_has_validation");

                    b.HasIndex("ValidationId");

                    b.ToTable("type_has_validation");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.UserHasRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("RoleId", "UserId")
                        .HasName("PK_user_has_role");

                    b.HasIndex("UserId");

                    b.ToTable("user_has_role");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Validation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.Property<string>("ValidationString")
                        .IsRequired()
                        .HasColumnName("validation_string")
                        .HasColumnType("varchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("validation");
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
                        .HasForeignKey("PreviousVersion");

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Application")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Assignment", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Application", "Application")
                        .WithMany("Assignment")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Assignment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Attendant", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Conference", "Conference")
                        .WithMany("Attendant")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.AppUser", "User")
                        .WithMany("Attendant")
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

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Field", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.EnumOptionsTable", "EnumOptionsTable")
                        .WithMany("Field")
                        .HasForeignKey("EnumOptionsTableId");

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", "FieldTypeNavigation")
                        .WithMany("Field")
                        .HasForeignKey("FieldType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldHasStyle", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Field", "Field")
                        .WithMany("FieldHasStyle")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Style", "Style")
                        .WithMany("FieldHasStyle")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldHasValidation", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Field", "Field")
                        .WithMany("FieldHasValidation")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Validation", "Validation")
                        .WithMany("FieldHasValidation")
                        .HasForeignKey("ValidationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Form", "PreviousVersionNavigation")
                        .WithMany("InversePreviousVersionNavigation")
                        .HasForeignKey("PreviousVersion");
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.FormHasField", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Field", "Field")
                        .WithMany("FormHasField")
                        .HasForeignKey("FieldId")
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

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasConfig", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Config", "Config")
                        .WithMany("TypeHasConfig")
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", "FieldType")
                        .WithMany("TypeHasConfig")
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasStyle", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", "FieldType")
                        .WithMany("TypeHasStyle")
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Style", "Style")
                        .WithMany("TypeHasStyle")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Reichinger.Masterarbeit.PK_4_0.Database.Models.TypeHasValidation", b =>
                {
                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.FieldType", "FieldType")
                        .WithMany("TypeHasValidation")
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Reichinger.Masterarbeit.PK_4_0.Database.Models.Validation", "Validation")
                        .WithMany("TypeHasValidation")
                        .HasForeignKey("ValidationId")
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
