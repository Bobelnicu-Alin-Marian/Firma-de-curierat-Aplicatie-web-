using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using FirmaCurierat.Models;

#nullable disable

namespace FirmaCurierat.Migrations
{
    [DbContext(typeof(FirmaCurieratContext))]
    [Migration("20260521120000_FixHubRelationship")]
    public partial class FixHubRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Elimină FK-ul shadow generat prin convenție (HubId_hub → Huburi)
            migrationBuilder.DropForeignKey(
                name: "FK_Comenzi_Huburi_HubId_hub",
                table: "Comenzi");

            // 2. Elimină indexul aferent
            migrationBuilder.DropIndex(
                name: "IX_Comenzi_HubId_hub",
                table: "Comenzi");

            // 3. Elimină coloana shadow – nu mai este folosită
            migrationBuilder.DropColumn(
                name: "HubId_hub",
                table: "Comenzi");

            // 4. Adaugă index pe Id_hub (coloana FK corectă, deja există nullable)
            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_hub",
                table: "Comenzi",
                column: "Id_hub");

            // 5. Adaugă FK corect: Id_hub → Huburi.Id_hub (nullable, Restrict)
            migrationBuilder.AddForeignKey(
                name: "FK_Comenzi_Huburi_Id_hub",
                table: "Comenzi",
                column: "Id_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comenzi_Huburi_Id_hub",
                table: "Comenzi");

            migrationBuilder.DropIndex(
                name: "IX_Comenzi_Id_hub",
                table: "Comenzi");

            migrationBuilder.AddColumn<int>(
                name: "HubId_hub",
                table: "Comenzi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_HubId_hub",
                table: "Comenzi",
                column: "HubId_hub");

            migrationBuilder.AddForeignKey(
                name: "FK_Comenzi_Huburi_HubId_hub",
                table: "Comenzi",
                column: "HubId_hub",
                principalTable: "Huburi",
                principalColumn: "Id_hub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
