using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnRefactoringForFieldTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "field_type");

            migrationBuilder.DropColumn(
                name: "name",
                table: "field_type");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "validation",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "style",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "label",
                table: "field_type",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "field_type",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "validation");

            migrationBuilder.DropColumn(
                name: "description",
                table: "style");

            migrationBuilder.DropColumn(
                name: "label",
                table: "field_type");

            migrationBuilder.DropColumn(
                name: "value",
                table: "field_type");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "field_type",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "field_type",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
