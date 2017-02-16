using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "disbaled",
                table: "field");

            migrationBuilder.RenameColumn(
                name: "disabled",
                table: "field",
                newName: "disbaled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "disbaled",
                table: "field",
                newName: "disabled");

            migrationBuilder.AddColumn<bool>(
                name: "disbaled",
                table: "field",
                nullable: true);
        }
    }
}
