namespace college_management.Constantes;


public static class AcessosContexto
{
	public const string ContextoCursos   = "Cursos";
	public const string ContextoCargos   = "Cargos";
	public const string ContextoMaterias = "Matérias";
	public const string ContextoUsuarios = "Contas";

	public static readonly string[] AcessoAlunos =
	[
		ContextoCursos,
		ContextoMaterias,
		ContextoUsuarios
	];

	public static readonly string[] AcessoGestoresAdministradores
		=
		[
			..AcessoAlunos,
			ContextoCargos
		];
}
