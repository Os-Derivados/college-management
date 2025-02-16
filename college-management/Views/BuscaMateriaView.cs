using college_management.Views.Interfaces;


namespace college_management.Views;


public class BuscaMateriaView : View, IBuscaMateriaView
{
	public BuscaMateriaView(string titulo) : base(titulo) { }

	public KeyValuePair<string, string> Buscar()
	{
		MenuView menuPesquisa = new("Pesquisar Matéria",
		                            "Selecione um dos campos para pesquisar.",
		                            ["Nome", "Id"]);
		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		var campoPesquisa = menuPesquisa.OpcaoEscolhida switch
		{
			1 => "Nome",
			2 => "Id",
			_ => null
		};

		while (campoPesquisa is null)
		{
			Aviso("Campo inválido. Tente novamente.");

			menuPesquisa.LerEntrada();

			campoPesquisa = menuPesquisa.OpcaoEscolhida switch
			{
				1 => "Nome",
				2 => "Id",
				_ => null
			};
		}

		InputView inputPesquisa = new($"Pesquisar por {campoPesquisa}");
		inputPesquisa.LerEntrada(campoPesquisa,
		                         $"Insira o {campoPesquisa} da Matéria: ");

		return new KeyValuePair<string, string>(
			campoPesquisa, inputPesquisa.ObterEntrada(campoPesquisa));
	}
}
