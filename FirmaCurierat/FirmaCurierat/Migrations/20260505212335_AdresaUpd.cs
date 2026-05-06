using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class AdresaUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HubId_hub",
                table: "Adrese",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_hub",
                table: "Adrese",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strada",
                table: "Adrese",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_HubId_hub",
                table: "Adrese",
                column: "HubId_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Huburi_HubId_hub",
                table: "Adrese",
                column: "HubId_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Huburi_HubId_hub",
                table: "Adrese");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_HubId_hub",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "HubId_hub",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "Id_hub",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "Strada",
                table: "Adrese");
        }
    }
}
