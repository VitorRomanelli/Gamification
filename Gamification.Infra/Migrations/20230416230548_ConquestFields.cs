using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamification.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConquestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Conquests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Conquests",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicesConcludedCount",
                table: "Conquests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "To",
                table: "Conquests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Conquests");

            migrationBuilder.DropColumn(
                name: "ServicesConcludedCount",
                table: "Conquests");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Conquests");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Conquests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
