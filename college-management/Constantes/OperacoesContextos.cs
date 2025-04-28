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
		"Ver Matricula",
		"Ver Notas"
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
		"Ver Grade Curricular"
	];

	public static readonly string[] RecursosEscritaCursos =
	[
		..RecursosLeituraCursos,
		..RecursosEscrita
	];
}
