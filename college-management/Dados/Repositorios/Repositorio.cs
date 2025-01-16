using System.Text.Json;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos;


namespace college_management.Dados.Repositorios;


public abstract class Repositorio<T> : IRepositorio<T>
	where T : Modelo
{
	protected readonly ServicoDados<T> _servicoDados = new();
	protected          List<T>?        BaseDeDados;

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
		if (Existe(modelo))
			return new RespostaRecurso<T>(modelo, StatusResposta.ErroDuplicata);

		BaseDeDados.Add(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public RespostaRecurso<List<T>> ObterTodos()
	{
		return new RespostaRecurso<List<T>>(BaseDeDados ?? [],
		                                    StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorId(string? id)
	{
		var registro = BaseDeDados.FirstOrDefault(t => t.Id == id);

		return registro is null
			? new RespostaRecurso<T>(null, StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorNome(string? nome)
	{
		var registro = BaseDeDados?.FirstOrDefault(t =>
		{
			var propriedadeNome
				= t.GetType().GetProperty("Nome");

			var valorNome
				= propriedadeNome.GetValue(t).ToString();

			return valorNome is not null
			       && valorNome == nome;
		});

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

		var foiRemovido = await Remover(modelo.Id);

		if (!foiRemovido)
			return new RespostaRecurso<T>(modelo, StatusResposta.ErroInterno);

		var atualizar = await Adicionar(modelo);

		if (atualizar.Status is StatusResposta.Sucesso)
			await _servicoDados.SalvarAssicrono(BaseDeDados);

		return atualizar;
	}

	public async Task<bool> Remover(string? id)
	{
		var obterModelo = ObterPorId(id);

		if (obterModelo.Status is StatusResposta.ErroNaoEncontrado)
			return false;

		BaseDeDados!.Remove(obterModelo.Modelo!);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return true;
	}

	public abstract bool Existe(T modelo);
}
