using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    active = table.Column<bool>(nullable: true),
                    created = table.Column<DateTime>(nullable: false, defaultValueSql: "('now'::text)::date"),
                    email = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    employee_type = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    firstname = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    ldap_id = table.Column<int>(nullable: false),
                    password = table.Column<string>(type: "bpchar", maxLength: 128, nullable: false),
                    rz_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    salt_string = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conference",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    conference_configuration = table.Column<string>(type: "json", nullable: true),
                    date_of_event = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    end_of_event = table.Column<string>(type: "varchar", maxLength: 5, nullable: false),
                    number_of_conference = table.Column<int>(nullable: false),
                    room_of_event = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    start_of_event = table.Column<string>(type: "varchar", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conference", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "config",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enum_options_table",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    reference_table_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enum_options_table", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "field_type",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    deprecated = table.Column<bool>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    is_public = table.Column<bool>(nullable: false),
                    previous_version = table.Column<Guid>(nullable: true),
                    restricted_access = table.Column<bool>(nullable: false),
                    title = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form", x => x.id);
                    table.ForeignKey(
                        name: "FK_form_form_previous_version",
                        column: x => x.previous_version,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "style",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    style_string = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_style", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "validation",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    validation_string = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attendant",
                columns: table => new
                {
                    conference_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    type_of_attendance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendant", x => new { x.conference_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_attendant_conference_conference_id",
                        column: x => x.conference_id,
                        principalTable: "conference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attendant_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "field",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    content_type = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    disbaled = table.Column<bool>(nullable: true),
                    enum_options_table_id = table.Column<Guid>(nullable: true),
                    field_type = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    multiple_select = table.Column<bool>(nullable: true),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    options = table.Column<string>(type: "json", nullable: true),
                    placeholder = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    required = table.Column<bool>(nullable: true),
                    value = table.Column<string>(type: "varchar", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_enum_options_table_enum_options_table_id",
                        column: x => x.enum_options_table_id,
                        principalTable: "enum_options_table",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_field_field_type_field_type",
                        column: x => x.field_type,
                        principalTable: "field_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_has_config",
                columns: table => new
                {
                    config_id = table.Column<Guid>(nullable: false),
                    field_type_id = table.Column<Guid>(nullable: false),
                    position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_has_config", x => new { x.config_id, x.field_type_id });
                    table.ForeignKey(
                        name: "FK_type_has_config_config_config_id",
                        column: x => x.config_id,
                        principalTable: "config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_type_has_config_field_type_field_type_id",
                        column: x => x.field_type_id,
                        principalTable: "field_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    conference_id = table.Column<Guid>(nullable: true),
                    created = table.Column<DateTime>(nullable: false, defaultValueSql: "('now'::text)::date"),
                    filled_form = table.Column<string>(type: "json", nullable: true),
                    form_id = table.Column<Guid>(nullable: false),
                    is_current = table.Column<bool>(nullable: false),
                    last_modified = table.Column<DateTime>(nullable: false),
                    previous_version = table.Column<Guid>(nullable: true),
                    status_id = table.Column<int>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.id);
                    table.ForeignKey(
                        name: "FK_application_conference_conference_id",
                        column: x => x.conference_id,
                        principalTable: "conference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_form_form_id",
                        column: x => x.form_id,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_application_application_previous_version",
                        column: x => x.previous_version,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    role_id = table.Column<Guid>(nullable: false),
                    permission_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_role_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_has_role",
                columns: table => new
                {
                    role_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_role", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_user_has_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_has_role_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_has_style",
                columns: table => new
                {
                    field_type_id = table.Column<Guid>(nullable: false),
                    style_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_has_style", x => new { x.field_type_id, x.style_id });
                    table.ForeignKey(
                        name: "FK_type_has_style_field_type_field_type_id",
                        column: x => x.field_type_id,
                        principalTable: "field_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_type_has_style_style_style_id",
                        column: x => x.style_id,
                        principalTable: "style",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_has_validation",
                columns: table => new
                {
                    field_type_id = table.Column<Guid>(nullable: false),
                    validation_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_has_validation", x => new { x.field_type_id, x.validation_id });
                    table.ForeignKey(
                        name: "FK_type_has_validation_field_type_field_type_id",
                        column: x => x.field_type_id,
                        principalTable: "field_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_type_has_validation_validation_validation_id",
                        column: x => x.validation_id,
                        principalTable: "validation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "field_has_style",
                columns: table => new
                {
                    field_id = table.Column<Guid>(nullable: false),
                    style_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_has_style", x => new { x.field_id, x.style_id });
                    table.ForeignKey(
                        name: "FK_field_has_style_field_field_id",
                        column: x => x.field_id,
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_has_style_style_style_id",
                        column: x => x.style_id,
                        principalTable: "style",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "field_has_validation",
                columns: table => new
                {
                    field_id = table.Column<Guid>(nullable: false),
                    validation_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_has_validation", x => new { x.field_id, x.validation_id });
                    table.ForeignKey(
                        name: "FK_field_has_validation_field_field_id",
                        column: x => x.field_id,
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_has_validation_validation_validation_id",
                        column: x => x.validation_id,
                        principalTable: "validation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_has_field",
                columns: table => new
                {
                    form_id = table.Column<Guid>(nullable: false),
                    field_id = table.Column<Guid>(nullable: false),
                    position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form_has_field", x => new { x.form_id, x.field_id });
                    table.ForeignKey(
                        name: "FK_form_has_field_field_field_id",
                        column: x => x.field_id,
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_form_has_field_form_form_id",
                        column: x => x.form_id,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                columns: table => new
                {
                    application_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignment", x => new { x.application_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_assignment_application_application_id",
                        column: x => x.application_id,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignment_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    application_id = table.Column<Guid>(nullable: false),
                    created = table.Column<DateTime>(nullable: false, defaultValueSql: "('now'::text)::date"),
                    is_private = table.Column<bool>(nullable: false, defaultValueSql: "false"),
                    message = table.Column<string>(nullable: false),
                    requires_changes = table.Column<bool>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_application_application_id",
                        column: x => x.application_id,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_conference_id",
                table: "application",
                column: "conference_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_form_id",
                table: "application",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_previous_version",
                table: "application",
                column: "previous_version");

            migrationBuilder.CreateIndex(
                name: "IX_application_user_id",
                table: "application",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "app_user_mat_nr_key",
                table: "app_user",
                column: "rz_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assignment_user_id",
                table: "assignment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_attendant_user_id",
                table: "attendant",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_application_id",
                table: "comment",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_enum_options_table_id",
                table: "field",
                column: "enum_options_table_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_field_type",
                table: "field",
                column: "field_type");

            migrationBuilder.CreateIndex(
                name: "IX_field_has_style_style_id",
                table: "field_has_style",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_has_validation_validation_id",
                table: "field_has_validation",
                column: "validation_id");

            migrationBuilder.CreateIndex(
                name: "IX_form_previous_version",
                table: "form",
                column: "previous_version");

            migrationBuilder.CreateIndex(
                name: "IX_form_has_field_field_id",
                table: "form_has_field",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_config_field_type_id",
                table: "type_has_config",
                column: "field_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_style_style_id",
                table: "type_has_style",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_validation_validation_id",
                table: "type_has_validation",
                column: "validation_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_role_user_id",
                table: "user_has_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignment");

            migrationBuilder.DropTable(
                name: "attendant");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "field_has_style");

            migrationBuilder.DropTable(
                name: "field_has_validation");

            migrationBuilder.DropTable(
                name: "form_has_field");

            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "type_has_config");

            migrationBuilder.DropTable(
                name: "type_has_style");

            migrationBuilder.DropTable(
                name: "type_has_validation");

            migrationBuilder.DropTable(
                name: "user_has_role");

            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "field");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "config");

            migrationBuilder.DropTable(
                name: "style");

            migrationBuilder.DropTable(
                name: "validation");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "conference");

            migrationBuilder.DropTable(
                name: "form");

            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "enum_options_table");

            migrationBuilder.DropTable(
                name: "field_type");
        }
    }
}
