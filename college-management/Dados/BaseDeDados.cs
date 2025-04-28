using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Dados;


public sealed class BaseDeDados
{
	public BaseDeDados(RepositorioCursos cursos,
	                   RepositorioMaterias materias,
	                   RepositorioUsuarios usuarios)
	{
		Cursos     = cursos;
		Materias   = materias;
		Usuarios   = usuarios;
	}

	public readonly RepositorioCursos     Cursos;
	public readonly RepositorioMaterias   Materias;
	public readonly RepositorioUsuarios   Usuarios;
}
