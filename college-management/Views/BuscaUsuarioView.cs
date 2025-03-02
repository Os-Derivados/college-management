using college_management.Views.Interfaces;


namespace college_management.Views;


public class BuscaUsuarioView : View, IBuscaUsuarioView
{
	public BuscaUsuarioView() : base("Pesquisar Usuario") { }

	public KeyValuePair<string, string> Buscar()
	{
		MenuView menuPesquisa = new("Pesquisar Usuário",
		                            "Selecione um dos campos para pesquisar.",
		                            ["Login", "Id"]);

		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		KeyValuePair<string, string>? campoPesquisa
			= menuPesquisa.OpcaoEscolhida switch
			{
				1 => new KeyValuePair<string, string>("Login",
					"Insira o Login do Usuario: "),
				2 => new KeyValuePair<string, string>("Id",
					"Insira o Id do Usuario: "),
				_ => null
			};

		InputView inputPesquisa = new("Ver Detalhes: Pesquisar Usuario");

		if (campoPesquisa is null)
		{
			inputPesquisa.LerEntrada("Campo",
			                         "Campo inválido. Tente novamente.");

			return new KeyValuePair<string, string>("", "");
		}

		inputPesquisa.LerEntrada(campoPesquisa?.Key!,
		                         campoPesquisa?.Value);

		return new KeyValuePair<string, string>(campoPesquisa?.Key!,
		                                        inputPesquisa.ObterEntrada(
			                                        campoPesquisa?.Key!));
	}
}
