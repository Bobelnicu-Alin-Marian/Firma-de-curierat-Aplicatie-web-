using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using FirmaCurierat.Models;

#nullable disable

namespace FirmaCurierat.Migrations
{
    [DbContext(typeof(FirmaCurieratContext))]
    [Migration("20260521130000_AddApplicationUserIdToAdrese")]
    public partial class AddApplicationUserIdToAdrese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Adrese",
                type: "nvarchar(450)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Adrese");
        }
    }
}
