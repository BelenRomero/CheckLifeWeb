using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckLifeWeb.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarioVacuna",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarioVacuna", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nacionalidad",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nacionalidad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 8, nullable: true),
                    RolID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Login_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Centro",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    CUIT = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(maxLength: 50, nullable: true),
                    Localidad = table.Column<string>(maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LoginID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centro", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Centro_Login_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Login",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    NacionalidadID = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LoginID = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Matricula = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medico_Login_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Login",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medico_Nacionalidad_NacionalidadID",
                        column: x => x.NacionalidadID,
                        principalTable: "Nacionalidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(maxLength: 50, nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    NacionalidadID = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LoginID = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    MedicoCabeceraID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Paciente_Login_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Login",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paciente_Medico_MedicoCabeceraID",
                        column: x => x.MedicoCabeceraID,
                        principalTable: "Medico",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paciente_Nacionalidad_NacionalidadID",
                        column: x => x.NacionalidadID,
                        principalTable: "Nacionalidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CVacunaID = table.Column<int>(nullable: true),
                    FechaAplicada = table.Column<DateTime>(nullable: false),
                    MarcaComercialLote = table.Column<string>(nullable: true),
                    SelloFirma = table.Column<string>(nullable: true),
                    MatriculaEnfermero = table.Column<int>(nullable: false),
                    PacienteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vacunas_CalendarioVacuna_CVacunaID",
                        column: x => x.CVacunaID,
                        principalTable: "CalendarioVacuna",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacunas_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Centro_LoginID",
                table: "Centro",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_Login_RolID",
                table: "Login",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_LoginID",
                table: "Medico",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_NacionalidadID",
                table: "Medico",
                column: "NacionalidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_LoginID",
                table: "Paciente",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_MedicoCabeceraID",
                table: "Paciente",
                column: "MedicoCabeceraID");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_NacionalidadID",
                table: "Paciente",
                column: "NacionalidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_CVacunaID",
                table: "Vacunas",
                column: "CVacunaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_PacienteID",
                table: "Vacunas",
                column: "PacienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Centro");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "CalendarioVacuna");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Nacionalidad");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
