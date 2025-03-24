using System.Text;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class MenuView : View, IMenuView
{
	private PaginaView _paginaView;
	private readonly string   _cabecalho;
	public readonly string[] Opcoes;

	public MenuView(string titulo,
	                string cabecalho,
	                string[] opcoes) : base(titulo)
	{
		_cabecalho = cabecalho;
		Opcoes     = opcoes;
	}

	public int OpcaoEscolhida { get; private set; }

	public void LerEntrada()
	{
		var entrada = _paginaView.LerEntrada(false);

		if (entrada.Key is ConsoleKey.Enter)
		{
			OpcaoEscolhida = 0;
			return;
		}
		
		var entradaValida = int.TryParse(entrada
		                                 .KeyChar
		                                 .ToString(),
		                                 out var opcaoEscolhida);

		if (!entradaValida || (opcaoEscolhida += (_paginaView.IndicePagina - 1) * 9) > Opcoes.Length)
		{
			LerEntrada();
			return;
		}

		OpcaoEscolhida = opcaoEscolhida;
	}

	public override void ConstruirLayout()
	{
		List<StringBuilder> conteudo = new();

		_paginaView = new(_cabecalho + " Selecione uma das opções abaixo.");

		for (int i = 0; i < (int)Math.Ceiling(Opcoes.Length / 9.0f); i++)
		{
			conteudo.Add(new());
			for (var j = i * 9; j < Math.Min(Opcoes.Length, (i + 1) * 9); j++)
				conteudo[i].AppendLine($"[{j % 9 + 1}] {Opcoes[j]}");
		}
		
		_paginaView.AdicionarPaginas(conteudo.Select(i => i.ToString()).ToArray());
	}
}
