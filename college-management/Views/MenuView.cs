using System.Text;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class MenuView : View, IMenuView
{
	private readonly string   _cabecalho;
	public readonly string[] Opcoes;
	public           int      OpcaoEscolhida { get; private set; }

	public MenuView(string   titulo,
	                string   cabecalho,
	                string[] opcoes) : base(titulo)
	{
		_cabecalho = cabecalho;
		Opcoes    = opcoes;
	}

	public override void ConstruirLayout()
	{
		Layout.Append(_cabecalho);
		Layout.AppendLine(" Selecione uma das opções abaixo.");
		Layout.AppendLine();

		for (var i = 0; i < Opcoes.Length; i++)
			Layout.AppendLine($"[{i + 1}] {Opcoes[i]}");

		Layout.AppendLine();
		Layout.Append("Digite 0 para sair. Sua opção (somente números): ");
	}

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
}
