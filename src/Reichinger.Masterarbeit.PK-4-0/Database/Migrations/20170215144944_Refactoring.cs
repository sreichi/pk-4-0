using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendand");

            migrationBuilder.CreateTable(
                name: "attendant",
                columns: table => new
                {
                    conference_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_attendant_user_id",
                table: "attendant",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendant");

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
    }
}
