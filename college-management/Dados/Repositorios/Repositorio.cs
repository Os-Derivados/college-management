using System.Linq.Expressions;
using System.Text.Json;
using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Repositorios;


public abstract class Repositorio<T> : IRepositorio<T> where T : Modelo
{
	private readonly BancoDeDados _bancoDeDados;
	private readonly DbSet<T>     _dados;

	protected Repositorio(BancoDeDados bancoDeDados)
	{
		_bancoDeDados = bancoDeDados;
		_dados        = _bancoDeDados.Set<T>();
	}

	public virtual async Task<RespostaRecurso<T>> Adicionar(T modelo)
	{
		if (Existe(modelo))
			return new RespostaRecurso<T>(modelo, StatusResposta.ErroDuplicata);

		_dados.Add(modelo);
		await _bancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public RespostaRecurso<IEnumerable<T>> ObterTodos()
	{
		var registros = _dados.ToList();

		return new RespostaRecurso<IEnumerable<T>>(registros,
		                                           StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorId(uint id)
	{
		var registro = _dados.Find(id);

		return registro is null
			? new RespostaRecurso<T>(null, StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorNome(string? nome)
	{
		var registro = _dados.FirstOrDefault(r => r.Nome == nome);

		return registro is null
			? new RespostaRecurso<T>(null, StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<T>> Atualizar(T modelo)
	{
		var modeloAntigo = ObterPorId(modelo.Id);

		if (modeloAntigo.Status is StatusResposta.ErroNaoEncontrado)
		{
			return await Adicionar(modelo);
		}

		_dados.Update(modelo);
		await _bancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<T>> Remover(uint id)
	{
		var (modelo, statusResposta) = ObterPorId(id);

		if (statusResposta is StatusResposta.ErroNaoEncontrado)
			return new RespostaRecurso<T>(null,
			                              StatusResposta.ErroNaoEncontrado);

		_dados.Remove(modelo!);
		await _bancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<IEnumerable<T>>> Buscar(
		Expression<Func<T, bool>> callback)
	{
		var registros = await _dados.Where(callback).ToListAsync();

		if (registros.Count == 0)
			return new RespostaRecurso<IEnumerable<T>>(null,
				StatusResposta.ErroNaoEncontrado);

		return new RespostaRecurso<IEnumerable<T>>(registros,
		                                           StatusResposta.Sucesso);
	}

	public abstract bool Existe(T modelo);
}
