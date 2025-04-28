using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class NameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Gestor_GestorId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Gestor_GestorId1",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Aluno_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestor_GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestor_GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Docente_DocenteId",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Gestor_GestorId",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Gestor_GestorId1",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Materias_MateriaId",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Gestor_GestorId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Gestor_GestorId1",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Docente_Gestor_GestorId",
                table: "Docente");

            migrationBuilder.DropForeignKey(
                name: "FK_Docente_Gestor_GestorId1",
                table: "Docente");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestor_Gestor_GestorId",
                table: "Gestor");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestor_Gestor_GestorId1",
                table: "Gestor");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Cursos_CursoId",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Gestor_GestorId",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Gestor_GestorId1",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Materias_MateriaId",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestor_GestorId",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestor_GestorId1",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Aluno_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestor_GestorId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestor_GestorId1",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAluno_Aluno_AlunoId",
                table: "TurmaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAluno_Gestor_GestorId",
                table: "TurmaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAluno_Turmas_TurmaId",
                table: "TurmaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Docente_DocenteId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Docente_DocenteId1",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestor_GestorId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestor_GestorId1",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmaAluno",
                table: "TurmaAluno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeCurricular",
                table: "GradeCurricular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gestor",
                table: "Gestor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Docente",
                table: "Docente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorpoDocente",
                table: "CorpoDocente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno");

            migrationBuilder.RenameTable(
                name: "TurmaAluno",
                newName: "TurmaAlunos");

            migrationBuilder.RenameTable(
                name: "GradeCurricular",
                newName: "GradesCurriculares");

            migrationBuilder.RenameTable(
                name: "Gestor",
                newName: "Gestores");

            migrationBuilder.RenameTable(
                name: "Docente",
                newName: "Docentes");

            migrationBuilder.RenameTable(
                name: "CorpoDocente",
                newName: "CorposDocentes");

            migrationBuilder.RenameTable(
                name: "Aluno",
                newName: "Alunos");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaAluno_GestorId",
                table: "TurmaAlunos",
                newName: "IX_TurmaAlunos_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaAluno_AlunoId",
                table: "TurmaAlunos",
                newName: "IX_TurmaAlunos_AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeCurricular_MateriaId",
                table: "GradesCurriculares",
                newName: "IX_GradesCurriculares_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeCurricular_GestorId1",
                table: "GradesCurriculares",
                newName: "IX_GradesCurriculares_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_GradeCurricular_GestorId",
                table: "GradesCurriculares",
                newName: "IX_GradesCurriculares_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Gestor_GestorId1",
                table: "Gestores",
                newName: "IX_Gestores_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Gestor_GestorId",
                table: "Gestores",
                newName: "IX_Gestores_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Docente_GestorId1",
                table: "Docentes",
                newName: "IX_Docentes_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Docente_GestorId",
                table: "Docentes",
                newName: "IX_Docentes_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_CorpoDocente_MateriaId",
                table: "CorposDocentes",
                newName: "IX_CorposDocentes_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CorpoDocente_GestorId1",
                table: "CorposDocentes",
                newName: "IX_CorposDocentes_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_CorpoDocente_GestorId",
                table: "CorposDocentes",
                newName: "IX_CorposDocentes_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_GestorId1",
                table: "Alunos",
                newName: "IX_Alunos_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_GestorId",
                table: "Alunos",
                newName: "IX_Alunos_GestorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmaAlunos",
                table: "TurmaAlunos",
                columns: new[] { "TurmaId", "AlunoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradesCurriculares",
                table: "GradesCurriculares",
                columns: new[] { "CursoId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gestores",
                table: "Gestores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Docentes",
                table: "Docentes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorposDocentes",
                table: "CorposDocentes",
                columns: new[] { "DocenteId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Gestores_GestorId",
                table: "Alunos",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Gestores_GestorId1",
                table: "Alunos",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Alunos_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId",
                table: "Avaliacoes",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId1",
                table: "Avaliacoes",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Docentes_DocenteId",
                table: "CorposDocentes",
                column: "DocenteId",
                principalTable: "Docentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId",
                table: "CorposDocentes",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId1",
                table: "CorposDocentes",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Gestores_GestorId",
                table: "Cursos",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Gestores_GestorId1",
                table: "Cursos",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Gestores_GestorId",
                table: "Docentes",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Gestores_GestorId1",
                table: "Docentes",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gestores_Gestores_GestorId",
                table: "Gestores",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gestores_Gestores_GestorId1",
                table: "Gestores",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Gestores_GestorId",
                table: "GradesCurriculares",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Gestores_GestorId1",
                table: "GradesCurriculares",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Gestores_GestorId",
                table: "Materias",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Gestores_GestorId1",
                table: "Materias",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Alunos_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Gestores_GestorId",
                table: "Matriculas",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Gestores_GestorId1",
                table: "Matriculas",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Alunos_AlunoId",
                table: "TurmaAlunos",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Gestores_GestorId",
                table: "TurmaAlunos",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Docentes_DocenteId",
                table: "Turmas",
                column: "DocenteId",
                principalTable: "Docentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Docentes_DocenteId1",
                table: "Turmas",
                column: "DocenteId1",
                principalTable: "Docentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Gestores_GestorId",
                table: "Turmas",
                column: "GestorId",
                principalTable: "Gestores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Gestores_GestorId1",
                table: "Turmas",
                column: "GestorId1",
                principalTable: "Gestores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Gestores_GestorId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Gestores_GestorId1",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Alunos_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Docentes_DocenteId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId1",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Materias_MateriaId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Gestores_GestorId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Gestores_GestorId1",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Gestores_GestorId",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Gestores_GestorId1",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestores_Gestores_GestorId",
                table: "Gestores");

            migrationBuilder.DropForeignKey(
                name: "FK_Gestores_Gestores_GestorId1",
                table: "Gestores");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Cursos_CursoId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Gestores_GestorId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Gestores_GestorId1",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Materias_MateriaId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestores_GestorId",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestores_GestorId1",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Alunos_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestores_GestorId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestores_GestorId1",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Alunos_AlunoId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Gestores_GestorId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Turmas_TurmaId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Docentes_DocenteId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Docentes_DocenteId1",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestores_GestorId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestores_GestorId1",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmaAlunos",
                table: "TurmaAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradesCurriculares",
                table: "GradesCurriculares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gestores",
                table: "Gestores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Docentes",
                table: "Docentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorposDocentes",
                table: "CorposDocentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos");

            migrationBuilder.RenameTable(
                name: "TurmaAlunos",
                newName: "TurmaAluno");

            migrationBuilder.RenameTable(
                name: "GradesCurriculares",
                newName: "GradeCurricular");

            migrationBuilder.RenameTable(
                name: "Gestores",
                newName: "Gestor");

            migrationBuilder.RenameTable(
                name: "Docentes",
                newName: "Docente");

            migrationBuilder.RenameTable(
                name: "CorposDocentes",
                newName: "CorpoDocente");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "Aluno");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaAlunos_GestorId",
                table: "TurmaAluno",
                newName: "IX_TurmaAluno_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaAlunos_AlunoId",
                table: "TurmaAluno",
                newName: "IX_TurmaAluno_AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_GradesCurriculares_MateriaId",
                table: "GradeCurricular",
                newName: "IX_GradeCurricular_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_GradesCurriculares_GestorId1",
                table: "GradeCurricular",
                newName: "IX_GradeCurricular_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_GradesCurriculares_GestorId",
                table: "GradeCurricular",
                newName: "IX_GradeCurricular_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Gestores_GestorId1",
                table: "Gestor",
                newName: "IX_Gestor_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Gestores_GestorId",
                table: "Gestor",
                newName: "IX_Gestor_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Docentes_GestorId1",
                table: "Docente",
                newName: "IX_Docente_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Docentes_GestorId",
                table: "Docente",
                newName: "IX_Docente_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_CorposDocentes_MateriaId",
                table: "CorpoDocente",
                newName: "IX_CorpoDocente_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CorposDocentes_GestorId1",
                table: "CorpoDocente",
                newName: "IX_CorpoDocente_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_CorposDocentes_GestorId",
                table: "CorpoDocente",
                newName: "IX_CorpoDocente_GestorId");

            migrationBuilder.RenameIndex(
                name: "IX_Alunos_GestorId1",
                table: "Aluno",
                newName: "IX_Aluno_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Alunos_GestorId",
                table: "Aluno",
                newName: "IX_Aluno_GestorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmaAluno",
                table: "TurmaAluno",
                columns: new[] { "TurmaId", "AlunoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeCurricular",
                table: "GradeCurricular",
                columns: new[] { "CursoId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gestor",
                table: "Gestor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Docente",
                table: "Docente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorpoDocente",
                table: "CorpoDocente",
                columns: new[] { "DocenteId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Gestor_GestorId",
                table: "Aluno",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Gestor_GestorId1",
                table: "Aluno",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Aluno_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Gestor_GestorId",
                table: "Avaliacoes",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Gestor_GestorId1",
                table: "Avaliacoes",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Docente_DocenteId",
                table: "CorpoDocente",
                column: "DocenteId",
                principalTable: "Docente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Gestor_GestorId",
                table: "CorpoDocente",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Gestor_GestorId1",
                table: "CorpoDocente",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Materias_MateriaId",
                table: "CorpoDocente",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Gestor_GestorId",
                table: "Cursos",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Gestor_GestorId1",
                table: "Cursos",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docente_Gestor_GestorId",
                table: "Docente",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Docente_Gestor_GestorId1",
                table: "Docente",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gestor_Gestor_GestorId",
                table: "Gestor",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gestor_Gestor_GestorId1",
                table: "Gestor",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Cursos_CursoId",
                table: "GradeCurricular",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Gestor_GestorId",
                table: "GradeCurricular",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Gestor_GestorId1",
                table: "GradeCurricular",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Materias_MateriaId",
                table: "GradeCurricular",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Gestor_GestorId",
                table: "Materias",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Gestor_GestorId1",
                table: "Materias",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Aluno_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Gestor_GestorId",
                table: "Matriculas",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Gestor_GestorId1",
                table: "Matriculas",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAluno_Aluno_AlunoId",
                table: "TurmaAluno",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAluno_Gestor_GestorId",
                table: "TurmaAluno",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAluno_Turmas_TurmaId",
                table: "TurmaAluno",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Docente_DocenteId",
                table: "Turmas",
                column: "DocenteId",
                principalTable: "Docente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Docente_DocenteId1",
                table: "Turmas",
                column: "DocenteId1",
                principalTable: "Docente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Gestor_GestorId",
                table: "Turmas",
                column: "GestorId",
                principalTable: "Gestor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Gestor_GestorId1",
                table: "Turmas",
                column: "GestorId1",
                principalTable: "Gestor",
                principalColumn: "Id");
        }
    }
}
