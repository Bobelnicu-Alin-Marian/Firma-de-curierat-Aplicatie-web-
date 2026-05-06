using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacte",
                columns: table => new
                {
                    Id_contact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valoare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iconita = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacte", x => x.Id_contact);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacte");
        }
    }
}
