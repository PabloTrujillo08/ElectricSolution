using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsfix37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VAT",
                table: "AspNetUsers");
        }
    }
}
