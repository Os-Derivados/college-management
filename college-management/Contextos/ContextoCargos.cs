using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Servicos;
using college_management.Servicos.Interfaces;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCargos : Contexto<Cargo>
{
	public ContextoCargos(BaseDeDados baseDeDados,
	                      Usuario usuarioContexto,
	                      IServicoModelos<Cargo> servicoCargos) :
		base(baseDeDados,
		     usuarioContexto)
	{
		_servicoCargos = servicoCargos;
	}

	private readonly IServicoModelos<Cargo> _servicoCargos;

	public override async Task Cadastrar()
	{
		if (TemAcessoRestrito)
		{
			var novoCargo = TelaDeCadastro();

			if (novoCargo is null)
			{
				TelaErro("O cargo precisa de um nome e permissões válidas");

				return;
			}

			if (ConfirmarEscolha("Deseja salvar o cargo: ", novoCargo))
			{
				var adicionarCargo
					= await BaseDeDados.Cargos.Adicionar(novoCargo);

				if (adicionarCargo.Status is StatusResposta.Sucesso)
				{
					View.Aviso("Cargo salvo com sucesso!");

					return;
				}

				View.Aviso(
					"Cargo não salvo, pois outro com o mesmo nome já existe no banco de dados!");
			}
		}
	}

	public override async Task Editar()
	{
		Cargo? cargo     = null;
		string nomeCargo = "";


		InputView inputView = new InputView("Editar Cargo");

		if (TemAcessoRestrito)
		{
			nomeCargo = SelecionaCargoParaEdicao(inputView);

			var obterPorNome = BaseDeDados.Cargos.ObterPorNome(nomeCargo);

			if (obterPorNome.Status is StatusResposta.ErroNaoEncontrado)
			{
				View.Aviso("Cargo não existe");

				return;
			}

			cargo = TelaDeEdicao(obterPorNome.Modelo!, inputView);

			if (cargo is null)
			{
				TelaErro("O cargo precisa de um nome e permissões válidas");
				return;
			}


			if (ConfirmarEscolha("", cargo))
			{
				await BaseDeDados.Cargos.Atualizar(cargo);
				inputView.LerEntrada("Sair", "Cargo Editado com sucesso");
			}
		}
	}

	public override async Task Excluir()
	{
		if (!ValidarPermissoes()) return;

		InputView inputView = new("Exclusao de Cargo");

		var cargo = PesquisaCargo();

		if (cargo is null)
		{
			TelaErro("Opção Inválida!");
		}

		else
		{
			if (ConfirmarEscolha("Tem certeza que " +
			                     $"deseja excluir o cargo {cargo.Nome}?"))
			{
				await BaseDeDados.Cargos.Remover(cargo.Id);
				inputView.LerEntrada(
					"Sair", "Exclusão realizada com sucesso");
			}
		}
	}

	public override void Visualizar()
	{
		RelatorioView<Cargo> relatorioView;

		if (TemAcessoRestrito)
		{
			relatorioView = new RelatorioView<Cargo>("Visualizar Usuários",
				BaseDeDados.Cargos.ObterTodos().Modelo!);
		}
		else
		{
			relatorioView = new RelatorioView<Cargo>("Visualizar Usuário",
			[
				BaseDeDados.Cargos.ObterPorId(UsuarioContexto.CargoId).Modelo!
			]);
		}

		relatorioView.ConstruirLayout();
		relatorioView.Exibir();
	}


	public override void VerDetalhes()
	{
		if (!TemAcessoRestrito) return;

		DetalhesView detalhesView = null;

		var cargo = PesquisaCargo();

		if (cargo is null)
		{
			TelaErro("Opção Inválida!");

			return;
		}

		ExibirDetalhesCargo(cargo, detalhesView);
	}


	#region Metodos privados uteis

	Cargo? TelaDeCadastro()
	{
		InputView inputUsuario = new("Cadastrar cargo");
		inputUsuario.ConstruirLayout();

		inputUsuario.LerEntrada("Nome", "Insira o nome do novo cargo: ");

		if (!ValidarEntrada(inputUsuario, "Nome")) return null;

		List<string> nivelDePermissao = SelecaoDePermissao();
		var       nomeCargo        = inputUsuario.EntradasUsuario["Nome"];

		if (nomeCargo is not null && nivelDePermissao.Count is not 0)
		{
			return new Cargo(nomeCargo, nivelDePermissao);
		}

		return null;
	}


	Cargo? PesquisaCargo()
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

		var obterCargo = _servicoCargos.Buscar(chave, valorBusca);

		return _servicoCargos.ValidarResposta(obterCargo) ? null : obterCargo.Modelo;
	}


	List<string> SelecaoDePermissao()
	{
		List<string> permissoes = new List<string>();

		var      propriedades     = typeof(PermissoesAcesso).GetFields();
		string[] nomePropriedades = new string[propriedades.Length];
		int      index            = 0;

		for (int i = 0; i < propriedades.Length; i++)
		{
			nomePropriedades[i] = propriedades[i].Name;
			index++;
		}

		index = 0;


		while (index < nomePropriedades.Length)
		{
			MenuView menuView = new
				MenuView("Permissões",
				         "\t\tPermissões\n\n\n",
				         nomePropriedades);

			menuView.ConstruirLayout();
			menuView.Exibir();
			menuView.LerEntrada();

			int opcao = menuView.OpcaoEscolhida - 1;

			if (opcao >= nomePropriedades.Length || opcao < 0) break;

			if (!permissoes.Contains
			    (nomePropriedades
				     [opcao]))

				permissoes.Add(nomePropriedades[opcao]);


			if (ConfirmarEscolha($"Adicionou {permissoes.Last()}" +
			                     $", deseja adicionar mais permissões?\n"))

				index = opcao;

			else break;
		}

		Console.Clear();
		return permissoes;
	}


	string SelecionaCargoParaEdicao(InputView inputView)
	{
		inputView.LerEntrada("name", "Insira o nome do cargo a ser editado: ");

		var cargoId = inputView.ObterEntrada("name");

		return cargoId;
	}


	Cargo? TelaDeEdicao(Cargo cargoAtual, InputView inputView)
	{
		inputView.LerEntrada("nome",
		                     $"Nome Atual: {cargoAtual.Nome}\n\nEscolha um novo nome: ");

		if (!ValidarEntrada(inputView, "nome")) return null;

		var permissoes = SelecaoDePermissao();
		if (!permissoes.Any()) return null;

		return new Cargo(inputView.ObterEntrada("nome"), permissoes)
		{
			Id = cargoAtual.Id
		};
	}


	bool ValidarEntrada(InputView inputView, string chave)
	{
		string item = inputView.ObterEntrada(chave);


		if (string.IsNullOrEmpty(item))
		{
			return false;
		}

		return true;
	}

	void TelaErro(string menssagem)
	{
		Console.Clear();

		Console.WriteLine(menssagem);
		Console.WriteLine(
			"\t\t\n\n <--- Pressione alguma tecla para Sair --->");
		Console.ReadKey();
	}

	void ExibirDetalhesCargo(Cargo cargo, DetalhesView detalhesView)
	{
		var dicionario = new Dictionary<string, string>();
		var permissoes = ListaParaString(cargo.Permissoes);

		dicionario.Add("Id", cargo.Id.ToString());
		dicionario.Add("Nome", cargo.Nome);
		dicionario.Add("Permissões", permissoes);

		detalhesView = new DetalhesView("Cargo", dicionario);

		detalhesView.ConstruirLayout();
		detalhesView.Exibir();
	}

	string ListaParaString(List<string> strings)
	{
		string output = "\n[\n";

		foreach (string str in strings)
		{
			output += $"\t{str}\n";
		}

		output += "]";

		return output;
	}


	bool ConfirmarEscolha(string menssagem, Cargo? cargo = null)
	{
		Console.Clear();
		Console.WriteLine(menssagem);

		if (cargo is not null)
		{
			DetalhesView detalhesView = null;
			ExibirDetalhesCargo(cargo, detalhesView);
		}

		Console.WriteLine("\n\nPara confirmar:\n\n[S]im ou [N]ão: ");

		var opcao = Console.ReadKey();

		Console.Clear();

		if (opcao.KeyChar == 's' || opcao.KeyChar == 'S')
			return true;

		return false;
	}

	#endregion
}
