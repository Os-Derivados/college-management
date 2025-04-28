using System.Collections;
using System.Text;
using college_management.Dados.Modelos;
using college_management.Servicos.Interfaces;


namespace college_management.Servicos;


public sealed class ServicoRelatorios<T> : IServicoRelatorios<T>
	where T : Modelo
{
	private readonly List<T> _modelos;

	public ServicoRelatorios(List<T> modelos) { _modelos = modelos; }

	public string GerarRelatorio()
	{
		if (_modelos.Count == 0)
			return "Nenhum registro encontrado.";

		var relatorio    = new StringBuilder();
		var propriedades = typeof(T).GetProperties();


		//  Adiciona o cabeçalho à string CSV relatorio
		foreach (var propriedade in propriedades)
		{
			if (propriedades.Last() == propriedade)
				relatorio.Append($"{propriedade.Name}\n");

			else
				relatorio.Append($"{propriedade.Name},");
		}

		// Adiciona os valores à string CSV
		foreach (var modelo in _modelos)
		{
			if (modelo == null)
				continue;

			// Adiciona os valores do registro à string CSV relatorio
			foreach (var propriedade in propriedades)
			{
				var _ = propriedade.GetValue(modelo);

				if (_ is Array a)
				{
					relatorio.Append(FormatarEnumerables(a));
				}

				else if (_ is IList l)
				{
					relatorio.Append(FormatarEnumerables(l));
				}

				else
					relatorio.Append(_);


				relatorio.Append(
					propriedades.Last() == propriedade ? "\n" : ",");
			}
		}

		return relatorio.ToString();


		static string FormatarEnumerables(IEnumerable enumerable)
		{
			StringBuilder stringLista = new();

			foreach (var v in enumerable)
			{
				if (v is not Modelo m)
					continue;

				stringLista.Append(m.Nome);

				stringLista.Append(',');
			}

			if (stringLista.Length > 0)
				stringLista.Remove(stringLista.Length - 1, 1);

			return stringLista.ToString();
		}
	}

	public async Task<string> ExportarRelatorio(string relatorio)
	{
		var timeNow = DateTime.UtcNow;

		var diretorioExportacao = ObterDiretorioExportacao();

		var caminhoRelatorio = Path.Combine(diretorioExportacao,
		                                    $"{typeof(T).Name}_{timeNow:dd-MM-yy_H-mm-ss}.csv");

		await File.WriteAllTextAsync(caminhoRelatorio, relatorio);

		return File.Exists(caminhoRelatorio) ? caminhoRelatorio : string.Empty;
	}

	private string ObterDiretorioExportacao()
	{
		var meusDocumentos
			= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

		if (meusDocumentos != string.Empty) return meusDocumentos;

		var areaDeTrabalho
			= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		if (areaDeTrabalho != string.Empty) return areaDeTrabalho;

		var perfilUsuario
			= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		if (perfilUsuario != string.Empty) return perfilUsuario;

		var meuComputador
			= Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

		return meuComputador != string.Empty
			? meuComputador
			: Environment.GetFolderPath(Environment.SpecialFolder.System);
	}
}
