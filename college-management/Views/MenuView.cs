using college_management.Views.Interfaces;


namespace college_management.Views;


public class MenuView : View, IMenuView
{
	private int _pagina;
	private readonly int _quantidadePaginas;
	private readonly string   _cabecalho;
	public readonly string[] Opcoes;
	public int Pagina
	{
		get => _pagina;
	}

	public MenuView(string titulo,
	                string cabecalho,
	                string[] opcoes) : base(titulo)
	{
		_cabecalho = cabecalho;
		Opcoes     = opcoes;
		_pagina	   = 1;
		_quantidadePaginas = (int) Math.Ceiling(Opcoes.Length / 9.0f);
	}

	public int OpcaoEscolhida { get; private set; }

	public void LerEntrada()
	{
		Exibir();

		var entrada = Console.ReadKey();
		if (_quantidadePaginas > 1)
		{
			if (entrada.Key == ConsoleKey.LeftArrow)
			{
				_pagina = Math.Clamp(_pagina - 1, 1, _quantidadePaginas);
				Layout.Clear();
				ConstruirLayout();
				LerEntrada();
				return;
			}

			if (entrada.Key == ConsoleKey.RightArrow)
			{
				_pagina = Math.Clamp(_pagina + 1, 1, _quantidadePaginas);
				Layout.Clear();
				ConstruirLayout();
				LerEntrada();
				return;
			}
		}

		var entradaValida = int.TryParse(entrada
		                                 .KeyChar
		                                 .ToString(),
		                                 out var opcaoEscolhida);

		if (!entradaValida) return;
		opcaoEscolhida = (opcaoEscolhida - 1) + (_pagina - 1) * 9;
		if (opcaoEscolhida > Opcoes.Length)
		{
			LerEntrada();
			return;
		}

		OpcaoEscolhida = opcaoEscolhida;
	}

	public override void ConstruirLayout()
	{
		Layout.Append(_cabecalho);
		Layout.AppendLine(" Selecione uma das opções abaixo.");
		Layout.AppendLine();

		int inicio = (Pagina - 1) * 9;
		for (var i = inicio; i < Math.Min(Opcoes.Length, Pagina * 9); i++)
			Layout.AppendLine($"[{i % 9 + 1}] {Opcoes[i]}");
		
		if (_quantidadePaginas > 1)
			Layout.AppendLine($"(Página {_pagina}/{_quantidadePaginas})");
		Layout.AppendLine();
		Layout.Append("Digite 0 para sair, use as setas para mudar a página. Sua opção (somente números): ");
	}
}
