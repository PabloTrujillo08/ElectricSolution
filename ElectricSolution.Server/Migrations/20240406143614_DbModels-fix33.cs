using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Provinces_ProvinceIdId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "ProvinceIdId",
                table: "Addresses",
                newName: "ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProvinceIdId",
                table: "Addresses",
                newName: "IX_Addresses_ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Provinces_ProvinceId",
                table: "Addresses",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Provinces_ProvinceId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "Addresses",
                newName: "ProvinceIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Addresses",
                newName: "IX_Addresses_ProvinceIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Provinces_ProvinceIdId",
                table: "Addresses",
                column: "ProvinceIdId",
                principalTable: "Provinces",
                principalColumn: "Id");
        }
    }
}
