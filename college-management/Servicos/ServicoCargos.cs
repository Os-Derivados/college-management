using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoCargos : ServicoModelos<Cargo>
{
	public ServicoCargos(IRepositorio<Cargo> repositorio) : base(repositorio)
	{
	}

	public override Cargo? Pesquisar()
	{
		MenuView menuPesquisa = new("Cargos",
		                            "Selecione um dos campos:",
		                            ["Nome do Cargo", "Id"]);

		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		KeyValuePair<string, string>? campoPesquisa
			= menuPesquisa.OpcaoEscolhida switch
			{
				1 => new KeyValuePair<string, string>("Nome",
					"Insira o Nome do Cargo: "),
				2 => new KeyValuePair<string, string>("Id",
					"Insira o Id do Cargo: "),

				_ => null
			};

		InputView inputPesquisa = new("Ver Detalhes: Pesquisar Cargo");

		if (campoPesquisa is null)
		{
			View.Aviso("Campo Inválido. Tente novamente.");

			return null;
		}

		inputPesquisa.LerEntrada(campoPesquisa?.Key,
		                         campoPesquisa?.Value);

		_ = Enum.TryParse<CriterioBusca>(
			inputPesquisa.EntradasUsuario[campoPesquisa?.Key!], out var chave);

		var valorBusca = inputPesquisa.EntradasUsuario[campoPesquisa?.Key!];

		var obterCargo = Buscar(chave, valorBusca);

		return ValidarResposta(obterCargo, ModoOperacao.Leitura)
			? null
			: obterCargo.Modelo;
	}
}
