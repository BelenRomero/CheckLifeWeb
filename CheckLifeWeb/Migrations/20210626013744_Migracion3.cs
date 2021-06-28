using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckLifeWeb.Migrations
{
    public partial class Migracion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centro_Login_LoginID",
                table: "Centro");

            migrationBuilder.DropForeignKey(
                name: "FK_Login_Rol_RolID",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Login_LoginID",
                table: "Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Nacionalidad_NacionalidadID",
                table: "Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Login_LoginID",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Nacionalidad_NacionalidadID",
                table: "Paciente");

            migrationBuilder.AddColumn<int>(
                name: "EstadoID",
                table: "Vacunas",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NacionalidadID",
                table: "Paciente",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Paciente",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NacionalidadID",
                table: "Medico",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Medico",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RolID",
                table: "Login",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Centro",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "EstadoVacuna",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoVacuna", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_EstadoID",
                table: "Vacunas",
                column: "EstadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Centro_Login_LoginID",
                table: "Centro",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Rol_RolID",
                table: "Login",
                column: "RolID",
                principalTable: "Rol",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Login_LoginID",
                table: "Medico",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Nacionalidad_NacionalidadID",
                table: "Medico",
                column: "NacionalidadID",
                principalTable: "Nacionalidad",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Login_LoginID",
                table: "Paciente",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Nacionalidad_NacionalidadID",
                table: "Paciente",
                column: "NacionalidadID",
                principalTable: "Nacionalidad",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunas_EstadoVacuna_EstadoID",
                table: "Vacunas",
                column: "EstadoID",
                principalTable: "EstadoVacuna",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centro_Login_LoginID",
                table: "Centro");

            migrationBuilder.DropForeignKey(
                name: "FK_Login_Rol_RolID",
                table: "Login");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Login_LoginID",
                table: "Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Nacionalidad_NacionalidadID",
                table: "Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Login_LoginID",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Nacionalidad_NacionalidadID",
                table: "Paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacunas_EstadoVacuna_EstadoID",
                table: "Vacunas");

            migrationBuilder.DropTable(
                name: "EstadoVacuna");

            migrationBuilder.DropIndex(
                name: "IX_Vacunas_EstadoID",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "EstadoID",
                table: "Vacunas");

            migrationBuilder.AlterColumn<int>(
                name: "NacionalidadID",
                table: "Paciente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Paciente",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NacionalidadID",
                table: "Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RolID",
                table: "Login",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoginID",
                table: "Centro",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Centro_Login_LoginID",
                table: "Centro",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Rol_RolID",
                table: "Login",
                column: "RolID",
                principalTable: "Rol",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Login_LoginID",
                table: "Medico",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Nacionalidad_NacionalidadID",
                table: "Medico",
                column: "NacionalidadID",
                principalTable: "Nacionalidad",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Login_LoginID",
                table: "Paciente",
                column: "LoginID",
                principalTable: "Login",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Nacionalidad_NacionalidadID",
                table: "Paciente",
                column: "NacionalidadID",
                principalTable: "Nacionalidad",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
