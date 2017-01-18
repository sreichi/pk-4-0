using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnNameRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "form");

            migrationBuilder.DropColumn(
                name: "text",
                table: "comment");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "form",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "disabled",
                table: "field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "comment",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "form");

            migrationBuilder.DropColumn(
                name: "disabled",
                table: "field");

            migrationBuilder.DropColumn(
                name: "message",
                table: "comment");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "form",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "comment",
                nullable: false,
                defaultValue: "");
        }
    }
}
