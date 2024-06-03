using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Battery",
                table: "SolarEnergies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SolarBatteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarBatteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolarEnergyBatteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolarEnergyId = table.Column<int>(type: "int", nullable: false),
                    SolarBatteryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarEnergyBatteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolarEnergyBatteries_SolarBatteries_SolarBatteryId",
                        column: x => x.SolarBatteryId,
                        principalTable: "SolarBatteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolarEnergyBatteries_SolarEnergies_SolarEnergyId",
                        column: x => x.SolarEnergyId,
                        principalTable: "SolarEnergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolarEnergyBatteries_SolarBatteryId",
                table: "SolarEnergyBatteries",
                column: "SolarBatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarEnergyBatteries_SolarEnergyId",
                table: "SolarEnergyBatteries",
                column: "SolarEnergyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolarEnergyBatteries");

            migrationBuilder.DropTable(
                name: "SolarBatteries");

            migrationBuilder.DropColumn(
                name: "Battery",
                table: "SolarEnergies");
        }
    }
}
