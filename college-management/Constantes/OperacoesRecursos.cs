namespace college_management.Constantes;


public static class OperacoesRecursos
{
	public static readonly string[] RecursosLeitura =
	[
		"Visualizar",
		"Gerar Relatorio"
	];

	public static readonly string[] RecursosEscrita =
	[
		"Cadastrar",
		"Editar",
		"Excluir"
	];

	public static readonly string[] RecursosLeituraUsuarios =
	[
		..RecursosLeitura,
		"Ver Matricula",
		"Ver Notas",
		"Ver Financeiro"
	];

	public static readonly string[] RecursosEscritaUsuarios =
	[
		..RecursosLeituraUsuarios,
		..RecursosEscrita,
		"Editar Matricula"
	];

	public static readonly string[] RecursosLeituraCursos =
	[
		..RecursosLeitura,
		"Ver Grade Horaria",
		"Ver Grade Curricular"
	];

	public static readonly string[] RecursosEscritaCursos =
	[
		..RecursosLeituraUsuarios,
		..RecursosEscrita
	];
}
