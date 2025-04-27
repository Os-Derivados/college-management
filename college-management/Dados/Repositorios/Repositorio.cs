using System.Linq.Expressions;
using System.Text.Json;
using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Repositorios;


public abstract class Repositorio<T> : IRepositorio<T> where T : Modelo
{
	private protected readonly BancoDeDados BancoDeDados;
	private protected readonly DbSet<T>     Dados;

	protected Repositorio(BancoDeDados bancoDeDados)
	{
		BancoDeDados = bancoDeDados;
		Dados        = BancoDeDados.Set<T>();
	}

	public virtual async Task<RespostaRecurso<T>> Adicionar(T modelo)
	{
		if (Existe(modelo))
			return new RespostaRecurso<T>(modelo, StatusResposta.ErroDuplicata);

		Dados.Add(modelo);
		await BancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public RespostaRecurso<IEnumerable<T>> ObterTodos()
	{
		var registros = Dados.AsNoTracking().ToList();

		return new RespostaRecurso<IEnumerable<T>>(registros,
		                                           StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorId(uint id)
	{
		var registro = Dados.AsNoTracking().FirstOrDefault(r => r.Id == id);

		return registro is null
			? new RespostaRecurso<T>(null, StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorNome(string? nome)
	{
		var registro = Dados.AsNoTracking().FirstOrDefault(r => r.Nome == nome);

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

		Dados.Update(modelo);
		await BancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<T>> Remover(uint id)
	{
		var modelo = Dados.FirstOrDefault(t => t.Id == id);

		if (modelo is null)
		{
			return new RespostaRecurso<T>(null,
			                              StatusResposta.ErroNaoEncontrado);
		}

		Dados.Remove(modelo);
		await BancoDeDados.SaveChangesAsync();

		return new RespostaRecurso<T>(null, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<IEnumerable<T>>> Buscar(
		Expression<Func<T, bool>> callback)
	{
		var registros = await Dados.AsNoTracking()
		                           .Where(callback)
		                           .ToListAsync();

		if (registros.Count == 0)
		{
			return new RespostaRecurso<IEnumerable<T>>(
				null,
				StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<IEnumerable<T>>(registros,
		                                           StatusResposta.Sucesso);
	}

	public abstract bool Existe(T modelo);
}
