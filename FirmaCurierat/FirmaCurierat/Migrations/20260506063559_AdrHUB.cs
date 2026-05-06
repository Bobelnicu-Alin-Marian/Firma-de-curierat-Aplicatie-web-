using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class AdrHUB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Huburi_Id_hub",
                table: "Adrese");

            migrationBuilder.DropForeignKey(
                name: "FK_Huburi_Adrese_Id_adresa",
                table: "Huburi");

            migrationBuilder.DropIndex(
                name: "IX_Huburi_Id_adresa",
                table: "Huburi");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_Id_hub",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "Id_adresa",
                table: "Huburi");

            migrationBuilder.DropColumn(
                name: "Id_hub",
                table: "Adrese");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_adresa",
                table: "Huburi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_hub",
                table: "Adrese",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Huburi_Id_adresa",
                table: "Huburi",
                column: "Id_adresa");

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_Id_hub",
                table: "Adrese",
                column: "Id_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Huburi_Id_hub",
                table: "Adrese",
                column: "Id_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Huburi_Adrese_Id_adresa",
                table: "Huburi",
                column: "Id_adresa",
                principalTable: "Adrese",
                principalColumn: "Id_adresa",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
