using college_management.Views.Interfaces;


namespace college_management.Views;


public class MenuView : View, IMenuView
{
	private readonly string   _cabecalho;
	private readonly string[] _opcoes;

	public MenuView(string titulo,
	                string cabecalho,
	                string[] opcoes) : base(titulo)
	{
		_cabecalho = cabecalho;
		_opcoes    = opcoes;
	}

	public int OpcaoEscolhida { get; private set; }

	public void LerEntrada()
	{
		Exibir();

		var entrada = Console.ReadKey();
		var entradaValida = int.TryParse(entrada
		                                 .KeyChar
		                                 .ToString(),
		                                 out var opcaoEscolhida);

		if (!entradaValida) return;

		OpcaoEscolhida = opcaoEscolhida;
	}

	public override string ConstruirLayout()
	{
		Layout.Append(_cabecalho);
		Layout.AppendLine(" Selecione uma das opções abaixo.");
		Layout.AppendLine();

		for (var i = 0; i < _opcoes.Length; i++)
			Layout.AppendLine($"[{i + 1}] {_opcoes[i]}");

		Layout.AppendLine();
		Layout.AppendLine(GerarDivisoria('-'));
		Layout.Append("Sua opção (somente números): ");
		
		return Layout.ToString();
	}
}
