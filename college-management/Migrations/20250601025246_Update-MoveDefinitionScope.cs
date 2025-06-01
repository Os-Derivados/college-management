using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoveDefinitionScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Usuarios_AlunoId",
                table: "TurmaAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmaAlunos",
                table: "TurmaAlunos");

            migrationBuilder.RenameTable(
                name: "TurmaAlunos",
                newName: "TurmasAlunos");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaAlunos_AlunoId",
                table: "TurmasAlunos",
                newName: "IX_TurmasAlunos_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmasAlunos",
                table: "TurmasAlunos",
                columns: new[] { "TurmaId", "AlunoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TurmasAlunos_Turmas_TurmaId",
                table: "TurmasAlunos",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmasAlunos_Usuarios_AlunoId",
                table: "TurmasAlunos",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmasAlunos_Turmas_TurmaId",
                table: "TurmasAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmasAlunos_Usuarios_AlunoId",
                table: "TurmasAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmasAlunos",
                table: "TurmasAlunos");

            migrationBuilder.RenameTable(
                name: "TurmasAlunos",
                newName: "TurmaAlunos");

            migrationBuilder.RenameIndex(
                name: "IX_TurmasAlunos_AlunoId",
                table: "TurmaAlunos",
                newName: "IX_TurmaAlunos_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmaAlunos",
                table: "TurmaAlunos",
                columns: new[] { "TurmaId", "AlunoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Usuarios_AlunoId",
                table: "TurmaAlunos",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
