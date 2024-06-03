using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientSolarEnergies");

            migrationBuilder.AddColumn<bool>(
                name: "Injection",
                table: "SolarEnergies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InstallationSolarEnergy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallationId = table.Column<int>(type: "int", nullable: false),
                    SolarEnergyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationSolarEnergy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallationSolarEnergy_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallationSolarEnergy_SolarEnergies_SolarEnergyId",
                        column: x => x.SolarEnergyId,
                        principalTable: "SolarEnergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallationSolarEnergy_InstallationId",
                table: "InstallationSolarEnergy",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationSolarEnergy_SolarEnergyId",
                table: "InstallationSolarEnergy",
                column: "SolarEnergyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallationSolarEnergy");

            migrationBuilder.DropColumn(
                name: "Injection",
                table: "SolarEnergies");

            migrationBuilder.CreateTable(
                name: "ClientSolarEnergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SolarEnergyId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSolarEnergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSolarEnergies_AspNetUsers_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientSolarEnergies_SolarEnergies_SolarEnergyId",
                        column: x => x.SolarEnergyId,
                        principalTable: "SolarEnergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientSolarEnergies_ClientId1",
                table: "ClientSolarEnergies",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSolarEnergies_SolarEnergyId",
                table: "ClientSolarEnergies",
                column: "SolarEnergyId");
        }
    }
}
