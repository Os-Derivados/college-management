using System.Text;
using college_management.Dados.Modelos;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class RelatorioView<T> : View, IPaginavel where T : Modelo
{
	public RelatorioView(string titulo, List<T> modelos) : base(titulo)
	{
		_modelos = modelos;
	}

	private readonly List<T> _modelos;

	public override void ConstruirLayout()
	{
		var cabecalho = _modelos.First().CabecalhoRelatorio();

		Layout.AppendLine($"Relatório de {typeof(T).Name}");
		Layout.AppendLine();

		Layout.AppendLine(cabecalho);

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

		var cabecalho = _modelos.First().CabecalhoRelatorio();

		for (var i = 0; i < Math.Ceiling((float)_modelos.Count / linhasMaximas); ++i)
		{
			conteudo.Add(new());
			var layoutAtual = conteudo[i];

			layoutAtual.AppendLine(cabecalho);

			for (var j = i * linhasMaximas; j < (i * linhasMaximas + linhasMaximas) & (j < _modelos.Count); ++j)
			{
				var modelo = _modelos[j];
				layoutAtual.AppendLine(modelo.ToString());
			}
		}

		return [.. conteudo.Select(i => i.ToString())];
	}
}
