using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Views.Interfaces;


namespace college_management.Views;

public class BuscaModeloView<T> : View, IBuscaModeloView where T : Modelo
{
	private string[] _campos;
	
	public BuscaModeloView(string titulo, string[]? campos = null) : base(titulo)
	{
		_campos = [..(campos ?? []), "Id"];
	}

	public KeyValuePair<int, string> Buscar()
	{
		MenuView menuPesquisa = new(Titulo,
		                            "Selecione um dos campos para pesquisar.",
		                            _campos);
		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		string? campoPesquisa = null;
		
		while (campoPesquisa is null)
		{
			menuPesquisa.LerEntrada();
			
			campoPesquisa = _campos.ElementAtOrDefault(menuPesquisa.OpcaoEscolhida - 1);
			
			if (campoPesquisa is null)
				Aviso("Campo inválido. Tente novamente.");
		}

		InputView inputPesquisa = new($"Pesquisar por {campoPesquisa}");
		inputPesquisa.LerEntrada(campoPesquisa,
		                         $"Insira o {campoPesquisa} d{typeof(T).Name.Last()} {typeof(T).Name}: ");

		return new KeyValuePair<int, string>(menuPesquisa.OpcaoEscolhida, inputPesquisa.ObterEntrada(campoPesquisa));
	}
}
