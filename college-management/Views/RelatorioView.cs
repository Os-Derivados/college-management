using System.Text;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class RelatorioView<T> : View, IPaginavel where T : Modelo
{
	private readonly List<T> _modelos;
	// Se o buffer do console for pequeno demais, ignoraremos a escala proporcional
	// e nos acomodaremos num valor mínimo.
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
		
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();
		var nomesPropriedades =
			UtilitarioTipos.ObterNomesPropriedades(propriedades);

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
			// Encurta o valor, omitindo partes da string a favor de estrutura no relatório, caso necessário.
			string nomeCurto = EncurtarValor(propriedade.Name, propriedades.Length);
			cabecalho += $"| {nomeCurto}" +
			             $"{new string(' ', Math.Clamp(_larguraBuffer / propriedades.Length - nomeCurto.Length - 3, 0, int.MaxValue))}";
			
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
			UtilitarioTipos.ObterPropriedades(modelo, propriedades.Select(i => i.Name).ToArray());

		string line = string.Empty;
		foreach (var (nome, valor) in valoresPropriedades)
		{
			// Encurta o valor, omitindo partes da string a favor de estrutura no relatório.
			string valorCurto = EncurtarValor(valor, propriedades.Length);

			line += $"| {valorCurto}" +
			        $"{new string(' ', Math.Clamp(_larguraBuffer / propriedades.Length - valorCurto.Length - 3, 0, int.MaxValue))}";
		}

		line += $"{new string(' ', Math.Abs(line.Length - _larguraBuffer))}";
		line = line.Remove(line.Length - 1) + '|';
		return line;
	}

	private string EncurtarValor(string valor, int limite)
	{
		string valorCurto = valor.Substring(0,
			valor.Length > (_larguraBuffer / limite / 2)
				? (_larguraBuffer / limite / 2)
				: valor.Length);
				
		valorCurto += valorCurto.Length < valor.Length ? "..." : string.Empty;

		return valorCurto;
	}
}
