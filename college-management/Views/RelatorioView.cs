using System.Text;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class RelatorioView<T> : View, IPaginavel where T : Modelo
{
	private readonly List<T> _modelos;

	public RelatorioView(string titulo, List<T> modelos) :
		base(titulo)
	{
		_modelos = modelos;
	}

	public override void ConstruirLayout()
	{
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();
		var nomesPropriedades =
			UtilitarioTipos.ObterNomesPropriedades(propriedades);

		Layout.AppendLine($"Relatório de {typeof(T).Name}");
		Layout.AppendLine();

		Layout.AppendLine(nomesPropriedades);

		foreach (var p in propriedades)
			Layout.Append($"| {new string('-', 16)} ");

		Layout.AppendLine("|");

		foreach (var modelo in _modelos)
			Layout.AppendLine(modelo.ToString());
	}
	
	public override void Exibir()
	{
		base.Exibir();

		Console.ReadLine();
	}

	public string[] ConstruirPaginas(int linhasMaximas)
	{
		int indice = 0;
		int linhas = 0;
		List<StringBuilder> conteudo = [new()];
		StringBuilder layout = conteudo[indice];
		
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();
		var nomesPropriedades =
			UtilitarioTipos.ObterNomesPropriedades(propriedades);

		layout.AppendLine(nomesPropriedades);
		foreach (var p in propriedades)
			layout.Append($"| {new string('-', 16)} ");
		layout.AppendLine("|");
		linhas += 2;
		
		foreach (var modelo in _modelos)
		{
			if (linhas > linhasMaximas)
			{
				linhas = 0;
				indice++;
				conteudo.Add(new());
				layout = conteudo[indice];
				layout.AppendLine(nomesPropriedades);
				foreach (var p in propriedades)
					layout.Append($"| {new string('-', 16)} ");
				layout.AppendLine("|");
				linhas += 2;
				
				continue;
			}

			layout.AppendLine(modelo.ToString());
			linhas++;
		}

		return conteudo.Select(i => i.ToString()).ToArray();
	}
}
