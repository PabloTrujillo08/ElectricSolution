﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "AspNetUsers");
        }
    }
}
