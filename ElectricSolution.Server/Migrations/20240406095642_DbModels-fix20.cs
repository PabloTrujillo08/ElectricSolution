using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ClientId",
                table: "Addresses",
                newName: "IX_Addresses_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AddressId",
                table: "Addresses",
                column: "AddressId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AddressId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AddressId",
                table: "Addresses",
                newName: "IX_Addresses_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId",
                table: "Addresses",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
