using System.Text.Json;
using college_management.Servicos.Interfaces;
using college_management.Utilitarios;


namespace college_management.Servicos;


public sealed class ServicoDados<T> : IServicoDados<T>
{
	public ServicoDados()
	{
		if (!File.Exists(_caminhoArquivo))
			File.Create(_caminhoArquivo).Dispose();
	}

	private readonly string _caminhoArquivo
		= Path.Combine(UtilitarioArquivos.DiretorioDados,
		               $"{typeof(T).Name}s.json");

	public async Task SalvarAssicrono(List<T>? items)
	{
		await using var streamArquivo
			= File.OpenWrite(_caminhoArquivo);

		await JsonSerializer.SerializeAsync(streamArquivo,
		                                    items);
	}

	public async Task<List<T>?> CarregarAssincrono()
	{
		await using var streamArquivo
			= File.OpenRead(_caminhoArquivo);

		return await JsonSerializer
			       .DeserializeAsync<List<T>>(streamArquivo);
	}
}
