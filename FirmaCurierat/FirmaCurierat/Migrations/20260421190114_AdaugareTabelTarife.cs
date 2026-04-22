using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareTabelTarife : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_tarif",
                table: "Colete",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifId_tarif",
                table: "Colete",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tarife",
                columns: table => new
                {
                    Id_tarif = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieGreutate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PretNational = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PretInternational = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarife", x => x.Id_tarif);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colete_TarifId_tarif",
                table: "Colete",
                column: "TarifId_tarif");

            migrationBuilder.AddForeignKey(
                name: "FK_Colete_Tarife_TarifId_tarif",
                table: "Colete",
                column: "TarifId_tarif",
                principalTable: "Tarife",
                principalColumn: "Id_tarif");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colete_Tarife_TarifId_tarif",
                table: "Colete");

            migrationBuilder.DropTable(
                name: "Tarife");

            migrationBuilder.DropIndex(
                name: "IX_Colete_TarifId_tarif",
                table: "Colete");

            migrationBuilder.DropColumn(
                name: "Id_tarif",
                table: "Colete");

            migrationBuilder.DropColumn(
                name: "TarifId_tarif",
                table: "Colete");
        }
    }
}
