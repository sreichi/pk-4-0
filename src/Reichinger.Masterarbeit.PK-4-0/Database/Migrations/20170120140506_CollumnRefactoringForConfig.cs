using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnRefactoringForConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "config");

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "config",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position",
                table: "config");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "config",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
