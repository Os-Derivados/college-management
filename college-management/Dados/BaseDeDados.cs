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



	public object EscolherBaseDeDados<T>() where T : Modelo
	{
		switch(typeof(T))
		{
			case Type cargo     when cargo	   == typeof(Cargo):
				return Cargos.ObterBaseDeDados();

			case Type curso     when curso     == typeof(Curso):
				return Cursos.ObterBaseDeDados();

            case Type materia   when materia   == typeof(Materia):
                return Materias.ObterBaseDeDados();

            case Type matricula when matricula == typeof(Matricula):
                return Matriculas.ObterBaseDeDados();

            case Type usuario   when usuario   == typeof(Usuario):
                return Usuarios.ObterBaseDeDados();

			default:
				throw new NotImplementedException($"Error: Tipo não implementado. (BaseDeDados.EscolherBaseDeDados)");
        }
	}
}
