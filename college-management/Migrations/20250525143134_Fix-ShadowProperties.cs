using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Materias_MateriaId1",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Usuarios_DocenteId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_DocenteId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_MateriaId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DocenteId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "MateriaId1",
                table: "Turmas");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Turmas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Turmas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "TurmaAlunos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "TurmaAlunos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Matriculas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Matriculas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Materias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Materias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "GradesCurriculares",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "GradesCurriculares",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Cursos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Cursos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "CorposDocentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "CorposDocentes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Avaliacoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Avaliacoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlunoCurso",
                columns: table => new
                {
                    AlunosId = table.Column<uint>(type: "INTEGER", nullable: false),
                    CursosId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoCurso", x => new { x.AlunosId, x.CursosId });
                    table.ForeignKey(
                        name: "FK_AlunoCurso_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoCurso_Usuarios_AlunosId",
                        column: x => x.AlunosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoMateria",
                columns: table => new
                {
                    AlunosId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MateriasId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoMateria", x => new { x.AlunosId, x.MateriasId });
                    table.ForeignKey(
                        name: "FK_AlunoMateria_Materias_MateriasId",
                        column: x => x.MateriasId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoMateria_Usuarios_AlunosId",
                        column: x => x.AlunosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    AlunosId = table.Column<uint>(type: "INTEGER", nullable: false),
                    TurmasId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => new { x.AlunosId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Turmas_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Usuarios_AlunosId",
                        column: x => x.AlunosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoMateria",
                columns: table => new
                {
                    CursosId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MateriasId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoMateria", x => new { x.CursosId, x.MateriasId });
                    table.ForeignKey(
                        name: "FK_CursoMateria_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoMateria_Materias_MateriasId",
                        column: x => x.MateriasId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocenteMateria",
                columns: table => new
                {
                    DocentesId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MateriasId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteMateria", x => new { x.DocentesId, x.MateriasId });
                    table.ForeignKey(
                        name: "FK_DocenteMateria_Materias_MateriasId",
                        column: x => x.MateriasId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocenteMateria_Usuarios_DocentesId",
                        column: x => x.DocentesId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoCurso_CursosId",
                table: "AlunoCurso",
                column: "CursosId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoMateria_MateriasId",
                table: "AlunoMateria",
                column: "MateriasId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_TurmasId",
                table: "AlunoTurma",
                column: "TurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoMateria_MateriasId",
                table: "CursoMateria",
                column: "MateriasId");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteMateria_MateriasId",
                table: "DocenteMateria",
                column: "MateriasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoCurso");

            migrationBuilder.DropTable(
                name: "AlunoMateria");

            migrationBuilder.DropTable(
                name: "AlunoTurma");

            migrationBuilder.DropTable(
                name: "CursoMateria");

            migrationBuilder.DropTable(
                name: "DocenteMateria");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "TurmaAlunos");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "GradesCurriculares");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "CorposDocentes");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Avaliacoes");

            migrationBuilder.AddColumn<uint>(
                name: "DocenteId1",
                table: "Turmas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "MateriaId1",
                table: "Turmas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_DocenteId1",
                table: "Turmas",
                column: "DocenteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_MateriaId1",
                table: "Turmas",
                column: "MateriaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Materias_MateriaId1",
                table: "Turmas",
                column: "MateriaId1",
                principalTable: "Materias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Usuarios_DocenteId1",
                table: "Turmas",
                column: "DocenteId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
