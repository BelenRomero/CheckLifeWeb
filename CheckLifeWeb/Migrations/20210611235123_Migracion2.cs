using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckLifeWeb.Migrations
{
    public partial class Migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunas_CalendarioVacuna_CVacunaID",
                table: "Vacunas");

            migrationBuilder.DropIndex(
                name: "IX_Vacunas_CVacunaID",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "CVacunaID",
                table: "Vacunas");

            migrationBuilder.AddColumn<int>(
                name: "CalendarioVacunaID",
                table: "Vacunas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_CalendarioVacunaID",
                table: "Vacunas",
                column: "CalendarioVacunaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunas_CalendarioVacuna_CalendarioVacunaID",
                table: "Vacunas",
                column: "CalendarioVacunaID",
                principalTable: "CalendarioVacuna",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunas_CalendarioVacuna_CalendarioVacunaID",
                table: "Vacunas");

            migrationBuilder.DropIndex(
                name: "IX_Vacunas_CalendarioVacunaID",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "CalendarioVacunaID",
                table: "Vacunas");

            migrationBuilder.AddColumn<int>(
                name: "CVacunaID",
                table: "Vacunas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_CVacunaID",
                table: "Vacunas",
                column: "CVacunaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunas_CalendarioVacuna_CVacunaID",
                table: "Vacunas",
                column: "CVacunaID",
                principalTable: "CalendarioVacuna",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
