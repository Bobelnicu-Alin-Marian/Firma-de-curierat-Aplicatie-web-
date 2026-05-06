using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class ctcupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iconita",
                table: "Contacte");

            migrationBuilder.RenameColumn(
                name: "Titlu",
                table: "Contacte",
                newName: "Metoda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Metoda",
                table: "Contacte",
                newName: "Titlu");

            migrationBuilder.AddColumn<string>(
                name: "Iconita",
                table: "Contacte",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
