using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioMaterias
{
	public RespostaRecurso<IEnumerable<Materia>> ObterGradeCurricular(uint cursoId);
}
