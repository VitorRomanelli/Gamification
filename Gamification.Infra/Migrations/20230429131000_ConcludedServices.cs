﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamification.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ConcludedServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcludedOrders",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcludedOrders",
                table: "AspNetUsers");
        }
    }
}
