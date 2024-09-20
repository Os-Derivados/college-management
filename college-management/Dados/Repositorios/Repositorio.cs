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

	public virtual async Task Adicionar(T modelo)
	{
		if (Existe(modelo)) return;

		BaseDeDados.Add(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);
	}

	public List<T> ObterTodos() { return BaseDeDados; }

	public T ObterPorId(string? id)
	{
		return BaseDeDados.FirstOrDefault(t => t.Id == id);
	}

	public async Task Atualizar(T modelo)
	{
		var modeloAntigo = ObterPorId(modelo.Id);

		if (modeloAntigo is null)
		{
			await Adicionar(modelo);

			return;
		}

		await Remover(modelo.Id);
		await Adicionar(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);
	}

	public async Task Remover(string? id)
	{
		var modelo = ObterPorId(id);

		if (modelo is null)
			return;

		BaseDeDados.Remove(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);
	}

	public abstract bool Existe(T modelo);
}
