using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioCursos
{
	public RespostaRecurso<Curso> ObterComMaterias(
		uint? cursoId = null,
		string? nomeCurso = null);
}
