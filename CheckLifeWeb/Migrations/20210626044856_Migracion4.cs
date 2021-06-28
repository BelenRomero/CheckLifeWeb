using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckLifeWeb.Migrations
{
    public partial class Migracion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunas_Paciente_PacienteID",
                table: "Vacunas");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteID",
                table: "Vacunas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunas_Paciente_PacienteID",
                table: "Vacunas",
                column: "PacienteID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunas_Paciente_PacienteID",
                table: "Vacunas");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteID",
                table: "Vacunas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunas_Paciente_PacienteID",
                table: "Vacunas",
                column: "PacienteID",
                principalTable: "Paciente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
