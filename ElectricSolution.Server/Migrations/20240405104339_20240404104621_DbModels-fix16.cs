using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class _20240404104621_DbModelsfix16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TariffHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TariffRateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffHours_TariffRates_TariffRateId",
                        column: x => x.TariffRateId,
                        principalTable: "TariffRates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffHours_TariffRateId",
                table: "TariffHours",
                column: "TariffRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TariffHours");
        }
    }
}
