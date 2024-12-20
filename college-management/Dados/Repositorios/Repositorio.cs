using System.Text.Json;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos;


namespace college_management.Dados.Repositorios;


public abstract class Repositorio<T> : IRepositorio<T>
where T : Modelo
{
	protected          List<T>?        BaseDeDados;
	protected readonly ServicoDados<T> _servicoDados = new();

	protected Repositorio()
	{
		if (BaseDeDados is not null)
			return;

		Task.Run(async () =>
		    {
			    try
			    {
				    using Task<List<T>>? dadosSalvos =
					    _servicoDados.CarregarAssincrono();

				    BaseDeDados = await dadosSalvos;
			    }
			    catch (Exception e) when (e is JsonException
			                                   or AggregateException
			                                   or IOException)
			    {
				    BaseDeDados = [];
			    }
		    })
		    .Wait();
	}

	public virtual async Task<RespostaRecurso<T>> Adicionar(T modelo)
	{
		if (Existe(modelo)) return false;

		BaseDeDados.Add(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return true;
	}

	public RespostaRecurso<List<T>> ObterTodos() { return BaseDeDados; }

	public RespostaRecurso<T> ObterPorId(string? id) { return BaseDeDados.FirstOrDefault(t => t.Id == id); }

	public RespostaRecurso<T> ObterPorNome(string? nome)
	{
		return BaseDeDados.FirstOrDefault(t =>
		{
			var propriedadeNome
				= t.GetType().GetProperty("Nome");

			var valorNome
				= propriedadeNome.GetValue(t).ToString();

			return valorNome is not null
			       && valorNome == nome;
		});
	}

	public async Task<RespostaRecurso<T>> Atualizar(T modelo)
	{
		var modeloAntigo = ObterPorId(modelo.Id);

		if (modeloAntigo is null) return await Adicionar(modelo);

		var foiRemovido = await Remover(modelo.Id);

		if (!foiRemovido) return false;

		await Adicionar(modelo);
		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return true;
	}

	public async Task<RespostaRecurso<T>> Remover(string? id)
	{
		var modelo = ObterPorId(id);

		if (modelo is null)
			return false;

		BaseDeDados.Remove(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return true;
	}

	public abstract bool Existe(T modelo);
}
