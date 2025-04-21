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


