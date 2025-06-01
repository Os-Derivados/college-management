using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioUsuarios : IRepositorio<Usuario>
{
	public RespostaRecurso<Usuario> ObterPorLogin(string login);
	public RespostaRecurso<Aluno>   ObterAluno(uint id);
	public RespostaRecurso<IEnumerable<Aluno>> ObterPorTurma(uint turmaId);
}
