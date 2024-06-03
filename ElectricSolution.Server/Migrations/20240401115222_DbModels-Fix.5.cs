using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsFix5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInstallations_AspNetUsers_ClientId1",
                table: "ClientInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_ClientId1",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ClientId1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ClientId1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ClientId1",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_ClientInstallations_ClientId1",
                table: "ClientInstallations");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "ClientInstallations");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ClientInstallations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ClientId",
                table: "Notifications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInstallations_ClientId",
                table: "ClientInstallations",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInstallations_AspNetUsers_ClientId",
                table: "ClientInstallations",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ClientId",
                table: "Notifications",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInstallations_AspNetUsers_ClientId",
                table: "ClientInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ClientId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ClientId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_ClientInstallations_ClientId",
                table: "ClientInstallations");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ClientInstallations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "ClientInstallations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ClientId1",
                table: "Notifications",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId1",
                table: "Contracts",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInstallations_ClientId1",
                table: "ClientInstallations",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInstallations_AspNetUsers_ClientId1",
                table: "ClientInstallations",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_ClientId1",
                table: "Contracts",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ClientId1",
                table: "Notifications",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
