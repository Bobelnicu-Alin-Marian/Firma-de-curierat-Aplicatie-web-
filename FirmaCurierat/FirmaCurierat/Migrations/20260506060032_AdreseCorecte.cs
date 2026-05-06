using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class AdreseCorecte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Clienti_ClientId_client",
                table: "Adrese");

            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Huburi_HubId_hub",
                table: "Adrese");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_ClientId_client",
                table: "Adrese");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_HubId_hub",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "ClientId_client",
                table: "Adrese");

            migrationBuilder.DropColumn(
                name: "HubId_hub",
                table: "Adrese");

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_Id_client",
                table: "Adrese",
                column: "Id_client");

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_Id_hub",
                table: "Adrese",
                column: "Id_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Clienti_Id_client",
                table: "Adrese",
                column: "Id_client",
                principalTable: "Clienti",
                principalColumn: "Id_client");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Huburi_Id_hub",
                table: "Adrese",
                column: "Id_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Clienti_Id_client",
                table: "Adrese");

            migrationBuilder.DropForeignKey(
                name: "FK_Adrese_Huburi_Id_hub",
                table: "Adrese");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_Id_client",
                table: "Adrese");

            migrationBuilder.DropIndex(
                name: "IX_Adrese_Id_hub",
                table: "Adrese");

            migrationBuilder.AddColumn<int>(
                name: "ClientId_client",
                table: "Adrese",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HubId_hub",
                table: "Adrese",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_ClientId_client",
                table: "Adrese",
                column: "ClientId_client");

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_HubId_hub",
                table: "Adrese",
                column: "HubId_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Clienti_ClientId_client",
                table: "Adrese",
                column: "ClientId_client",
                principalTable: "Clienti",
                principalColumn: "Id_client",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adrese_Huburi_HubId_hub",
                table: "Adrese",
                column: "HubId_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
