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
		var tipo = typeof(T);
		var cabecalho = Modelo.CabecalhoRelatorio(
			tipo.GetProperties().Select(p => p.Name)
		);

		Layout.AppendLine($"Relatório de {tipo.Name}");
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

		var tipo = typeof(T);
		var cabecalho = Modelo.CabecalhoRelatorio(
            tipo.GetProperties().Select(p => p.Name)
        );

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
