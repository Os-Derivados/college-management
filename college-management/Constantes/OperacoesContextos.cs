namespace college_management.Constantes;


public static class OperacoesContextos
{
	public static readonly string[] RecursosLeitura =
	[
		"Visualizar",
		"Ver Detalhes"
	];

	public static readonly string[] RecursosEscrita =
	[
		"Cadastrar",
		"Editar",
		"Excluir",
		"Gerar Relatorio"
	];

	public static readonly string[] RecursosLeituraUsuarios =
	[
		..RecursosLeitura,
	];

	public static readonly string[] RecursosEscritaUsuarios =
	[
		..RecursosLeituraUsuarios,
		..RecursosEscrita,
		"Cadastrar Gestor",
		"Cadastrar Docente",
		"Cadastrar Aluno"
	];

	public static readonly string[] RecursosLeituraCursos =
	[
		..RecursosLeitura,
		"Ver Matriculas",
		"Pesuisar Matricula",
		"Ver Grade Curricular",
	];

	public static readonly string[] RecursosEscritaCursos =
	[
		..RecursosLeituraCursos,
		..RecursosEscrita,
		"Criar Matricula",
		"Editar Matricula",
		"Criar Grade Curricular",
		"Editar Grade Curricular"
	];

	public static readonly string[] RecursosLeituraMaterias = [
		..RecursosLeitura,
		"Ver Notas",
		"Ver Corpo Docente"
	];

	public static readonly string[] RecursosEscritaMaterias = [
		..RecursosLeituraMaterias,
		"Lancar Nota",
		"Editar Nota",
		"Criar Corpo Docente",
		"Editar Corpo Docente"
	];

	public static readonly string[] RecursosLeituraTurmas = [
		..RecursosLeitura,
	];

	public static readonly string[] RecursosEscritaTurmas = [
		..RecursosLeituraTurmas,
		..RecursosEscrita,
	];
}
