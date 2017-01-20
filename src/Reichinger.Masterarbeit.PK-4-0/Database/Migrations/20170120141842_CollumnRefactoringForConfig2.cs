using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnRefactoringForConfig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position",
                table: "config");

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "type_has_config",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position",
                table: "type_has_config");

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "config",
                nullable: false,
                defaultValue: 0);
        }
    }
}
