using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class CollumnAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "rz_name",
                table: "app_user",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employee_type",
                table: "app_user",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee_type",
                table: "app_user");

            migrationBuilder.AlterColumn<string>(
                name: "rz_name",
                table: "app_user",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);
        }
    }
}
