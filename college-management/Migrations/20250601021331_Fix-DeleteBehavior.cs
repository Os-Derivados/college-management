using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Materias_MateriaId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Usuarios_DocenteId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Usuarios_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Usuarios_AlunoId",
                table: "TurmaAlunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Materias_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Usuarios_DocenteId",
                table: "CorposDocentes",
                column: "DocenteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Usuarios_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Materias_MateriaId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Usuarios_DocenteId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Usuarios_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Usuarios_AlunoId",
                table: "TurmaAlunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Materias_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Usuarios_DocenteId",
                table: "CorposDocentes",
                column: "DocenteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Usuarios_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Usuarios_AlunoId",
                table: "TurmaAlunos",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
