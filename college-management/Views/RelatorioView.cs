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
		List<StringBuilder> conteudo = [];
		
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();
		var nomesPropriedades =
			UtilitarioTipos.ObterNomesPropriedades(propriedades);

		for (int i = 0; i < _modelos.Count / linhasMaximas; ++i)
		{
			conteudo.Add(new());
			StringBuilder layout = conteudo[i];
			
			layout.AppendLine(nomesPropriedades);
				
			foreach (var p in propriedades)
				layout.Append($"| {new string('-', 16)} ");
			layout.AppendLine("|");

			for (int j = i * linhasMaximas; j < i * linhasMaximas + linhasMaximas; ++j)
			{
				var modelo = _modelos[j];
				layout.AppendLine(modelo.ToString());
			}
		}

		return conteudo.Select(i => i.ToString()).ToArray();
	}
}
