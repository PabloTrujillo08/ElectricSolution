using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_ContractId",
                table: "Installations");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "FarmName",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_Installations_ContractId",
                table: "Installations",
                column: "ContractId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Installations_ContractId",
                table: "Installations");

            migrationBuilder.AddColumn<string>(
                name: "Certificate",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmName",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installations_ContractId",
                table: "Installations",
                column: "ContractId");
        }
    }
}
