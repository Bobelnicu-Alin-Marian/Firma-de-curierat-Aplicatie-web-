using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirmaCurierat.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Angajati",
                columns: table => new
                {
                    Id_angajat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Categorie_permis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departament = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajati", x => x.Id_angajat);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id_client = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id_client);
                });

            migrationBuilder.CreateTable(
                name: "Vehicule",
                columns: table => new
                {
                    Id_vehicul = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicule", x => x.Id_vehicul);
                });

            migrationBuilder.CreateTable(
                name: "Adrese",
                columns: table => new
                {
                    Id_adresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Judet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localitate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_client = table.Column<int>(type: "int", nullable: true),
                    ClientId_client = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adrese", x => x.Id_adresa);
                    table.ForeignKey(
                        name: "FK_Adrese_Clienti_ClientId_client",
                        column: x => x.ClientId_client,
                        principalTable: "Clienti",
                        principalColumn: "Id_client",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conduceri",
                columns: table => new
                {
                    Id_conduce = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_curier = table.Column<int>(type: "int", nullable: false),
                    Id_vehicul = table.Column<int>(type: "int", nullable: false),
                    VehiculId_vehicul = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conduceri", x => x.Id_conduce);
                    table.ForeignKey(
                        name: "FK_Conduceri_Angajati_Id_curier",
                        column: x => x.Id_curier,
                        principalTable: "Angajati",
                        principalColumn: "Id_angajat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conduceri_Vehicule_VehiculId_vehicul",
                        column: x => x.VehiculId_vehicul,
                        principalTable: "Vehicule",
                        principalColumn: "Id_vehicul",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Huburi",
                columns: table => new
                {
                    Id_hub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacitate = table.Column<int>(type: "int", nullable: false),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_adresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huburi", x => x.Id_hub);
                    table.ForeignKey(
                        name: "FK_Huburi_Adrese_Id_adresa",
                        column: x => x.Id_adresa,
                        principalTable: "Adrese",
                        principalColumn: "Id_adresa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comenzi",
                columns: table => new
                {
                    Id_comanda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_expeditor = table.Column<int>(type: "int", nullable: false),
                    Id_destinatar = table.Column<int>(type: "int", nullable: false),
                    Id_adresa_ridicare = table.Column<int>(type: "int", nullable: false),
                    Id_adresa_livrare = table.Column<int>(type: "int", nullable: false),
                    Id_operator = table.Column<int>(type: "int", nullable: true),
                    Id_curier = table.Column<int>(type: "int", nullable: true),
                    Id_hub = table.Column<int>(type: "int", nullable: true),
                    HubId_hub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comenzi", x => x.Id_comanda);
                    table.ForeignKey(
                        name: "FK_Comenzi_Adrese_Id_adresa_livrare",
                        column: x => x.Id_adresa_livrare,
                        principalTable: "Adrese",
                        principalColumn: "Id_adresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Adrese_Id_adresa_ridicare",
                        column: x => x.Id_adresa_ridicare,
                        principalTable: "Adrese",
                        principalColumn: "Id_adresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Angajati_Id_curier",
                        column: x => x.Id_curier,
                        principalTable: "Angajati",
                        principalColumn: "Id_angajat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Angajati_Id_operator",
                        column: x => x.Id_operator,
                        principalTable: "Angajati",
                        principalColumn: "Id_angajat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Clienti_Id_destinatar",
                        column: x => x.Id_destinatar,
                        principalTable: "Clienti",
                        principalColumn: "Id_client",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Clienti_Id_expeditor",
                        column: x => x.Id_expeditor,
                        principalTable: "Clienti",
                        principalColumn: "Id_client",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comenzi_Huburi_HubId_hub",
                        column: x => x.HubId_hub,
                        principalTable: "Huburi",
                        principalColumn: "Id_hub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colete",
                columns: table => new
                {
                    Id_colet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Awb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Greutate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dimensiune = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_comanda = table.Column<int>(type: "int", nullable: false),
                    ComandaId_comanda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colete", x => x.Id_colet);
                    table.ForeignKey(
                        name: "FK_Colete_Comenzi_ComandaId_comanda",
                        column: x => x.ComandaId_comanda,
                        principalTable: "Comenzi",
                        principalColumn: "Id_comanda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturi",
                columns: table => new
                {
                    Id_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valoare = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Id_client = table.Column<int>(type: "int", nullable: false),
                    ClientId_client = table.Column<int>(type: "int", nullable: false),
                    Id_comanda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturi", x => x.Id_factura);
                    table.ForeignKey(
                        name: "FK_Facturi_Clienti_ClientId_client",
                        column: x => x.ClientId_client,
                        principalTable: "Clienti",
                        principalColumn: "Id_client",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturi_Comenzi_Id_comanda",
                        column: x => x.Id_comanda,
                        principalTable: "Comenzi",
                        principalColumn: "Id_comanda",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusuriLivrare",
                columns: table => new
                {
                    Id_status = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_colet = table.Column<int>(type: "int", nullable: false),
                    ColetId_colet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusuriLivrare", x => x.Id_status);
                    table.ForeignKey(
                        name: "FK_StatusuriLivrare_Colete_ColetId_colet",
                        column: x => x.ColetId_colet,
                        principalTable: "Colete",
                        principalColumn: "Id_colet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tranzitari",
                columns: table => new
                {
                    Id_tranziteaza = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_hub = table.Column<int>(type: "int", nullable: false),
                    Id_colet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tranzitari", x => x.Id_tranziteaza);
                    table.ForeignKey(
                        name: "FK_Tranzitari_Colete_Id_colet",
                        column: x => x.Id_colet,
                        principalTable: "Colete",
                        principalColumn: "Id_colet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tranzitari_Huburi_Id_hub",
                        column: x => x.Id_hub,
                        principalTable: "Huburi",
                        principalColumn: "Id_hub",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_ClientId_client",
                table: "Adrese",
                column: "ClientId_client");

            migrationBuilder.CreateIndex(
                name: "IX_Colete_ComandaId_comanda",
                table: "Colete",
                column: "ComandaId_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_HubId_hub",
                table: "Comenzi",
                column: "HubId_hub");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_adresa_livrare",
                table: "Comenzi",
                column: "Id_adresa_livrare");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_adresa_ridicare",
                table: "Comenzi",
                column: "Id_adresa_ridicare");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_curier",
                table: "Comenzi",
                column: "Id_curier");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_destinatar",
                table: "Comenzi",
                column: "Id_destinatar");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_expeditor",
                table: "Comenzi",
                column: "Id_expeditor");

            migrationBuilder.CreateIndex(
                name: "IX_Comenzi_Id_operator",
                table: "Comenzi",
                column: "Id_operator");

            migrationBuilder.CreateIndex(
                name: "IX_Conduceri_Id_curier",
                table: "Conduceri",
                column: "Id_curier");

            migrationBuilder.CreateIndex(
                name: "IX_Conduceri_VehiculId_vehicul",
                table: "Conduceri",
                column: "VehiculId_vehicul");

            migrationBuilder.CreateIndex(
                name: "IX_Facturi_ClientId_client",
                table: "Facturi",
                column: "ClientId_client");

            migrationBuilder.CreateIndex(
                name: "IX_Facturi_Id_comanda",
                table: "Facturi",
                column: "Id_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_Huburi_Id_adresa",
                table: "Huburi",
                column: "Id_adresa");

            migrationBuilder.CreateIndex(
                name: "IX_StatusuriLivrare_ColetId_colet",
                table: "StatusuriLivrare",
                column: "ColetId_colet");

            migrationBuilder.CreateIndex(
                name: "IX_Tranzitari_Id_colet",
                table: "Tranzitari",
                column: "Id_colet");

            migrationBuilder.CreateIndex(
                name: "IX_Tranzitari_Id_hub",
                table: "Tranzitari",
                column: "Id_hub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conduceri");

            migrationBuilder.DropTable(
                name: "Facturi");

            migrationBuilder.DropTable(
                name: "StatusuriLivrare");

            migrationBuilder.DropTable(
                name: "Tranzitari");

            migrationBuilder.DropTable(
                name: "Vehicule");

            migrationBuilder.DropTable(
                name: "Colete");

            migrationBuilder.DropTable(
                name: "Comenzi");

            migrationBuilder.DropTable(
                name: "Angajati");

            migrationBuilder.DropTable(
                name: "Huburi");

            migrationBuilder.DropTable(
                name: "Adrese");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
