using System.Text.Json;
using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos;


namespace college_management.Dados.Repositorios;


public abstract class Repositorio<T> : IRepositorio<T>
	where T : Modelo
{
	protected          List<T>?        BaseDeDados;
	private readonly ServicoDados<T> _servicoDados = new();

	protected Repositorio()
	{
		if (BaseDeDados is not null)
			return;

		Task.Run(async () =>
		    {
			    try
			    {
				    using var dadosSalvos = _servicoDados.CarregarAssincrono();

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
		{
			return new RespostaRecurso<T>(modelo, StatusResposta.ErroDuplicata);
		}

		BaseDeDados!.Add(modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return new RespostaRecurso<T>(modelo, StatusResposta.Sucesso);
	}

	public RespostaRecurso<List<T>> ObterTodos()
	{
		return new RespostaRecurso<List<T>>(BaseDeDados,
		                                    StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorId(string? id)
	{
		var registro = BaseDeDados!.FirstOrDefault(t => t.Id.Equals(id));

		if (registro is null)
		{
			return new RespostaRecurso<T>(registro,
			                              StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public RespostaRecurso<T> ObterPorNome(string? nome)
	{
		var registro = BaseDeDados!.FirstOrDefault(t =>
		{
			var propriedadeNome = t.GetType().GetProperty("Nome");

			var valorNome = propriedadeNome?.GetValue(t)?.ToString();

			return (valorNome is not null) && (valorNome.Equals(nome));
		});

		if (registro is null)
		{
			return new RespostaRecurso<T>(registro,
			                              StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<T>(registro, StatusResposta.Sucesso);
	}

	public async Task<RespostaRecurso<T>> Atualizar(T modelo)
	{
		var resposta = ObterPorId(modelo.Id);

		if (resposta.Modelo is null) return await Adicionar(modelo);

		var respostaRemocao = await Remover(modelo.Id);

		if (respostaRemocao.Status is not StatusResposta.Sucesso)
		{
			return respostaRemocao;
		}

		await Adicionar(modelo);
		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return respostaRemocao;
	}

	public async Task<RespostaRecurso<T>> Remover(string? id)
	{
		var resposta = ObterPorId(id);

		if (resposta.Status is not StatusResposta.Sucesso ||
		    resposta.Modelo is null)
		{
			return resposta;
		}

		BaseDeDados!.Remove(resposta.Modelo);

		await _servicoDados.SalvarAssicrono(BaseDeDados);

		return resposta;
	}

	public abstract bool Existe(T modelo);
}
