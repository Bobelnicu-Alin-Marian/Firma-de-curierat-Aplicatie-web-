using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class CostLivrare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostLivrare",
                table: "Colete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ZonaLivrare",
                table: "Colete",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostLivrare",
                table: "Colete");

            migrationBuilder.DropColumn(
                name: "ZonaLivrare",
                table: "Colete");
        }
    }
}
