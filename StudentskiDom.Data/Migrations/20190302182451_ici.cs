using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentskiDom.Data.Migrations
{
    public partial class ici : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fakulteti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakulteti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalozi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalozi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pojsjetioci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pojsjetioci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CijenaPoSatu = table.Column<double>(nullable: false),
                    Kapacitet = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoviSoba",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviSoba", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoviUplata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviUplata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteZaposlenika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IznosSatnice = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteZaposlenika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    _DrzavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradovi_Drzave__DrzavaId",
                        column: x => x._DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sobe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojKreveta = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Sprat = table.Column<int>(nullable: false),
                    _TipSobeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sobe_TipoviSoba__TipSobeId",
                        column: x => x._TipSobeId,
                        principalTable: "TipoviSoba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Ime = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    KorisnickiNalogId = table.Column<int>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Spol = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    _FakultetId = table.Column<int>(nullable: true),
                    _GradId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studenti_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studenti_Fakulteti__FakultetId",
                        column: x => x._FakultetId,
                        principalTable: "Fakulteti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studenti_Gradovi__GradId",
                        column: x => x._GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    KorisnickiNalogId = table.Column<int>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    _GradId = table.Column<int>(nullable: true),
                    _VrstaZaposlenikaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_KorisnickiNalozi_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalozi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_Gradovi__GradId",
                        column: x => x._GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenici_VrsteZaposlenika__VrstaZaposlenikaId",
                        column: x => x._VrstaZaposlenikaId,
                        principalTable: "VrsteZaposlenika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    Naslov = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    _ZaposlenikId = table.Column<int>(nullable: false),
                    procitana = table.Column<bool>(nullable: false),
                    samoZaposlenicima = table.Column<bool>(nullable: false),
                    zaSve = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obavijesti_Zaposlenici__ZaposlenikId",
                        column: x => x._ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obracun",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojNeradnihDana = table.Column<int>(nullable: false),
                    Godina = table.Column<string>(nullable: true),
                    Mjesec = table.Column<string>(nullable: true),
                    Satnica = table.Column<int>(nullable: false),
                    Ukupno = table.Column<int>(nullable: false),
                    VrstaZaposlenikaId = table.Column<int>(nullable: true),
                    _ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obracun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obracun_Zaposlenici__ZaposlenikId",
                        column: x => x._ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijeSale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojSati = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    UkupnaCijena = table.Column<double>(nullable: false),
                    _PosjetilacId = table.Column<int>(nullable: false),
                    _SalaId = table.Column<int>(nullable: false),
                    _ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijeSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezervacijeSale_Pojsjetioci__PosjetilacId",
                        column: x => x._PosjetilacId,
                        principalTable: "Pojsjetioci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezervacijeSale_Sale__SalaId",
                        column: x => x._SalaId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezervacijeSale_Zaposlenici__ZaposlenikId",
                        column: x => x._ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentiSobe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumDodjele = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    _SobaId = table.Column<int>(nullable: false),
                    _StudentId = table.Column<int>(nullable: false),
                    _ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentiSobe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentiSobe_Sobe__SobaId",
                        column: x => x._SobaId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentiSobe_Studenti__StudentId",
                        column: x => x._StudentId,
                        principalTable: "Studenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentiSobe_Zaposlenici__ZaposlenikId",
                        column: x => x._ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    Iznos = table.Column<double>(nullable: false),
                    _StudentId = table.Column<int>(nullable: false),
                    _TipUplateId = table.Column<int>(nullable: false),
                    _ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplate_Studenti__StudentId",
                        column: x => x._StudentId,
                        principalTable: "Studenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_TipoviUplata__TipUplateId",
                        column: x => x._TipUplateId,
                        principalTable: "TipoviUplata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_Zaposlenici__ZaposlenikId",
                        column: x => x._ZaposlenikId,
                        principalTable: "Zaposlenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi__DrzavaId",
                table: "Gradovi",
                column: "_DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti__ZaposlenikId",
                table: "Obavijesti",
                column: "_ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Obracun__ZaposlenikId",
                table: "Obracun",
                column: "_ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeSale__PosjetilacId",
                table: "RezervacijeSale",
                column: "_PosjetilacId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeSale__SalaId",
                table: "RezervacijeSale",
                column: "_SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeSale__ZaposlenikId",
                table: "RezervacijeSale",
                column: "_ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Sobe__TipSobeId",
                table: "Sobe",
                column: "_TipSobeId");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_KorisnickiNalogId",
                table: "Studenti",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti__FakultetId",
                table: "Studenti",
                column: "_FakultetId");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti__GradId",
                table: "Studenti",
                column: "_GradId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiSobe__SobaId",
                table: "StudentiSobe",
                column: "_SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiSobe__StudentId",
                table: "StudentiSobe",
                column: "_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiSobe__ZaposlenikId",
                table: "StudentiSobe",
                column: "_ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate__StudentId",
                table: "Uplate",
                column: "_StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate__TipUplateId",
                table: "Uplate",
                column: "_TipUplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate__ZaposlenikId",
                table: "Uplate",
                column: "_ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici_KorisnickiNalogId",
                table: "Zaposlenici",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici__GradId",
                table: "Zaposlenici",
                column: "_GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenici__VrstaZaposlenikaId",
                table: "Zaposlenici",
                column: "_VrstaZaposlenikaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "Obracun");

            migrationBuilder.DropTable(
                name: "RezervacijeSale");

            migrationBuilder.DropTable(
                name: "StudentiSobe");

            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "Pojsjetioci");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Sobe");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "TipoviUplata");

            migrationBuilder.DropTable(
                name: "Zaposlenici");

            migrationBuilder.DropTable(
                name: "TipoviSoba");

            migrationBuilder.DropTable(
                name: "Fakulteti");

            migrationBuilder.DropTable(
                name: "KorisnickiNalozi");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "VrsteZaposlenika");

            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
