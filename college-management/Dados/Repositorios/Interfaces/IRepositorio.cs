using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorio<T> where T : Modelo
{
	public Task<RespostaRecurso<T>> Adicionar(T modelo);

	public RespostaRecurso<List<T>> ObterTodos();

	public RespostaRecurso<T> ObterPorId(string? id);

	public RespostaRecurso<T> ObterPorNome(string? nome);

	public Task<RespostaRecurso<T>> Atualizar(T modelo);

	public Task<bool> Remover(string? id);

	public bool Existe(T modelo);
}
