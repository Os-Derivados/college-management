using System.Linq.Expressions;
using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorio<T> where T : Modelo
{
	public Task<RespostaRecurso<T>> Adicionar(T modelo);

	public RespostaRecurso<IEnumerable<T>> ObterTodos();

	public RespostaRecurso<T> ObterPorId(uint id);

	public RespostaRecurso<T> ObterPorNome(string? nome);

	public Task<RespostaRecurso<T>> Atualizar(T modelo);

	public Task<RespostaRecurso<T>> Remover(uint id);

	public Task<RespostaRecurso<IEnumerable<T>>> Buscar(
		Expression<Func<T, bool>> callback);

	public bool Existe(T modelo);
}
