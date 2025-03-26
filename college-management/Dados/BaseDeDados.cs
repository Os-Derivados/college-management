using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Dados;


public sealed class BaseDeDados
{
	public readonly RepositorioCargos     Cargos     = new();
	public readonly RepositorioCursos     Cursos     = new();
	public readonly RepositorioMaterias   Materias   = new();
	public readonly RepositorioMatriculas Matriculas = new();
	public readonly RepositorioUsuarios   Usuarios   = new();
}
