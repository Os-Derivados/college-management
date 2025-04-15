using System.Reflection;
using System.Text;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class RelatorioView<T> : View, IPaginavel where T : Modelo
{
	private readonly List<T> _modelos;
	private readonly int _larguraBuffer = Console.BufferWidth;

	public RelatorioView(string titulo, List<T> modelos) :
		base(titulo)
	{
		_modelos = modelos;
	}

	public override void ConstruirLayout()
	{
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();

		Layout.AppendLine($"Relatório de {typeof(T).Name}");
		Layout.AppendLine();
		
		Layout.AppendLine(ConstruirCabecalho(propriedades));
		ConstruirTabela(propriedades);
		
	}
	
	public override void Exibir()
	{
		base.Exibir();

		Console.ReadLine();
	}

	public string[] ConstruirPaginas(int linhasMaximas)
	{
		List<StringBuilder> conteudo = [];

		var propriedades = UtilitarioTipos.ObterPropriedades<T>();

		for (int i = 0; i < Math.Ceiling((float) _modelos.Count / linhasMaximas); ++i)
		{
			conteudo.Add(new());
			StringBuilder layout = conteudo[i];

			layout.AppendLine(ConstruirCabecalho(propriedades));
			
			for (int j = i * linhasMaximas; j < (i * linhasMaximas + linhasMaximas) & (j < _modelos.Count); ++j)
			{
				layout.AppendLine(ConstruirLinha(_modelos[j], propriedades));
			}
		}

		return conteudo.Select(i => i.ToString()).ToArray();
	}

	private string ConstruirCabecalho(PropertyInfo[] propriedades)
	{
		string cabecalho = string.Empty;
		foreach (var propriedade in propriedades)
		{
			cabecalho += $"| {propriedade.Name}" +
			        $"{new string(' ', Math.Clamp(_larguraBuffer / propriedades.Length - propriedade.Name.Length - 2, 0, int.MaxValue))}";

			cabecalho = FormatarLinha(cabecalho, propriedade.Name, propriedades.Length);
		}
		
		cabecalho += $"{new string(' ', Math.Abs(cabecalho.Length - _larguraBuffer))}";
		cabecalho = cabecalho.Remove(cabecalho.Length - 1) + '|';
		cabecalho += $"\n| {new string('-', _larguraBuffer - 4)} |";
		return cabecalho;
	}

	private void ConstruirTabela(PropertyInfo[] propriedades)
	{
		foreach (var modelo in _modelos)
		{
			Layout.AppendLine(ConstruirLinha(modelo, propriedades));
		}
	}

	private string ConstruirLinha(T modelo, PropertyInfo[] propriedades)
	{
		var valoresPropriedades =
			UtilitarioTipos.ObterPropriedades(modelo);

		string linha = string.Empty;
		foreach (var (nome, valor) in valoresPropriedades)
		{
			linha += $"| {valor}" +
			        $"{new string(' ', Math.Clamp(_larguraBuffer / propriedades.Length - valor.Length - 2, 0, int.MaxValue))}";

			linha = FormatarLinha(linha, valor, propriedades.Length);
		}

		linha += $"{new string(' ', Math.Abs(linha.Length - _larguraBuffer))}";
		linha = linha.Remove(linha.Length - 1) + '|';
		return linha;
	}

	private string FormatarLinha(string linha, string valor, int limite)
	{
		if (valor.Length > Math.Clamp(_larguraBuffer / limite - 2, valor.Length - 1, int.MaxValue))
		{
			linha = linha.Remove(linha.Length - Math.Abs(valor.Length - _larguraBuffer / limite) - 6) + "... ";
		}

		return linha;
	}
}
