using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioMatriculas
{
	public RespostaRecurso<IEnumerable<Matricula>> ObterPorAluno(ulong alunoId);
	public RespostaRecurso<IEnumerable<Matricula>> ObterPorCurso(ulong cursoId);
}
