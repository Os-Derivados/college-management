using college_management.Dados.Repositorios;


namespace college_management.Dados;


public sealed class BaseDeDados
{
	public readonly RepositorioUsuarios usuarios = new();
	public readonly RepositorioCargos   cargos   = new();
	public readonly RepositorioCursos   cursos   = new();
	public readonly RepositorioMaterias materias = new();
}
