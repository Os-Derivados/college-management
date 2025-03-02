using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
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
		if (!ValidarPermissoes()) return;

		var novoCargo = TelaDeCadastro();

		if (novoCargo is null)
		{
			View.Aviso("O Cargo precisa de um nome e permissões válidas");

			return;
		}

		if (ConfirmarEscolha("Deseja salvar o cargo: ", novoCargo))
		{
			var adicionarCargo
				= await BaseDeDados.Cargos.Adicionar(novoCargo);

			_servicoCargos.ValidarResposta(adicionarCargo,
			                               ModoOperacao.Escrita);
		}
	}

	public override async Task Editar()
	{
		if (!ValidarPermissoes()) return;

		InputView inputView = new("Editar Cargo");

		var nomeCargo = SelecionaCargoParaEdicao(inputView);

		var obterPorNome = BaseDeDados.Cargos.ObterPorNome(nomeCargo);

		if (obterPorNome.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Cargo não existe");

			return;
		}

		var cargo = TelaDeEdicao(obterPorNome.Modelo!, inputView);

		if (cargo is null)
		{
			View.Aviso("O Cargo precisa de um nome e permissões válidas");

			return;
		}


		if (ConfirmarEscolha("", cargo))
		{
			await BaseDeDados.Cargos.Atualizar(cargo);
			inputView.LerEntrada("Sair", "Cargo Editado com sucesso");
		}
	}

	public override async Task Excluir()
	{
		if (!ValidarPermissoes()) return;

		InputView inputView = new("Exclusao de Cargo");

		var cargo = _servicoCargos.Pesquisar();

		if (cargo is null)
		{
			View.Aviso("Opção Inválida!");
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

		var cargo = _servicoCargos.Pesquisar();

		if (cargo is null)
		{
			View.Aviso("Opção Inválida!");

			return;
		}

		ExibirDetalhesCargo(cargo);
	}

	Cargo? TelaDeCadastro()
	{
		InputView inputUsuario = new("Cadastrar cargo");
		inputUsuario.ConstruirLayout();

		inputUsuario.LerEntrada("Nome", "Insira o nome do novo cargo: ");

		if (!ValidarEntrada(inputUsuario, "Nome")) return null;

		var nivelDePermissao = SelecaoDePermissao();
		var nomeCargo        = inputUsuario.EntradasUsuario["Nome"];

		return nivelDePermissao.Count is not 0
			? new Cargo(nomeCargo, nivelDePermissao)
			: null;
	}

	List<string> SelecaoDePermissao()
	{
		List<string> permissoes = [];

		var propriedades     = typeof(PermissoesAcesso).GetFields();
		var nomePropriedades = new string[propriedades.Length];
		var index            = 0;

		for (var i = 0; i < propriedades.Length; i++)
		{
			nomePropriedades[i] = propriedades[i].Name;
			index++;
		}

		index = 0;

		while (index < nomePropriedades.Length)
		{
			MenuView menuView = new("Permissões",
			                        "\t\tPermissões\n\n\n",
			                        nomePropriedades);

			menuView.ConstruirLayout();
			menuView.LerEntrada();

			var opcao = menuView.OpcaoEscolhida - 1;

			if (opcao >= nomePropriedades.Length || opcao < 0) break;

			if (!permissoes.Contains
			    (nomePropriedades
				     [opcao]))

				permissoes.Add(nomePropriedades[opcao]);


			if (ConfirmarEscolha($"Adicionou {permissoes.Last()}" +
			                     $", deseja adicionar mais permissões?\n"))
			{
				index = opcao;
			}

			else break;
		}

		Console.Clear();

		return permissoes;
	}


	// trsaints: Isso daqui é uma View
	string SelecionaCargoParaEdicao(InputView inputView)
	{
		inputView.LerEntrada("name", "Insira o nome do cargo a ser editado: ");

		var cargoId = inputView.ObterEntrada("name");

		return cargoId;
	}


	// trsaints: Isso daqui também é uma View, altamente acoplada com
	// gerar um novo objeto Cargo
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


	// trsaints: Redundante, deve ser validado no Serviço
	bool ValidarEntrada(InputView inputView, string chave)
	{
		string item = inputView.ObterEntrada(chave);


		if (string.IsNullOrEmpty(item))
		{
			return false;
		}

		return true;
	}

	// trsaints: isso aqui também é uma View
	void ExibirDetalhesCargo(Cargo cargo)
	{
		var dicionario = new Dictionary<string, string>();
		var permissoes = ListaParaString(cargo.Permissoes);

		dicionario.Add("Id", cargo.Id.ToString());
		dicionario.Add("Nome", cargo.Nome);
		dicionario.Add("Permissões", permissoes);

		DetalhesView detalhesView = new("Cargo", dicionario);

		detalhesView.ConstruirLayout();
		detalhesView.Exibir();
	}

	// trsaints: Também é uma View
	string ListaParaString(List<string> strings)
	{
		var output = "\n[\n";

		foreach (var str in strings)
		{
			output += $"\t{str}\n";
		}

		output += "]";

		return output;
	}


	// trsaints: Já existe um ConfirmaView, isso aqui é redundante
	bool ConfirmarEscolha(string menssagem, Cargo? cargo = null)
	{
		Console.Clear();
		Console.WriteLine(menssagem);

		if (cargo is not null)
		{
			ExibirDetalhesCargo(cargo);
		}

		Console.WriteLine("\n\nPara confirmar:\n\n[S]im ou [N]ão: ");

		var opcao = Console.ReadKey();

		Console.Clear();

		if (opcao.KeyChar == 's' || opcao.KeyChar == 'S')
			return true;

		return false;
	}
}
