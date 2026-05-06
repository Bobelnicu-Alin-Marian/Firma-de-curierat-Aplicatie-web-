using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class Statusuri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusuriLivrare",
                columns: table => new
                {
                    Id_status = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_actualizare = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_colet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusuriLivrare", x => x.Id_status);
                    table.ForeignKey(
                        name: "FK_StatusuriLivrare_Colete_Id_colet",
                        column: x => x.Id_colet,
                        principalTable: "Colete",
                        principalColumn: "Id_colet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusuriLivrare_Id_colet",
                table: "StatusuriLivrare",
                column: "Id_colet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
