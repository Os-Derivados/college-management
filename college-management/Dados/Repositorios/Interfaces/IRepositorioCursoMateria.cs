using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioCursoMateria
{
	public RespostaRecurso<IEnumerable<CursoMateria>> ObterPorCurso(ulong cursoId);
	public RespostaRecurso<IEnumerable<CursoMateria>> ObterPorMateria(ulong materiaId);
}
