using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TariffHours_TariffRates_TariffRateId",
                table: "TariffHours");

            migrationBuilder.DropIndex(
                name: "IX_TariffHours_TariffRateId",
                table: "TariffHours");

            migrationBuilder.DropColumn(
                name: "TariffRateId",
                table: "TariffHours");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TariffHours",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "TariffHoursId",
                table: "TariffRates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TariffRates_TariffHoursId",
                table: "TariffRates",
                column: "TariffHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_TariffRates_TariffHours_TariffHoursId",
                table: "TariffRates",
                column: "TariffHoursId",
                principalTable: "TariffHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TariffRates_TariffHours_TariffHoursId",
                table: "TariffRates");

            migrationBuilder.DropIndex(
                name: "IX_TariffRates_TariffHoursId",
                table: "TariffRates");

            migrationBuilder.DropColumn(
                name: "TariffHoursId",
                table: "TariffRates");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TariffHours",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "TariffRateId",
                table: "TariffHours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TariffHours_TariffRateId",
                table: "TariffHours",
                column: "TariffRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TariffHours_TariffRates_TariffRateId",
                table: "TariffHours",
                column: "TariffRateId",
                principalTable: "TariffRates",
                principalColumn: "Id");
        }
    }
}
