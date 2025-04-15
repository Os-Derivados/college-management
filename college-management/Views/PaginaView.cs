using System.Globalization;
using college_management.Dados.Modelos;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class PaginaView : View, IPaginaView	
{
	private int _paginaAtual;
	private List<string> _conteudoPaginas;
	
	/// <summary>
	/// Índice da página atual.
	/// </summary>
	public int IndicePagina  => _paginaAtual;
	/// <summary>
	/// Conteúdo da página atual.
	/// </summary>
	public string ConteudoAtual => _conteudoPaginas[IndicePagina - 1];
	/// <summary>
	/// Quantidade total de páginas.
	/// </summary>
	public int QuantidadePaginas => _conteudoPaginas.Count;
	
	public PaginaView(string titulo) : base(titulo)
	{
		_paginaAtual = 1;
		_conteudoPaginas = new();
	}
	
	public PaginaView(string titulo, string[] conteudoPaginas) : base(titulo)
	{
		_paginaAtual = 1;
		_conteudoPaginas = new(conteudoPaginas);
	}

	public PaginaView(IPaginavel view)
		: this((view as View)!.Titulo, view.ConstruirPaginas(Console.BufferHeight / 2))
	{}
	
	public void AdicionarPagina(string conteudo) =>
		_conteudoPaginas.Add(conteudo);
	public void AdicionarPagina(IPaginavel view) =>
		AdicionarPaginas(view.ConstruirPaginas(Console.BufferHeight / 2));
	public void AdicionarPaginas(string[] paginas) =>
		_conteudoPaginas.AddRange(paginas);
	
	public string ObterPagina(int indice) =>
		_conteudoPaginas[indice - 1];
	
	public ConsoleKeyInfo LerEntrada(bool ignorarEntrada = false)
	{
		AtualizarLayout();
		Exibir();
		
		var input = Console.ReadKey();
		switch (input.Key)
		{
			case ConsoleKey.LeftArrow:
				_paginaAtual = Math.Clamp(_paginaAtual - 1, 1, QuantidadePaginas);
				break;
			case ConsoleKey.RightArrow:
				_paginaAtual = Math.Clamp(_paginaAtual + 1, 1, QuantidadePaginas);
				break;
			case ConsoleKey.PageDown:
				_paginaAtual = Math.Clamp(_paginaAtual - 5, 1, QuantidadePaginas);
				break;
			case ConsoleKey.PageUp:
				_paginaAtual = Math.Clamp(_paginaAtual + 5, 1, QuantidadePaginas);
				break;
			case ConsoleKey.Divide:
				PularParaPagina();
				break;
			default:
				return ignorarEntrada && input.Key is not ConsoleKey.Enter ? LerEntrada(ignorarEntrada) : input;
		}
		
		return LerEntrada(ignorarEntrada);
	}
	
	public override void ConstruirLayout()
	{
		Layout.AppendLine(Titulo);
		Layout.AppendLine();
		
		if (_conteudoPaginas.Count <= 0)
			_conteudoPaginas.Add(string.Empty);

		Layout.AppendLine(_conteudoPaginas[IndicePagina - 1]);
		
		if (QuantidadePaginas > 1)
			Layout.AppendLine($"(Página {IndicePagina}/{QuantidadePaginas})");
		Layout.AppendLine();
		Layout.Append($"Pressione ENTER para sair{(QuantidadePaginas > 1 ? ", use as setas (direita ou esquerda) para navegar entre páginas" : string.Empty)}.");
	}

	private void PularParaPagina()
	{
		var entrada = Console.ReadLine();
		var resultado = UInt32.TryParse(entrada, out uint indice);

		if (!resultado | indice > QuantidadePaginas | indice <= 0)
		{
			View.Aviso("Índice inválido.");
			return;
		}

		_paginaAtual = (int) indice;
	}

	private void AtualizarLayout()
	{
		Layout.Clear();
		ConstruirLayout();
	}
}
