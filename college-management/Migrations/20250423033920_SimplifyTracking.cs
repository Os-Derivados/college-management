using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Gestores_GestorId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Gestores_GestorId1",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Gestores_GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId",
                table: "CorposDocentes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorposDocentes_Gestores_GestorId1",
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
                name: "FK_GradesCurriculares_Gestores_GestorId",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_GradesCurriculares_Gestores_GestorId1",
                table: "GradesCurriculares");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestores_GestorId",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Gestores_GestorId1",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestores_GestorId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestores_GestorId1",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAlunos_Gestores_GestorId",
                table: "TurmaAlunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestores_GestorId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Gestores_GestorId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_GestorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_GestorId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_TurmaAlunos_GestorId",
                table: "TurmaAlunos");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_GestorId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_GestorId1",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Materias_GestorId",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materias_GestorId1",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_GradesCurriculares_GestorId",
                table: "GradesCurriculares");

            migrationBuilder.DropIndex(
                name: "IX_GradesCurriculares_GestorId1",
                table: "GradesCurriculares");

            migrationBuilder.DropIndex(
                name: "IX_Gestores_GestorId",
                table: "Gestores");

            migrationBuilder.DropIndex(
                name: "IX_Gestores_GestorId1",
                table: "Gestores");

            migrationBuilder.DropIndex(
                name: "IX_Docentes_GestorId",
                table: "Docentes");

            migrationBuilder.DropIndex(
                name: "IX_Docentes_GestorId1",
                table: "Docentes");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_GestorId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_GestorId1",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_CorposDocentes_GestorId",
                table: "CorposDocentes");

            migrationBuilder.DropIndex(
                name: "IX_CorposDocentes_GestorId1",
                table: "CorposDocentes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_GestorId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_GestorId1",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Gestores");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Gestores");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Alunos");

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Turmas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Turmas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "TurmaAlunos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "TurmaAlunos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Matriculas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Matriculas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Materias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Materias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "GradesCurriculares",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "GradesCurriculares",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Gestores",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Gestores",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Docentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Docentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Cursos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Cursos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "CorposDocentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "CorposDocentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Avaliacoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Avaliacoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Alunos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditadoPor",
                table: "Alunos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Gestores");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Gestores");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EditadoPor",
                table: "Alunos");

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Turmas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Turmas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "TurmaAlunos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Matriculas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Matriculas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Materias",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Materias",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "GradesCurriculares",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "GradesCurriculares",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Gestores",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Gestores",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Docentes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Docentes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Cursos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Cursos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "CorposDocentes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "CorposDocentes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Avaliacoes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Avaliacoes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "Alunos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "Alunos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_GestorId",
                table: "Turmas",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_GestorId1",
                table: "Turmas",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAlunos_GestorId",
                table: "TurmaAlunos",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_GestorId",
                table: "Matriculas",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_GestorId1",
                table: "Matriculas",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_GestorId",
                table: "Materias",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_GestorId1",
                table: "Materias",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_GradesCurriculares_GestorId",
                table: "GradesCurriculares",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_GradesCurriculares_GestorId1",
                table: "GradesCurriculares",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_GestorId",
                table: "Gestores",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_GestorId1",
                table: "Gestores",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_GestorId",
                table: "Docentes",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_GestorId1",
                table: "Docentes",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_GestorId",
                table: "Cursos",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_GestorId1",
                table: "Cursos",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_CorposDocentes_GestorId",
                table: "CorposDocentes",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_CorposDocentes_GestorId1",
                table: "CorposDocentes",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_GestorId",
                table: "Avaliacoes",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_GestorId1",
                table: "Avaliacoes",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_GestorId",
                table: "Alunos",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_GestorId1",
                table: "Alunos",
                column: "GestorId1");

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
                name: "FK_TurmaAlunos_Gestores_GestorId",
                table: "TurmaAlunos",
                column: "GestorId",
                principalTable: "Gestores",
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
    }
}
