using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Materias");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materias_Nome",
                table: "Materias",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_Nome",
                table: "Cursos",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Materias_Nome",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_Nome",
                table: "Cursos");

            migrationBuilder.AddColumn<uint>(
                name: "CargaHoraria",
                table: "Materias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);
        }
    }
}
