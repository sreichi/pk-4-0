using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "form_has_field",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "conference_configuration",
                table: "conference",
                type: "json",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "position",
                table: "form_has_field");

            migrationBuilder.DropColumn(
                name: "conference_configuration",
                table: "conference");
        }
    }
}
