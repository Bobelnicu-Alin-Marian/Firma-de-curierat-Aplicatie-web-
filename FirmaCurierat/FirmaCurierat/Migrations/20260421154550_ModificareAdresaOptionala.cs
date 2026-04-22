using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class ModificareAdresaOptionala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colete_Comenzi_ComandaId_comanda",
                table: "Colete");

            migrationBuilder.AlterColumn<int>(
                name: "Id_adresa",
                table: "Huburi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id_comanda",
                table: "Colete",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Dimensiune",
                table: "Colete",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ComandaId_comanda",
                table: "Colete",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Colete_Comenzi_ComandaId_comanda",
                table: "Colete",
                column: "ComandaId_comanda",
                principalTable: "Comenzi",
                principalColumn: "Id_comanda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colete_Comenzi_ComandaId_comanda",
                table: "Colete");

            migrationBuilder.AlterColumn<int>(
                name: "Id_adresa",
                table: "Huburi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id_comanda",
                table: "Colete",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensiune",
                table: "Colete",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComandaId_comanda",
                table: "Colete",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Colete_Comenzi_ComandaId_comanda",
                table: "Colete",
                column: "ComandaId_comanda",
                principalTable: "Comenzi",
                principalColumn: "Id_comanda",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
