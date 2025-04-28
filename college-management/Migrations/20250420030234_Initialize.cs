using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace college_management.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    GestorId = table.Column<uint>(type: "INTEGER", nullable: false),
                    GestorId1 = table.Column<uint>(type: "INTEGER", nullable: true),
                    TipoUsuario = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    CargaHoraria = table.Column<uint>(type: "INTEGER", nullable: true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Sal = table.Column<string>(type: "TEXT", nullable: true),
                    Cargo = table.Column<int>(type: "INTEGER", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Modelo_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modelo_Modelo_GestorId1",
                        column: x => x.GestorId1,
                        principalTable: "Modelo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    AlunoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MateriaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    P1 = table.Column<float>(type: "REAL", nullable: true),
                    P2 = table.Column<float>(type: "REAL", nullable: true),
                    P3 = table.Column<float>(type: "REAL", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => new { x.AlunoId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Modelo_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Modelo_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorpoDocente",
                columns: table => new
                {
                    MateriaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DocenteId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpoDocente", x => new { x.DocenteId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_CorpoDocente_Modelo_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorpoDocente_Modelo_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeCurricular",
                columns: table => new
                {
                    CursoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MateriaId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeCurricular", x => new { x.CursoId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_GradeCurricular_Modelo_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeCurricular_Modelo_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    CursoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    AlunoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Periodo = table.Column<uint>(type: "INTEGER", nullable: false),
                    Modalidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Matriculas_Modelo_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Modelo_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    MateriaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    AlunoId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Turno = table.Column<int>(type: "INTEGER", nullable: false),
                    DocenteId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DocenteId1 = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => new { x.AlunoId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_Turmas_Modelo_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Modelo_DocenteId",
                        column: x => x.DocenteId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Modelo_DocenteId1",
                        column: x => x.DocenteId1,
                        principalTable: "Modelo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Turmas_Modelo_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_MateriaId",
                table: "Avaliacoes",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CorpoDocente_MateriaId",
                table: "CorpoDocente",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeCurricular_MateriaId",
                table: "GradeCurricular",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_GestorId",
                table: "Modelo",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_GestorId1",
                table: "Modelo",
                column: "GestorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_DocenteId",
                table: "Turmas",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_DocenteId1",
                table: "Turmas",
                column: "DocenteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_MateriaId",
                table: "Turmas",
                column: "MateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "CorpoDocente");

            migrationBuilder.DropTable(
                name: "GradeCurricular");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Modelo");
        }
    }
}
