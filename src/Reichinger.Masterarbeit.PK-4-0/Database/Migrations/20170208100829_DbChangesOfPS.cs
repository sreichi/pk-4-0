using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class DbChangesOfPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_has_role_role_id",
                table: "user_has_role");

            migrationBuilder.DropIndex(
                name: "IX_type_has_validation_field_type_id",
                table: "type_has_validation");

            migrationBuilder.DropIndex(
                name: "IX_type_has_style_field_type_id",
                table: "type_has_style");

            migrationBuilder.DropIndex(
                name: "IX_type_has_config_config_id",
                table: "type_has_config");

            migrationBuilder.DropIndex(
                name: "IX_role_permission_role_id",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "IX_form_has_field_form_id",
                table: "form_has_field");

            migrationBuilder.DropIndex(
                name: "IX_field_has_validation_field_id",
                table: "field_has_validation");

            migrationBuilder.DropIndex(
                name: "IX_field_has_style_field_id",
                table: "field_has_style");

            migrationBuilder.DropIndex(
                name: "IX_assignment_application_id",
                table: "assignment");

            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "form",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "conference",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_of_event",
                table: "conference",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "number_of_conference",
                table: "conference",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "room_of_event",
                table: "conference",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "start_of_event",
                table: "conference",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "attendand",
                columns: table => new
                {
                    conference_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendand", x => new { x.conference_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_attendand_conference_conference_id",
                        column: x => x.conference_id,
                        principalTable: "conference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attendand_app_user_user_id",
                        column: x => x.user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendand_user_id",
                table: "attendand",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendand");

            migrationBuilder.DropColumn(
                name: "created",
                table: "form");

            migrationBuilder.DropColumn(
                name: "end_of_event",
                table: "conference");

            migrationBuilder.DropColumn(
                name: "number_of_conference",
                table: "conference");

            migrationBuilder.DropColumn(
                name: "room_of_event",
                table: "conference");

            migrationBuilder.DropColumn(
                name: "start_of_event",
                table: "conference");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "conference",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_user_has_role_role_id",
                table: "user_has_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_validation_field_type_id",
                table: "type_has_validation",
                column: "field_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_style_field_type_id",
                table: "type_has_style",
                column: "field_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_type_has_config_config_id",
                table: "type_has_config",
                column: "config_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_form_has_field_form_id",
                table: "form_has_field",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_has_validation_field_id",
                table: "field_has_validation",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_has_style_field_id",
                table: "field_has_style",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_assignment_application_id",
                table: "assignment",
                column: "application_id");
        }
    }
}
