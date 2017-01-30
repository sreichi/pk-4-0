using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reichinger.Masterarbeit.PK40.Database.Migrations
{
    public partial class userCollumNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "app_user_mat_nr_key",
                table: "app_user");

            migrationBuilder.DropColumn(
                name: "mat_nr",
                table: "app_user");

            migrationBuilder.AddColumn<string>(
                name: "rz_name",
                table: "app_user",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "app_user_mat_nr_key",
                table: "app_user",
                column: "rz_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "app_user_mat_nr_key",
                table: "app_user");

            migrationBuilder.DropColumn(
                name: "rz_name",
                table: "app_user");

            migrationBuilder.AddColumn<int>(
                name: "mat_nr",
                table: "app_user",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "app_user_mat_nr_key",
                table: "app_user",
                column: "mat_nr",
                unique: true);
        }
    }
}
