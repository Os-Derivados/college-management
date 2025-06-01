using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioMaterias : IRepositorio<Materia>
{
	public RespostaRecurso<IEnumerable<Materia>> ObterGradeCurricular(uint cursoId);
}
