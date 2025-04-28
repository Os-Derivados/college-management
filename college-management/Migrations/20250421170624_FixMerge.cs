using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class FixMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Modelo_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Modelo_MateriaId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Modelo_DocenteId",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_CorpoDocente_Modelo_MateriaId",
                table: "CorpoDocente");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Modelo_CursoId",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeCurricular_Modelo_MateriaId",
                table: "GradeCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Modelo_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Modelo_CursoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Modelo_Modelo_GestorId",
                table: "Modelo");

            migrationBuilder.DropForeignKey(
                name: "FK_Modelo_Modelo_GestorId1",
                table: "Modelo");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Modelo_AlunoId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Modelo_DocenteId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Modelo_DocenteId1",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Modelo_MateriaId",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculas",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_MateriaId",
                table: "Avaliacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modelo",
                table: "Modelo");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Modelo");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Modelo");

            migrationBuilder.DropColumn(
                name: "Sal",
                table: "Modelo");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Modelo");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Modelo");

            migrationBuilder.RenameTable(
                name: "Modelo",
                newName: "Materias");

            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "Turmas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Modelo_GestorId1",
                table: "Materias",
                newName: "IX_Materias_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Modelo_GestorId",
                table: "Materias",
                newName: "IX_Materias_GestorId");

            migrationBuilder.AlterColumn<uint>(
                name: "DocenteId",
                table: "Turmas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "MateriaId",
                table: "Turmas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

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
                name: "MateriaId1",
                table: "Turmas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Turmas",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

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
                table: "GradeCurricular",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "GradeCurricular",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId",
                table: "CorpoDocente",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "GestorId1",
                table: "CorpoDocente",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Avaliacoes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);

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

            migrationBuilder.AlterColumn<uint>(
                name: "GestorId",
                table: "Materias",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "CargaHoraria",
                table: "Materias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Materias",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculas",
                table: "Matriculas",
                columns: new[] { "CursoId", "AlunoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                columns: new[] { "MateriaId", "AlunoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materias",
                table: "Materias",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Gestor",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: true),
                    GestorId1 = table.Column<uint>(type: "INTEGER", nullable: true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Sal = table.Column<string>(type: "TEXT", nullable: true),
                    Cargo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gestor_Gestor_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gestor_Gestor_GestorId1",
                        column: x => x.GestorId1,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: true),
                    GestorId1 = table.Column<uint>(type: "INTEGER", nullable: true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Sal = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Gestor_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aluno_Gestor_GestorId1",
                        column: x => x.GestorId1,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: true),
                    GestorId1 = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Gestor_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cursos_Gestor_GestorId1",
                        column: x => x.GestorId1,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Docente",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: true),
                    GestorId1 = table.Column<uint>(type: "INTEGER", nullable: true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Sal = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docente_Gestor_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Docente_Gestor_GestorId1",
                        column: x => x.GestorId1,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TurmaAluno",
                columns: table => new
                {
                    TurmaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    AlunoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAluno", x => new { x.TurmaId, x.AlunoId });
                    table.ForeignKey(
                        name: "FK_TurmaAluno_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaAluno_Gestor_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TurmaAluno_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_GestorId",
                table: "Turmas",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_GestorId1",
                table: "Turmas",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_MateriaId1",
                table: "Turmas",
                column: "MateriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_GestorId",
                table: "Matriculas",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_GestorId1",
                table: "Matriculas",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_GradeCurricular_GestorId",
                table: "GradeCurricular",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeCurricular_GestorId1",
                table: "GradeCurricular",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_CorpoDocente_GestorId",
                table: "CorpoDocente",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_CorpoDocente_GestorId1",
                table: "CorpoDocente",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_GestorId",
                table: "Avaliacoes",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_GestorId1",
                table: "Avaliacoes",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_GestorId",
                table: "Aluno",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_GestorId1",
                table: "Aluno",
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
                name: "IX_Docente_GestorId",
                table: "Docente",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Docente_GestorId1",
                table: "Docente",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Gestor_GestorId",
                table: "Gestor",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Gestor_GestorId1",
                table: "Gestor",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAluno_AlunoId",
                table: "TurmaAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAluno_GestorId",
                table: "TurmaAluno",
                column: "GestorId");

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
                name: "FK_Avaliacoes_Materias_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas",
                column: "CursoId",
                principalTable: "Cursos",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Materias_MateriaId",
                table: "Turmas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Materias_MateriaId1",
                table: "Turmas",
                column: "MateriaId1",
                principalTable: "Materias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Avaliacoes_Materias_MateriaId",
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
                name: "FK_Matriculas_Cursos_CursoId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestor_GestorId",
                table: "Matriculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Gestor_GestorId1",
                table: "Matriculas");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Materias_MateriaId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Materias_MateriaId1",
                table: "Turmas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Docente");

            migrationBuilder.DropTable(
                name: "TurmaAluno");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Gestor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_GestorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_GestorId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_MateriaId1",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculas",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_GestorId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_GestorId1",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_GradeCurricular_GestorId",
                table: "GradeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_GradeCurricular_GestorId1",
                table: "GradeCurricular");

            migrationBuilder.DropIndex(
                name: "IX_CorpoDocente_GestorId",
                table: "CorpoDocente");

            migrationBuilder.DropIndex(
                name: "IX_CorpoDocente_GestorId1",
                table: "CorpoDocente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_GestorId1",
                table: "Avaliacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materias",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "MateriaId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "GradeCurricular");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "GradeCurricular");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "CorpoDocente");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "CorpoDocente");

            migrationBuilder.DropColumn(
                name: "GestorId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "GestorId1",
                table: "Avaliacoes");

            migrationBuilder.RenameTable(
                name: "Materias",
                newName: "Modelo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Turmas",
                newName: "AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Materias_GestorId1",
                table: "Modelo",
                newName: "IX_Modelo_GestorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Materias_GestorId",
                table: "Modelo",
                newName: "IX_Modelo_GestorId");

            migrationBuilder.AlterColumn<uint>(
                name: "MateriaId",
                table: "Turmas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "DocenteId",
                table: "Turmas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Avaliacoes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "GestorId",
                table: "Modelo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "CargaHoraria",
                table: "Modelo",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Modelo",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Cargo",
                table: "Modelo",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Modelo",
                type: "TEXT",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sal",
                table: "Modelo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Modelo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "Modelo",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas",
                columns: new[] { "AlunoId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculas",
                table: "Matriculas",
                columns: new[] { "AlunoId", "CursoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avaliacoes",
                table: "Avaliacoes",
                columns: new[] { "AlunoId", "MateriaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modelo",
                table: "Modelo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Modelo_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Modelo_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Modelo_DocenteId",
                table: "CorpoDocente",
                column: "DocenteId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CorpoDocente_Modelo_MateriaId",
                table: "CorpoDocente",
                column: "MateriaId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Modelo_CursoId",
                table: "GradeCurricular",
                column: "CursoId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeCurricular_Modelo_MateriaId",
                table: "GradeCurricular",
                column: "MateriaId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Modelo_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Modelo_CursoId",
                table: "Matriculas",
                column: "CursoId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modelo_Modelo_GestorId",
                table: "Modelo",
                column: "GestorId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modelo_Modelo_GestorId1",
                table: "Modelo",
                column: "GestorId1",
                principalTable: "Modelo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Modelo_AlunoId",
                table: "Turmas",
                column: "AlunoId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Modelo_DocenteId",
                table: "Turmas",
                column: "DocenteId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Modelo_DocenteId1",
                table: "Turmas",
                column: "DocenteId1",
                principalTable: "Modelo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Modelo_MateriaId",
                table: "Turmas",
                column: "MateriaId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
