CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Modelo" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Modelo" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NOT NULL,
    "GestorId" INTEGER NOT NULL,
    "GestorId1" INTEGER NULL,
    "TipoUsuario" TEXT NOT NULL,
    "CargaHoraria" INTEGER NULL,
    "Login" TEXT NULL,
    "Senha" TEXT NULL,
    "Sal" TEXT NULL,
    "Cargo" INTEGER NULL DEFAULT 0,
    CONSTRAINT "FK_Modelo_Modelo_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Modelo_Modelo_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Modelo" ("Id")
);

CREATE TABLE "Avaliacoes" (
    "AlunoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL DEFAULT 0,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("AlunoId", "MateriaId"),
    CONSTRAINT "FK_Avaliacoes_Modelo_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Avaliacoes_Modelo_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CorpoDocente" (
    "MateriaId" INTEGER NOT NULL,
    "DocenteId" INTEGER NOT NULL,
    CONSTRAINT "PK_CorpoDocente" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorpoDocente_Modelo_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CorpoDocente_Modelo_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE
);

CREATE TABLE "GradeCurricular" (
    "CursoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    CONSTRAINT "PK_GradeCurricular" PRIMARY KEY ("CursoId", "MateriaId"),
    CONSTRAINT "FK_GradeCurricular_Modelo_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GradeCurricular_Modelo_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    "Modalidade" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("AlunoId", "CursoId"),
    CONSTRAINT "FK_Matriculas_Modelo_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Modelo_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Turmas" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "Turno" INTEGER NOT NULL,
    "DocenteId" INTEGER NOT NULL,
    "DocenteId1" INTEGER NULL,
    CONSTRAINT "PK_Turmas" PRIMARY KEY ("AlunoId", "MateriaId"),
    CONSTRAINT "FK_Turmas_Modelo_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Turmas_Modelo_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Turmas_Modelo_DocenteId1" FOREIGN KEY ("DocenteId1") REFERENCES "Modelo" ("Id"),
    CONSTRAINT "FK_Turmas_Modelo_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Modelo" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Avaliacoes_MateriaId" ON "Avaliacoes" ("MateriaId");

CREATE INDEX "IX_CorpoDocente_MateriaId" ON "CorpoDocente" ("MateriaId");

CREATE INDEX "IX_GradeCurricular_MateriaId" ON "GradeCurricular" ("MateriaId");

CREATE INDEX "IX_Matriculas_CursoId" ON "Matriculas" ("CursoId");

CREATE INDEX "IX_Modelo_GestorId" ON "Modelo" ("GestorId");

CREATE INDEX "IX_Modelo_GestorId1" ON "Modelo" ("GestorId1");

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_DocenteId1" ON "Turmas" ("DocenteId1");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250420030234_Initialize', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

DROP INDEX "IX_Matriculas_CursoId";

DROP INDEX "IX_Avaliacoes_MateriaId";

ALTER TABLE "Modelo" RENAME TO "Materias";

ALTER TABLE "Turmas" RENAME COLUMN "AlunoId" TO "Id";

DROP INDEX "IX_Modelo_GestorId1";

CREATE INDEX "IX_Materias_GestorId1" ON "Materias" ("GestorId1");

DROP INDEX "IX_Modelo_GestorId";

CREATE INDEX "IX_Materias_GestorId" ON "Materias" ("GestorId");

ALTER TABLE "Turmas" ADD "GestorId" INTEGER NULL;

ALTER TABLE "Turmas" ADD "GestorId1" INTEGER NULL;

ALTER TABLE "Turmas" ADD "MateriaId1" INTEGER NULL;

ALTER TABLE "Turmas" ADD "Nome" TEXT NOT NULL DEFAULT '';

ALTER TABLE "Matriculas" ADD "GestorId" INTEGER NULL;

ALTER TABLE "Matriculas" ADD "GestorId1" INTEGER NULL;

ALTER TABLE "GradeCurricular" ADD "GestorId" INTEGER NULL;

ALTER TABLE "GradeCurricular" ADD "GestorId1" INTEGER NULL;

ALTER TABLE "CorpoDocente" ADD "GestorId" INTEGER NULL;

ALTER TABLE "CorpoDocente" ADD "GestorId1" INTEGER NULL;

ALTER TABLE "Avaliacoes" ADD "GestorId" INTEGER NULL;

ALTER TABLE "Avaliacoes" ADD "GestorId1" INTEGER NULL;

CREATE TABLE "Gestor" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Gestor" PRIMARY KEY,
    "Nome" TEXT NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Senha" TEXT NULL,
    "Sal" TEXT NULL,
    "Cargo" INTEGER NOT NULL,
    CONSTRAINT "FK_Gestor_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Gestor_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

CREATE TABLE "Aluno" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Aluno" PRIMARY KEY,
    "Nome" TEXT NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Senha" TEXT NULL,
    "Sal" TEXT NULL,
    CONSTRAINT "FK_Aluno_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Aluno_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

CREATE TABLE "Cursos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Cursos" PRIMARY KEY,
    "Nome" TEXT NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    CONSTRAINT "FK_Cursos_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Cursos_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

CREATE TABLE "Docente" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Docente" PRIMARY KEY,
    "Nome" TEXT NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Senha" TEXT NULL,
    "Sal" TEXT NULL,
    CONSTRAINT "FK_Docente_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Docente_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

CREATE TABLE "TurmaAluno" (
    "TurmaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    CONSTRAINT "PK_TurmaAluno" PRIMARY KEY ("TurmaId", "AlunoId"),
    CONSTRAINT "FK_TurmaAluno_Aluno_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Aluno" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TurmaAluno_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_TurmaAluno_Turmas_TurmaId" FOREIGN KEY ("TurmaId") REFERENCES "Turmas" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Turmas_GestorId" ON "Turmas" ("GestorId");

CREATE INDEX "IX_Turmas_GestorId1" ON "Turmas" ("GestorId1");

CREATE INDEX "IX_Turmas_MateriaId1" ON "Turmas" ("MateriaId1");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_Matriculas_GestorId" ON "Matriculas" ("GestorId");

CREATE INDEX "IX_Matriculas_GestorId1" ON "Matriculas" ("GestorId1");

CREATE INDEX "IX_GradeCurricular_GestorId" ON "GradeCurricular" ("GestorId");

CREATE INDEX "IX_GradeCurricular_GestorId1" ON "GradeCurricular" ("GestorId1");

CREATE INDEX "IX_CorpoDocente_GestorId" ON "CorpoDocente" ("GestorId");

CREATE INDEX "IX_CorpoDocente_GestorId1" ON "CorpoDocente" ("GestorId1");

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_Avaliacoes_GestorId" ON "Avaliacoes" ("GestorId");

CREATE INDEX "IX_Avaliacoes_GestorId1" ON "Avaliacoes" ("GestorId1");

CREATE INDEX "IX_Aluno_GestorId" ON "Aluno" ("GestorId");

CREATE INDEX "IX_Aluno_GestorId1" ON "Aluno" ("GestorId1");

CREATE INDEX "IX_Cursos_GestorId" ON "Cursos" ("GestorId");

CREATE INDEX "IX_Cursos_GestorId1" ON "Cursos" ("GestorId1");

CREATE INDEX "IX_Docente_GestorId" ON "Docente" ("GestorId");

CREATE INDEX "IX_Docente_GestorId1" ON "Docente" ("GestorId1");

CREATE INDEX "IX_Gestor_GestorId" ON "Gestor" ("GestorId");

CREATE INDEX "IX_Gestor_GestorId1" ON "Gestor" ("GestorId1");

CREATE INDEX "IX_TurmaAluno_AlunoId" ON "TurmaAluno" ("AlunoId");

CREATE INDEX "IX_TurmaAluno_GestorId" ON "TurmaAluno" ("GestorId");

CREATE TABLE "ef_temp_Avaliacoes" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("MateriaId", "AlunoId"),
    CONSTRAINT "FK_Avaliacoes_Aluno_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Aluno" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Avaliacoes_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Avaliacoes_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Avaliacoes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Avaliacoes" ("MateriaId", "AlunoId", "GestorId", "GestorId1", "P1", "P2", "P3", "Status")
SELECT "MateriaId", "AlunoId", "GestorId", "GestorId1", "P1", "P2", "P3", "Status"
FROM "Avaliacoes";

CREATE TABLE "ef_temp_CorpoDocente" (
    "DocenteId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    CONSTRAINT "PK_CorpoDocente" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorpoDocente_Docente_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docente" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CorpoDocente_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_CorpoDocente_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_CorpoDocente_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_CorpoDocente" ("DocenteId", "MateriaId", "GestorId", "GestorId1")
SELECT "DocenteId", "MateriaId", "GestorId", "GestorId1"
FROM "CorpoDocente";

CREATE TABLE "ef_temp_GradeCurricular" (
    "CursoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    CONSTRAINT "PK_GradeCurricular" PRIMARY KEY ("CursoId", "MateriaId"),
    CONSTRAINT "FK_GradeCurricular_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GradeCurricular_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_GradeCurricular_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_GradeCurricular_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_GradeCurricular" ("CursoId", "MateriaId", "GestorId", "GestorId1")
SELECT "CursoId", "MateriaId", "GestorId", "GestorId1"
FROM "GradeCurricular";

CREATE TABLE "ef_temp_Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Modalidade" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("CursoId", "AlunoId"),
    CONSTRAINT "FK_Matriculas_Aluno_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Aluno" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Matriculas_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

INSERT INTO "ef_temp_Matriculas" ("CursoId", "AlunoId", "GestorId", "GestorId1", "Modalidade", "Periodo")
SELECT "CursoId", "AlunoId", "GestorId", "GestorId1", "Modalidade", "Periodo"
FROM "Matriculas";

CREATE TABLE "ef_temp_Materias" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Materias" PRIMARY KEY,
    "CargaHoraria" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    CONSTRAINT "FK_Materias_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Materias_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id")
);

INSERT INTO "ef_temp_Materias" ("Id", "CargaHoraria", "GestorId", "GestorId1", "Nome")
SELECT "Id", IFNULL("CargaHoraria", 0), "GestorId", "GestorId1", "Nome"
FROM "Materias";

CREATE TABLE "ef_temp_Turmas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY,
    "DocenteId" INTEGER NULL,
    "DocenteId1" INTEGER NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "MateriaId" INTEGER NULL,
    "MateriaId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    "Turno" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Docente_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docente" ("Id"),
    CONSTRAINT "FK_Turmas_Docente_DocenteId1" FOREIGN KEY ("DocenteId1") REFERENCES "Docente" ("Id"),
    CONSTRAINT "FK_Turmas_Gestor_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Turmas_Gestor_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestor" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId1" FOREIGN KEY ("MateriaId1") REFERENCES "Materias" ("Id")
);

INSERT INTO "ef_temp_Turmas" ("Id", "DocenteId", "DocenteId1", "GestorId", "GestorId1", "MateriaId", "MateriaId1", "Nome", "Turno")
SELECT "Id", "DocenteId", "DocenteId1", "GestorId", "GestorId1", "MateriaId", "MateriaId1", "Nome", "Turno"
FROM "Turmas";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Avaliacoes";

ALTER TABLE "ef_temp_Avaliacoes" RENAME TO "Avaliacoes";

DROP TABLE "CorpoDocente";

ALTER TABLE "ef_temp_CorpoDocente" RENAME TO "CorpoDocente";

DROP TABLE "GradeCurricular";

ALTER TABLE "ef_temp_GradeCurricular" RENAME TO "GradeCurricular";

DROP TABLE "Matriculas";

ALTER TABLE "ef_temp_Matriculas" RENAME TO "Matriculas";

DROP TABLE "Materias";

ALTER TABLE "ef_temp_Materias" RENAME TO "Materias";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_Avaliacoes_GestorId" ON "Avaliacoes" ("GestorId");

CREATE INDEX "IX_Avaliacoes_GestorId1" ON "Avaliacoes" ("GestorId1");

CREATE INDEX "IX_CorpoDocente_GestorId" ON "CorpoDocente" ("GestorId");

CREATE INDEX "IX_CorpoDocente_GestorId1" ON "CorpoDocente" ("GestorId1");

CREATE INDEX "IX_CorpoDocente_MateriaId" ON "CorpoDocente" ("MateriaId");

CREATE INDEX "IX_GradeCurricular_GestorId" ON "GradeCurricular" ("GestorId");

CREATE INDEX "IX_GradeCurricular_GestorId1" ON "GradeCurricular" ("GestorId1");

CREATE INDEX "IX_GradeCurricular_MateriaId" ON "GradeCurricular" ("MateriaId");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_Matriculas_GestorId" ON "Matriculas" ("GestorId");

CREATE INDEX "IX_Matriculas_GestorId1" ON "Matriculas" ("GestorId1");

CREATE INDEX "IX_Materias_GestorId" ON "Materias" ("GestorId");

CREATE INDEX "IX_Materias_GestorId1" ON "Materias" ("GestorId1");

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_DocenteId1" ON "Turmas" ("DocenteId1");

CREATE INDEX "IX_Turmas_GestorId" ON "Turmas" ("GestorId");

CREATE INDEX "IX_Turmas_GestorId1" ON "Turmas" ("GestorId1");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

CREATE INDEX "IX_Turmas_MateriaId1" ON "Turmas" ("MateriaId1");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250421170624_FixMerge', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "TurmaAluno" RENAME TO "TurmaAlunos";

ALTER TABLE "GradeCurricular" RENAME TO "GradesCurriculares";

ALTER TABLE "Gestor" RENAME TO "Gestores";

ALTER TABLE "Docente" RENAME TO "Docentes";

ALTER TABLE "CorpoDocente" RENAME TO "CorposDocentes";

ALTER TABLE "Aluno" RENAME TO "Alunos";

DROP INDEX "IX_TurmaAluno_GestorId";

CREATE INDEX "IX_TurmaAlunos_GestorId" ON "TurmaAlunos" ("GestorId");

DROP INDEX "IX_TurmaAluno_AlunoId";

CREATE INDEX "IX_TurmaAlunos_AlunoId" ON "TurmaAlunos" ("AlunoId");

DROP INDEX "IX_GradeCurricular_MateriaId";

CREATE INDEX "IX_GradesCurriculares_MateriaId" ON "GradesCurriculares" ("MateriaId");

DROP INDEX "IX_GradeCurricular_GestorId1";

CREATE INDEX "IX_GradesCurriculares_GestorId1" ON "GradesCurriculares" ("GestorId1");

DROP INDEX "IX_GradeCurricular_GestorId";

CREATE INDEX "IX_GradesCurriculares_GestorId" ON "GradesCurriculares" ("GestorId");

DROP INDEX "IX_Gestor_GestorId1";

CREATE INDEX "IX_Gestores_GestorId1" ON "Gestores" ("GestorId1");

DROP INDEX "IX_Gestor_GestorId";

CREATE INDEX "IX_Gestores_GestorId" ON "Gestores" ("GestorId");

DROP INDEX "IX_Docente_GestorId1";

CREATE INDEX "IX_Docentes_GestorId1" ON "Docentes" ("GestorId1");

DROP INDEX "IX_Docente_GestorId";

CREATE INDEX "IX_Docentes_GestorId" ON "Docentes" ("GestorId");

DROP INDEX "IX_CorpoDocente_MateriaId";

CREATE INDEX "IX_CorposDocentes_MateriaId" ON "CorposDocentes" ("MateriaId");

DROP INDEX "IX_CorpoDocente_GestorId1";

CREATE INDEX "IX_CorposDocentes_GestorId1" ON "CorposDocentes" ("GestorId1");

DROP INDEX "IX_CorpoDocente_GestorId";

CREATE INDEX "IX_CorposDocentes_GestorId" ON "CorposDocentes" ("GestorId");

DROP INDEX "IX_Aluno_GestorId1";

CREATE INDEX "IX_Alunos_GestorId1" ON "Alunos" ("GestorId1");

DROP INDEX "IX_Aluno_GestorId";

CREATE INDEX "IX_Alunos_GestorId" ON "Alunos" ("GestorId");

CREATE TABLE "ef_temp_Alunos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL,
    CONSTRAINT "FK_Alunos_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Alunos_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Alunos" ("Id", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha"
FROM "Alunos";

CREATE TABLE "ef_temp_Avaliacoes" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("MateriaId", "AlunoId"),
    CONSTRAINT "FK_Avaliacoes_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Avaliacoes_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Avaliacoes_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Avaliacoes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Avaliacoes" ("MateriaId", "AlunoId", "GestorId", "GestorId1", "P1", "P2", "P3", "Status")
SELECT "MateriaId", "AlunoId", "GestorId", "GestorId1", "P1", "P2", "P3", "Status"
FROM "Avaliacoes";

CREATE TABLE "ef_temp_CorposDocentes" (
    "DocenteId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    CONSTRAINT "PK_CorposDocentes" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorposDocentes_Docentes_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docentes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CorposDocentes_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_CorposDocentes_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_CorposDocentes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_CorposDocentes" ("DocenteId", "MateriaId", "GestorId", "GestorId1")
SELECT "DocenteId", "MateriaId", "GestorId", "GestorId1"
FROM "CorposDocentes";

CREATE TABLE "ef_temp_Cursos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Cursos" PRIMARY KEY,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    CONSTRAINT "FK_Cursos_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Cursos_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Cursos" ("Id", "GestorId", "GestorId1", "Nome")
SELECT "Id", "GestorId", "GestorId1", "Nome"
FROM "Cursos";

CREATE TABLE "ef_temp_Docentes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Docentes" PRIMARY KEY,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL,
    CONSTRAINT "FK_Docentes_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Docentes_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Docentes" ("Id", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha"
FROM "Docentes";

CREATE TABLE "ef_temp_Gestores" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Gestores" PRIMARY KEY,
    "Cargo" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL,
    CONSTRAINT "FK_Gestores_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Gestores_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Gestores" ("Id", "Cargo", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "Cargo", "GestorId", "GestorId1", "Login", "Nome", "Sal", "Senha"
FROM "Gestores";

CREATE TABLE "ef_temp_GradesCurriculares" (
    "CursoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    CONSTRAINT "PK_GradesCurriculares" PRIMARY KEY ("CursoId", "MateriaId"),
    CONSTRAINT "FK_GradesCurriculares_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GradesCurriculares_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_GradesCurriculares_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_GradesCurriculares_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_GradesCurriculares" ("CursoId", "MateriaId", "GestorId", "GestorId1")
SELECT "CursoId", "MateriaId", "GestorId", "GestorId1"
FROM "GradesCurriculares";

CREATE TABLE "ef_temp_Materias" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Materias" PRIMARY KEY,
    "CargaHoraria" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    CONSTRAINT "FK_Materias_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Materias_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Materias" ("Id", "CargaHoraria", "GestorId", "GestorId1", "Nome")
SELECT "Id", "CargaHoraria", "GestorId", "GestorId1", "Nome"
FROM "Materias";

CREATE TABLE "ef_temp_Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "Modalidade" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("CursoId", "AlunoId"),
    CONSTRAINT "FK_Matriculas_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Matriculas_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id")
);

INSERT INTO "ef_temp_Matriculas" ("CursoId", "AlunoId", "GestorId", "GestorId1", "Modalidade", "Periodo")
SELECT "CursoId", "AlunoId", "GestorId", "GestorId1", "Modalidade", "Periodo"
FROM "Matriculas";

CREATE TABLE "ef_temp_TurmaAlunos" (
    "TurmaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "GestorId" INTEGER NULL,
    CONSTRAINT "PK_TurmaAlunos" PRIMARY KEY ("TurmaId", "AlunoId"),
    CONSTRAINT "FK_TurmaAlunos_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TurmaAlunos_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_TurmaAlunos_Turmas_TurmaId" FOREIGN KEY ("TurmaId") REFERENCES "Turmas" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TurmaAlunos" ("TurmaId", "AlunoId", "GestorId")
SELECT "TurmaId", "AlunoId", "GestorId"
FROM "TurmaAlunos";

CREATE TABLE "ef_temp_Turmas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY,
    "DocenteId" INTEGER NULL,
    "DocenteId1" INTEGER NULL,
    "GestorId" INTEGER NULL,
    "GestorId1" INTEGER NULL,
    "MateriaId" INTEGER NULL,
    "MateriaId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    "Turno" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Docentes_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docentes" ("Id"),
    CONSTRAINT "FK_Turmas_Docentes_DocenteId1" FOREIGN KEY ("DocenteId1") REFERENCES "Docentes" ("Id"),
    CONSTRAINT "FK_Turmas_Gestores_GestorId" FOREIGN KEY ("GestorId") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Turmas_Gestores_GestorId1" FOREIGN KEY ("GestorId1") REFERENCES "Gestores" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId1" FOREIGN KEY ("MateriaId1") REFERENCES "Materias" ("Id")
);

INSERT INTO "ef_temp_Turmas" ("Id", "DocenteId", "DocenteId1", "GestorId", "GestorId1", "MateriaId", "MateriaId1", "Nome", "Turno")
SELECT "Id", "DocenteId", "DocenteId1", "GestorId", "GestorId1", "MateriaId", "MateriaId1", "Nome", "Turno"
FROM "Turmas";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Alunos";

ALTER TABLE "ef_temp_Alunos" RENAME TO "Alunos";

DROP TABLE "Avaliacoes";

ALTER TABLE "ef_temp_Avaliacoes" RENAME TO "Avaliacoes";

DROP TABLE "CorposDocentes";

ALTER TABLE "ef_temp_CorposDocentes" RENAME TO "CorposDocentes";

DROP TABLE "Cursos";

ALTER TABLE "ef_temp_Cursos" RENAME TO "Cursos";

DROP TABLE "Docentes";

ALTER TABLE "ef_temp_Docentes" RENAME TO "Docentes";

DROP TABLE "Gestores";

ALTER TABLE "ef_temp_Gestores" RENAME TO "Gestores";

DROP TABLE "GradesCurriculares";

ALTER TABLE "ef_temp_GradesCurriculares" RENAME TO "GradesCurriculares";

DROP TABLE "Materias";

ALTER TABLE "ef_temp_Materias" RENAME TO "Materias";

DROP TABLE "Matriculas";

ALTER TABLE "ef_temp_Matriculas" RENAME TO "Matriculas";

DROP TABLE "TurmaAlunos";

ALTER TABLE "ef_temp_TurmaAlunos" RENAME TO "TurmaAlunos";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Alunos_GestorId" ON "Alunos" ("GestorId");

CREATE INDEX "IX_Alunos_GestorId1" ON "Alunos" ("GestorId1");

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_Avaliacoes_GestorId" ON "Avaliacoes" ("GestorId");

CREATE INDEX "IX_Avaliacoes_GestorId1" ON "Avaliacoes" ("GestorId1");

CREATE INDEX "IX_CorposDocentes_GestorId" ON "CorposDocentes" ("GestorId");

CREATE INDEX "IX_CorposDocentes_GestorId1" ON "CorposDocentes" ("GestorId1");

CREATE INDEX "IX_CorposDocentes_MateriaId" ON "CorposDocentes" ("MateriaId");

CREATE INDEX "IX_Cursos_GestorId" ON "Cursos" ("GestorId");

CREATE INDEX "IX_Cursos_GestorId1" ON "Cursos" ("GestorId1");

CREATE INDEX "IX_Docentes_GestorId" ON "Docentes" ("GestorId");

CREATE INDEX "IX_Docentes_GestorId1" ON "Docentes" ("GestorId1");

CREATE INDEX "IX_Gestores_GestorId" ON "Gestores" ("GestorId");

CREATE INDEX "IX_Gestores_GestorId1" ON "Gestores" ("GestorId1");

CREATE INDEX "IX_GradesCurriculares_GestorId" ON "GradesCurriculares" ("GestorId");

CREATE INDEX "IX_GradesCurriculares_GestorId1" ON "GradesCurriculares" ("GestorId1");

CREATE INDEX "IX_GradesCurriculares_MateriaId" ON "GradesCurriculares" ("MateriaId");

CREATE INDEX "IX_Materias_GestorId" ON "Materias" ("GestorId");

CREATE INDEX "IX_Materias_GestorId1" ON "Materias" ("GestorId1");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_Matriculas_GestorId" ON "Matriculas" ("GestorId");

CREATE INDEX "IX_Matriculas_GestorId1" ON "Matriculas" ("GestorId1");

CREATE INDEX "IX_TurmaAlunos_AlunoId" ON "TurmaAlunos" ("AlunoId");

CREATE INDEX "IX_TurmaAlunos_GestorId" ON "TurmaAlunos" ("GestorId");

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_DocenteId1" ON "Turmas" ("DocenteId1");

CREATE INDEX "IX_Turmas_GestorId" ON "Turmas" ("GestorId");

CREATE INDEX "IX_Turmas_GestorId1" ON "Turmas" ("GestorId1");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

CREATE INDEX "IX_Turmas_MateriaId1" ON "Turmas" ("MateriaId1");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250423022720_NameTables', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

DROP INDEX "IX_Turmas_GestorId";

DROP INDEX "IX_Turmas_GestorId1";

DROP INDEX "IX_TurmaAlunos_GestorId";

DROP INDEX "IX_Matriculas_GestorId";

DROP INDEX "IX_Matriculas_GestorId1";

DROP INDEX "IX_Materias_GestorId";

DROP INDEX "IX_Materias_GestorId1";

DROP INDEX "IX_GradesCurriculares_GestorId";

DROP INDEX "IX_GradesCurriculares_GestorId1";

DROP INDEX "IX_Gestores_GestorId";

DROP INDEX "IX_Gestores_GestorId1";

DROP INDEX "IX_Docentes_GestorId";

DROP INDEX "IX_Docentes_GestorId1";

DROP INDEX "IX_Cursos_GestorId";

DROP INDEX "IX_Cursos_GestorId1";

DROP INDEX "IX_CorposDocentes_GestorId";

DROP INDEX "IX_CorposDocentes_GestorId1";

DROP INDEX "IX_Avaliacoes_GestorId";

DROP INDEX "IX_Avaliacoes_GestorId1";

DROP INDEX "IX_Alunos_GestorId";

DROP INDEX "IX_Alunos_GestorId1";

ALTER TABLE "Turmas" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Turmas" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "TurmaAlunos" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "TurmaAlunos" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Matriculas" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Matriculas" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Materias" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Materias" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "GradesCurriculares" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "GradesCurriculares" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Gestores" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Gestores" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Docentes" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Docentes" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Cursos" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Cursos" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "CorposDocentes" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "CorposDocentes" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Avaliacoes" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Avaliacoes" ADD "EditadoPor" TEXT NULL;

ALTER TABLE "Alunos" ADD "CriadoPor" TEXT NULL;

ALTER TABLE "Alunos" ADD "EditadoPor" TEXT NULL;

CREATE TABLE "ef_temp_Alunos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Alunos" PRIMARY KEY,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL
);

INSERT INTO "ef_temp_Alunos" ("Id", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha"
FROM "Alunos";

CREATE TABLE "ef_temp_Avaliacoes" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("MateriaId", "AlunoId"),
    CONSTRAINT "FK_Avaliacoes_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Avaliacoes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Avaliacoes" ("MateriaId", "AlunoId", "CriadoPor", "EditadoPor", "P1", "P2", "P3", "Status")
SELECT "MateriaId", "AlunoId", "CriadoPor", "EditadoPor", "P1", "P2", "P3", "Status"
FROM "Avaliacoes";

CREATE TABLE "ef_temp_CorposDocentes" (
    "DocenteId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_CorposDocentes" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorposDocentes_Docentes_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docentes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CorposDocentes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_CorposDocentes" ("DocenteId", "MateriaId", "CriadoPor", "EditadoPor")
SELECT "DocenteId", "MateriaId", "CriadoPor", "EditadoPor"
FROM "CorposDocentes";

CREATE TABLE "ef_temp_Cursos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Cursos" PRIMARY KEY,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Cursos" ("Id", "CriadoPor", "EditadoPor", "Nome")
SELECT "Id", "CriadoPor", "EditadoPor", "Nome"
FROM "Cursos";

CREATE TABLE "ef_temp_Docentes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Docentes" PRIMARY KEY,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL
);

INSERT INTO "ef_temp_Docentes" ("Id", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha"
FROM "Docentes";

CREATE TABLE "ef_temp_Gestores" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Gestores" PRIMARY KEY,
    "Cargo" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL
);

INSERT INTO "ef_temp_Gestores" ("Id", "Cargo", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha")
SELECT "Id", "Cargo", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha"
FROM "Gestores";

CREATE TABLE "ef_temp_GradesCurriculares" (
    "CursoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_GradesCurriculares" PRIMARY KEY ("CursoId", "MateriaId"),
    CONSTRAINT "FK_GradesCurriculares_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_GradesCurriculares_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_GradesCurriculares" ("CursoId", "MateriaId", "CriadoPor", "EditadoPor")
SELECT "CursoId", "MateriaId", "CriadoPor", "EditadoPor"
FROM "GradesCurriculares";

CREATE TABLE "ef_temp_Materias" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Materias" PRIMARY KEY,
    "CargaHoraria" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Materias" ("Id", "CargaHoraria", "CriadoPor", "EditadoPor", "Nome")
SELECT "Id", "CargaHoraria", "CriadoPor", "EditadoPor", "Nome"
FROM "Materias";

CREATE TABLE "ef_temp_Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Modalidade" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("CursoId", "AlunoId"),
    CONSTRAINT "FK_Matriculas_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Matriculas" ("CursoId", "AlunoId", "CriadoPor", "EditadoPor", "Modalidade", "Periodo")
SELECT "CursoId", "AlunoId", "CriadoPor", "EditadoPor", "Modalidade", "Periodo"
FROM "Matriculas";

CREATE TABLE "ef_temp_TurmaAlunos" (
    "TurmaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_TurmaAlunos" PRIMARY KEY ("TurmaId", "AlunoId"),
    CONSTRAINT "FK_TurmaAlunos_Alunos_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Alunos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TurmaAlunos_Turmas_TurmaId" FOREIGN KEY ("TurmaId") REFERENCES "Turmas" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TurmaAlunos" ("TurmaId", "AlunoId", "CriadoPor", "EditadoPor")
SELECT "TurmaId", "AlunoId", "CriadoPor", "EditadoPor"
FROM "TurmaAlunos";

CREATE TABLE "ef_temp_Turmas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY,
    "CriadoPor" TEXT NULL,
    "DocenteId" INTEGER NULL,
    "DocenteId1" INTEGER NULL,
    "EditadoPor" TEXT NULL,
    "MateriaId" INTEGER NULL,
    "MateriaId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    "Turno" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Docentes_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Docentes" ("Id"),
    CONSTRAINT "FK_Turmas_Docentes_DocenteId1" FOREIGN KEY ("DocenteId1") REFERENCES "Docentes" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId1" FOREIGN KEY ("MateriaId1") REFERENCES "Materias" ("Id")
);

INSERT INTO "ef_temp_Turmas" ("Id", "CriadoPor", "DocenteId", "DocenteId1", "EditadoPor", "MateriaId", "MateriaId1", "Nome", "Turno")
SELECT "Id", "CriadoPor", "DocenteId", "DocenteId1", "EditadoPor", "MateriaId", "MateriaId1", "Nome", "Turno"
FROM "Turmas";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Alunos";

ALTER TABLE "ef_temp_Alunos" RENAME TO "Alunos";

DROP TABLE "Avaliacoes";

ALTER TABLE "ef_temp_Avaliacoes" RENAME TO "Avaliacoes";

DROP TABLE "CorposDocentes";

ALTER TABLE "ef_temp_CorposDocentes" RENAME TO "CorposDocentes";

DROP TABLE "Cursos";

ALTER TABLE "ef_temp_Cursos" RENAME TO "Cursos";

DROP TABLE "Docentes";

ALTER TABLE "ef_temp_Docentes" RENAME TO "Docentes";

DROP TABLE "Gestores";

ALTER TABLE "ef_temp_Gestores" RENAME TO "Gestores";

DROP TABLE "GradesCurriculares";

ALTER TABLE "ef_temp_GradesCurriculares" RENAME TO "GradesCurriculares";

DROP TABLE "Materias";

ALTER TABLE "ef_temp_Materias" RENAME TO "Materias";

DROP TABLE "Matriculas";

ALTER TABLE "ef_temp_Matriculas" RENAME TO "Matriculas";

DROP TABLE "TurmaAlunos";

ALTER TABLE "ef_temp_TurmaAlunos" RENAME TO "TurmaAlunos";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_CorposDocentes_MateriaId" ON "CorposDocentes" ("MateriaId");

CREATE INDEX "IX_GradesCurriculares_MateriaId" ON "GradesCurriculares" ("MateriaId");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_TurmaAlunos_AlunoId" ON "TurmaAlunos" ("AlunoId");

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_DocenteId1" ON "Turmas" ("DocenteId1");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

CREATE INDEX "IX_Turmas_MateriaId1" ON "Turmas" ("MateriaId1");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250423033920_SimplifyTracking', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

DROP TABLE "Alunos";

DROP TABLE "Docentes";

ALTER TABLE "Gestores" RENAME TO "Usuarios";

ALTER TABLE "Usuarios" ADD "Tipo" TEXT NOT NULL DEFAULT '';

CREATE TABLE "ef_temp_Avaliacoes" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("MateriaId", "AlunoId"),
    CONSTRAINT "FK_Avaliacoes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Avaliacoes_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Avaliacoes" ("MateriaId", "AlunoId", "CriadoPor", "EditadoPor", "P1", "P2", "P3", "Status")
SELECT "MateriaId", "AlunoId", "CriadoPor", "EditadoPor", "P1", "P2", "P3", "Status"
FROM "Avaliacoes";

CREATE TABLE "ef_temp_CorposDocentes" (
    "DocenteId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_CorposDocentes" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorposDocentes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CorposDocentes_Usuarios_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_CorposDocentes" ("DocenteId", "MateriaId", "CriadoPor", "EditadoPor")
SELECT "DocenteId", "MateriaId", "CriadoPor", "EditadoPor"
FROM "CorposDocentes";

CREATE TABLE "ef_temp_Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Modalidade" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("CursoId", "AlunoId"),
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_Matriculas" ("CursoId", "AlunoId", "CriadoPor", "EditadoPor", "Modalidade", "Periodo")
SELECT "CursoId", "AlunoId", "CriadoPor", "EditadoPor", "Modalidade", "Periodo"
FROM "Matriculas";

CREATE TABLE "ef_temp_TurmaAlunos" (
    "TurmaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_TurmaAlunos" PRIMARY KEY ("TurmaId", "AlunoId"),
    CONSTRAINT "FK_TurmaAlunos_Turmas_TurmaId" FOREIGN KEY ("TurmaId") REFERENCES "Turmas" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TurmaAlunos_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_TurmaAlunos" ("TurmaId", "AlunoId", "CriadoPor", "EditadoPor")
SELECT "TurmaId", "AlunoId", "CriadoPor", "EditadoPor"
FROM "TurmaAlunos";

CREATE TABLE "ef_temp_Turmas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "CriadoPor" TEXT NULL,
    "DocenteId" INTEGER NULL,
    "DocenteId1" INTEGER NULL,
    "EditadoPor" TEXT NULL,
    "MateriaId" INTEGER NULL,
    "MateriaId1" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    "Turno" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Materias_MateriaId1" FOREIGN KEY ("MateriaId1") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Usuarios_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Usuarios" ("Id"),
    CONSTRAINT "FK_Turmas_Usuarios_DocenteId1" FOREIGN KEY ("DocenteId1") REFERENCES "Usuarios" ("Id")
);

INSERT INTO "ef_temp_Turmas" ("Id", "CriadoPor", "DocenteId", "DocenteId1", "EditadoPor", "MateriaId", "MateriaId1", "Nome", "Turno")
SELECT "Id", "CriadoPor", "DocenteId", "DocenteId1", "EditadoPor", "MateriaId", "MateriaId1", "Nome", "Turno"
FROM "Turmas";

CREATE TABLE "ef_temp_Usuarios" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Usuarios" PRIMARY KEY AUTOINCREMENT,
    "Cargo" INTEGER NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Login" TEXT NULL,
    "Nome" TEXT NOT NULL,
    "Sal" TEXT NULL,
    "Senha" TEXT NULL,
    "Tipo" TEXT NOT NULL
);

INSERT INTO "ef_temp_Usuarios" ("Id", "Cargo", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha", "Tipo")
SELECT "Id", "Cargo", "CriadoPor", "EditadoPor", "Login", "Nome", "Sal", "Senha", "Tipo"
FROM "Usuarios";

CREATE TABLE "ef_temp_Materias" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Materias" PRIMARY KEY AUTOINCREMENT,
    "CargaHoraria" INTEGER NOT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Materias" ("Id", "CargaHoraria", "CriadoPor", "EditadoPor", "Nome")
SELECT "Id", "CargaHoraria", "CriadoPor", "EditadoPor", "Nome"
FROM "Materias";

CREATE TABLE "ef_temp_Cursos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Cursos" PRIMARY KEY AUTOINCREMENT,
    "CriadoPor" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Cursos" ("Id", "CriadoPor", "EditadoPor", "Nome")
SELECT "Id", "CriadoPor", "EditadoPor", "Nome"
FROM "Cursos";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Avaliacoes";

ALTER TABLE "ef_temp_Avaliacoes" RENAME TO "Avaliacoes";

DROP TABLE "CorposDocentes";

ALTER TABLE "ef_temp_CorposDocentes" RENAME TO "CorposDocentes";

DROP TABLE "Matriculas";

ALTER TABLE "ef_temp_Matriculas" RENAME TO "Matriculas";

DROP TABLE "TurmaAlunos";

ALTER TABLE "ef_temp_TurmaAlunos" RENAME TO "TurmaAlunos";

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

DROP TABLE "Usuarios";

ALTER TABLE "ef_temp_Usuarios" RENAME TO "Usuarios";

DROP TABLE "Materias";

ALTER TABLE "ef_temp_Materias" RENAME TO "Materias";

DROP TABLE "Cursos";

ALTER TABLE "ef_temp_Cursos" RENAME TO "Cursos";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_CorposDocentes_MateriaId" ON "CorposDocentes" ("MateriaId");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_TurmaAlunos_AlunoId" ON "TurmaAlunos" ("AlunoId");

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_DocenteId1" ON "Turmas" ("DocenteId1");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

CREATE INDEX "IX_Turmas_MateriaId1" ON "Turmas" ("MateriaId1");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250426223503_UnifyUsers', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

DROP INDEX "IX_Turmas_DocenteId1";

DROP INDEX "IX_Turmas_MateriaId1";

ALTER TABLE "Usuarios" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Usuarios" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "Turmas" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Turmas" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "TurmaAlunos" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "TurmaAlunos" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "Matriculas" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Matriculas" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "Materias" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Materias" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "GradesCurriculares" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "GradesCurriculares" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "Cursos" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Cursos" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "CorposDocentes" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "CorposDocentes" ADD "EditadoEm" TEXT NULL;

ALTER TABLE "Avaliacoes" ADD "CriadoEm" TEXT NULL;

ALTER TABLE "Avaliacoes" ADD "EditadoEm" TEXT NULL;

CREATE TABLE "AlunoCurso" (
    "AlunosId" INTEGER NOT NULL,
    "CursosId" INTEGER NOT NULL,
    CONSTRAINT "PK_AlunoCurso" PRIMARY KEY ("AlunosId", "CursosId"),
    CONSTRAINT "FK_AlunoCurso_Cursos_CursosId" FOREIGN KEY ("CursosId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AlunoCurso_Usuarios_AlunosId" FOREIGN KEY ("AlunosId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AlunoMateria" (
    "AlunosId" INTEGER NOT NULL,
    "MateriasId" INTEGER NOT NULL,
    CONSTRAINT "PK_AlunoMateria" PRIMARY KEY ("AlunosId", "MateriasId"),
    CONSTRAINT "FK_AlunoMateria_Materias_MateriasId" FOREIGN KEY ("MateriasId") REFERENCES "Materias" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AlunoMateria_Usuarios_AlunosId" FOREIGN KEY ("AlunosId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AlunoTurma" (
    "AlunosId" INTEGER NOT NULL,
    "TurmasId" INTEGER NOT NULL,
    CONSTRAINT "PK_AlunoTurma" PRIMARY KEY ("AlunosId", "TurmasId"),
    CONSTRAINT "FK_AlunoTurma_Turmas_TurmasId" FOREIGN KEY ("TurmasId") REFERENCES "Turmas" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AlunoTurma_Usuarios_AlunosId" FOREIGN KEY ("AlunosId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CursoMateria" (
    "CursosId" INTEGER NOT NULL,
    "MateriasId" INTEGER NOT NULL,
    CONSTRAINT "PK_CursoMateria" PRIMARY KEY ("CursosId", "MateriasId"),
    CONSTRAINT "FK_CursoMateria_Cursos_CursosId" FOREIGN KEY ("CursosId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CursoMateria_Materias_MateriasId" FOREIGN KEY ("MateriasId") REFERENCES "Materias" ("Id") ON DELETE CASCADE
);

CREATE TABLE "DocenteMateria" (
    "DocentesId" INTEGER NOT NULL,
    "MateriasId" INTEGER NOT NULL,
    CONSTRAINT "PK_DocenteMateria" PRIMARY KEY ("DocentesId", "MateriasId"),
    CONSTRAINT "FK_DocenteMateria_Materias_MateriasId" FOREIGN KEY ("MateriasId") REFERENCES "Materias" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_DocenteMateria_Usuarios_DocentesId" FOREIGN KEY ("DocentesId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AlunoCurso_CursosId" ON "AlunoCurso" ("CursosId");

CREATE INDEX "IX_AlunoMateria_MateriasId" ON "AlunoMateria" ("MateriasId");

CREATE INDEX "IX_AlunoTurma_TurmasId" ON "AlunoTurma" ("TurmasId");

CREATE INDEX "IX_CursoMateria_MateriasId" ON "CursoMateria" ("MateriasId");

CREATE INDEX "IX_DocenteMateria_MateriasId" ON "DocenteMateria" ("MateriasId");

CREATE TABLE "ef_temp_Turmas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Turmas" PRIMARY KEY AUTOINCREMENT,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "DocenteId" INTEGER NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "MateriaId" INTEGER NULL,
    "Nome" TEXT NOT NULL,
    "Turno" INTEGER NOT NULL,
    CONSTRAINT "FK_Turmas_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id"),
    CONSTRAINT "FK_Turmas_Usuarios_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Usuarios" ("Id")
);

INSERT INTO "ef_temp_Turmas" ("Id", "CriadoEm", "CriadoPor", "DocenteId", "EditadoEm", "EditadoPor", "MateriaId", "Nome", "Turno")
SELECT "Id", "CriadoEm", "CriadoPor", "DocenteId", "EditadoEm", "EditadoPor", "MateriaId", "Nome", "Turno"
FROM "Turmas";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Turmas";

ALTER TABLE "ef_temp_Turmas" RENAME TO "Turmas";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Turmas_DocenteId" ON "Turmas" ("DocenteId");

CREATE INDEX "IX_Turmas_MateriaId" ON "Turmas" ("MateriaId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250525143134_Fix-ShadowProperties', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

CREATE UNIQUE INDEX "IX_Usuarios_Login" ON "Usuarios" ("Login");

CREATE UNIQUE INDEX "IX_Materias_Nome" ON "Materias" ("Nome");

CREATE UNIQUE INDEX "IX_Cursos_Nome" ON "Cursos" ("Nome");

CREATE TABLE "ef_temp_Materias" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Materias" PRIMARY KEY AUTOINCREMENT,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Nome" TEXT NOT NULL
);

INSERT INTO "ef_temp_Materias" ("Id", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "Nome")
SELECT "Id", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "Nome"
FROM "Materias";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Materias";

ALTER TABLE "ef_temp_Materias" RENAME TO "Materias";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE UNIQUE INDEX "IX_Materias_Nome" ON "Materias" ("Nome");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250601015203_Update-Restrictions', '8.0.14');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "ef_temp_Avaliacoes" (
    "MateriaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "P1" REAL NULL,
    "P2" REAL NULL,
    "P3" REAL NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "PK_Avaliacoes" PRIMARY KEY ("MateriaId", "AlunoId"),
    CONSTRAINT "FK_Avaliacoes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Avaliacoes_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_Avaliacoes" ("MateriaId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "P1", "P2", "P3", "Status")
SELECT "MateriaId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "P1", "P2", "P3", "Status"
FROM "Avaliacoes";

CREATE TABLE "ef_temp_CorposDocentes" (
    "DocenteId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_CorposDocentes" PRIMARY KEY ("DocenteId", "MateriaId"),
    CONSTRAINT "FK_CorposDocentes_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_CorposDocentes_Usuarios_DocenteId" FOREIGN KEY ("DocenteId") REFERENCES "Usuarios" ("Id") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_CorposDocentes" ("DocenteId", "MateriaId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor")
SELECT "DocenteId", "MateriaId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor"
FROM "CorposDocentes";

CREATE TABLE "ef_temp_GradesCurriculares" (
    "CursoId" INTEGER NOT NULL,
    "MateriaId" INTEGER NOT NULL,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_GradesCurriculares" PRIMARY KEY ("CursoId", "MateriaId"),
    CONSTRAINT "FK_GradesCurriculares_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_GradesCurriculares_Materias_MateriaId" FOREIGN KEY ("MateriaId") REFERENCES "Materias" ("Id") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_GradesCurriculares" ("CursoId", "MateriaId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor")
SELECT "CursoId", "MateriaId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor"
FROM "GradesCurriculares";

CREATE TABLE "ef_temp_Matriculas" (
    "CursoId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    "Modalidade" INTEGER NOT NULL,
    "Periodo" INTEGER NOT NULL,
    CONSTRAINT "PK_Matriculas" PRIMARY KEY ("CursoId", "AlunoId"),
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_Matriculas_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_Matriculas" ("CursoId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "Modalidade", "Periodo")
SELECT "CursoId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor", "Modalidade", "Periodo"
FROM "Matriculas";

CREATE TABLE "ef_temp_TurmaAlunos" (
    "TurmaId" INTEGER NOT NULL,
    "AlunoId" INTEGER NOT NULL,
    "CriadoEm" TEXT NULL,
    "CriadoPor" TEXT NULL,
    "EditadoEm" TEXT NULL,
    "EditadoPor" TEXT NULL,
    CONSTRAINT "PK_TurmaAlunos" PRIMARY KEY ("TurmaId", "AlunoId"),
    CONSTRAINT "FK_TurmaAlunos_Turmas_TurmaId" FOREIGN KEY ("TurmaId") REFERENCES "Turmas" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_TurmaAlunos_Usuarios_AlunoId" FOREIGN KEY ("AlunoId") REFERENCES "Usuarios" ("Id") ON DELETE RESTRICT
);

INSERT INTO "ef_temp_TurmaAlunos" ("TurmaId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor")
SELECT "TurmaId", "AlunoId", "CriadoEm", "CriadoPor", "EditadoEm", "EditadoPor"
FROM "TurmaAlunos";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "Avaliacoes";

ALTER TABLE "ef_temp_Avaliacoes" RENAME TO "Avaliacoes";

DROP TABLE "CorposDocentes";

ALTER TABLE "ef_temp_CorposDocentes" RENAME TO "CorposDocentes";

DROP TABLE "GradesCurriculares";

ALTER TABLE "ef_temp_GradesCurriculares" RENAME TO "GradesCurriculares";

DROP TABLE "Matriculas";

ALTER TABLE "ef_temp_Matriculas" RENAME TO "Matriculas";

DROP TABLE "TurmaAlunos";

ALTER TABLE "ef_temp_TurmaAlunos" RENAME TO "TurmaAlunos";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_Avaliacoes_AlunoId" ON "Avaliacoes" ("AlunoId");

CREATE INDEX "IX_CorposDocentes_MateriaId" ON "CorposDocentes" ("MateriaId");

CREATE INDEX "IX_GradesCurriculares_MateriaId" ON "GradesCurriculares" ("MateriaId");

CREATE INDEX "IX_Matriculas_AlunoId" ON "Matriculas" ("AlunoId");

CREATE INDEX "IX_TurmaAlunos_AlunoId" ON "TurmaAlunos" ("AlunoId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250601021331_Fix-DeleteBehavior', '8.0.14');

COMMIT;


