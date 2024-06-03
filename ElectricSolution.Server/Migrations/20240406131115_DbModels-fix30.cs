using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContractId",
                table: "Notifications",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Contracts_ContractId",
                table: "Notifications",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Contracts_ContractId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ContractId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Notifications");
        }
    }
}
