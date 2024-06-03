using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "TariffRates",
                type: "time(7)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "TariffRates",
                type: "time(7)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "TariffRates",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(7)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "TariffRates",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(7)");
        }
    }
}
