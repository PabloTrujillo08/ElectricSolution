using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsFix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ClientId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ClientId",
                table: "Addresses",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId",
                table: "Addresses",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ClientId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ClientId1",
                table: "Addresses",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId1",
                table: "Addresses",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
