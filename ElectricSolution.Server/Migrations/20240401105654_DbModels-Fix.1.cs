using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressClient");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_ClientId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ClientId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Addresses");

            migrationBuilder.CreateTable(
                name: "AddressClient",
                columns: table => new
                {
                    AddressesId = table.Column<int>(type: "int", nullable: false),
                    ClientsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressClient", x => new { x.AddressesId, x.ClientsId });
                    table.ForeignKey(
                        name: "FK_AddressClient_Addresses_AddressesId",
                        column: x => x.AddressesId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressClient_AspNetUsers_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressClient_ClientsId",
                table: "AddressClient",
                column: "ClientsId");
        }
    }
}
