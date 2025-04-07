using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Dados;


public sealed class BaseDeDados
{
	public BaseDeDados(RepositorioAvaliacoes avaliacoes,
	                   RepositorioCursos cursos,
	                   RepositorioMaterias materias,
	                   RepositorioMatriculas matriculas,
	                   RepositorioUsuarios usuarios)
	{
		Avaliacoes = avaliacoes;
		Cursos     = cursos;
		Materias   = materias;
		Matriculas = matriculas;
		Usuarios   = usuarios;
	}

	public readonly RepositorioAvaliacoes Avaliacoes;
	public readonly RepositorioCursos     Cursos;
	public readonly RepositorioMaterias   Materias;
	public readonly RepositorioMatriculas Matriculas;
	public readonly RepositorioUsuarios   Usuarios;
}
