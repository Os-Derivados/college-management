using System.Text;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoMaterias : Contexto<Materia>
{
	public ContextoMaterias(BaseDeDados baseDeDados,
	                        Usuario usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto)
	{
	}

	private async Task<Materia?> ObterMateriaComValidacao()
	{
		if (!TemAcessoRestrito)
		{
			ExibirMensagemErro(
				"Você não tem permissão para acessar esse recurso.");
			return null;
		}

		var materia = ObterDetalhesMateria();

		if (materia == null)
		{
			ExibirMensagemErro(
				"Matéria não encontrada."); // Mensagem mais específica
			return null;
		}

		return materia;
	}

	private Dictionary<string, string> ObterEntradasUsuario(
		string titulo,
		string mensagemConfir)
	{
		InputView inputUsuario = new(titulo);
		inputUsuario.ConstruirLayout();

		KeyValuePair<string, string?>[] mensagensUsuario =
		[
			new("Nome", "Insira o Nome: "),
			new("Turno", "Insira o Turno: "),
			new("CargaHoraria", "Insira a Carga Horária: ")
		];

		foreach (var mensagem in mensagensUsuario)
			inputUsuario.LerEntrada(mensagem.Key, mensagem.Value);

		DetalhesView detalhesView
			= new(mensagemConfir, inputUsuario.EntradasUsuario);
		detalhesView.ConstruirLayout();

		var mensagemConfirmacao
			= new StringBuilder(detalhesView.Layout.ToString());
		mensagemConfirmacao.AppendLine($"{mensagemConfir}?\n[S]im\t[N]ão: ");

		inputUsuario.LerEntrada("Confirma", mensagemConfirmacao.ToString());
		return inputUsuario.EntradasUsuario;
	}

	private void ExibirMensagemErro(string mensagem)
	{
		InputView inputUsuario = new(""); //evita criar inputview desnecessario
		inputUsuario.LerEntrada("Erro", mensagem);
	}

	private async Task<bool> ValidarECadastrarMateria(
		Dictionary<string, string> dadosMateria)
	{
		if (!Enum.TryParse(dadosMateria["Turno"], out Turno turnoEscolhido))
		{
			ExibirMensagemErro("O Turno inserido não foi encontrado.");
			return false;
		}

		if (!int.TryParse(dadosMateria["CargaHoraria"],
		                  out var cargaHoraria))
		{
			ExibirMensagemErro("A carga horária inserida não é válida.");
			return false;
		}

		Materia? novaMateria
			= new(dadosMateria["Nome"], turnoEscolhido, cargaHoraria);

		if (novaMateria is null)
		{
			ExibirMensagemErro(
				$"Não foi possível criar uma nova {nameof(Materia)}.");
			return false;
		}

		var cadastroMateria = await BaseDeDados.Materias.Adicionar(novaMateria);
		
		return cadastroMateria.Status is StatusResposta.Sucesso;
	}

	private async Task<bool> ValidarEAtualizarMateria(
		Materia materia,
		Dictionary<string, string> editarMateria)
	{
		if (!string.IsNullOrEmpty(editarMateria["Nome"]))
			materia.Nome = editarMateria["Nome"];

		if (!string.IsNullOrEmpty(editarMateria["Turno"]))
		{
			if (!Enum.TryParse(editarMateria["Turno"],
			                   out Turno turnoEscolhido))
			{
				ExibirMensagemErro("O Turno inserido não foi encontrado.");
				return false;
			}

			materia.Turno = turnoEscolhido;
		}

		if (!string.IsNullOrEmpty(editarMateria["CargaHoraria"]))
		{
			if (!int.TryParse(editarMateria["CargaHoraria"],
			                  out var cargaHoraria))
			{
				ExibirMensagemErro("A carga horária inserida não é válida.");
				return false;
			}

			materia.CargaHoraria = cargaHoraria;
		}

		return await BaseDeDados.Materias.Atualizar(materia);
	}

	private Materia? ObterDetalhesMateria()
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

		if (campoPesquisa == null)
		{
			ExibirMensagemErro("Campo inválido. Tente novamente.");
			return null;
		}

		InputView inputPesquisa = new($"Pesquisar por {campoPesquisa}");
		inputPesquisa.LerEntrada(campoPesquisa,
		                         $"Insira o {campoPesquisa} da Matéria: ");

		var valorPesquisa = inputPesquisa.ObterEntrada(campoPesquisa);

		return campoPesquisa switch
		{
			"Nome" => BaseDeDados.Materias.ObterPorNome(valorPesquisa),
			"Id"   => BaseDeDados.Materias.ObterPorId(valorPesquisa),
			_      => null // Nunca deve acontecer, mas para garantir
		};
	}

	public override async Task Cadastrar()
	{
		if (!TemAcessoRestrito)
		{
			ExibirMensagemErro(
				"Você não tem permissão para acessar esse recurso.");
			return;
		}

		var cadastroMateria
			= ObterEntradasUsuario("Cadastrar Matéria", "Confirmar Cadastro");

		if (cadastroMateria["Confirma"] is not "S") return;

		var foiAdicionado = await ValidarECadastrarMateria(cadastroMateria);

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Materia)} cadastrada com sucesso."
			: $"Não foi possível cadastrar uma nova {nameof(Materia)}.";

		ExibirMensagemErro(mensagemOperacao);
	}

	public override async Task Editar()
	{
		var materia = await ObterMateriaComValidacao();
		if (materia == null) return;

		var editarMateria
			= ObterEntradasUsuario("Editar Matéria", "Confirmar Edição");

		if (editarMateria["Confirma"] is not "S") return;

		var foiAtualizado
			= await ValidarEAtualizarMateria(materia, editarMateria);

		var mensagemOperacao = foiAtualizado
			? $"{nameof(Materia)} atualizada com sucesso."
			: $"Não foi possível atualizar a {nameof(Materia)}.";

		ExibirMensagemErro(mensagemOperacao);
	}

	public override async Task Excluir()
	{
		var materia = await ObterMateriaComValidacao();
		if (materia == null) return;

		InputView inputUsuario = new("Deletar Matéria");
		var mensagemConfirmacao
			= new StringBuilder(inputUsuario.Layout.ToString());
		mensagemConfirmacao.AppendLine(
			"Tem certeza que deseja deletar essa matéria?\n[S]im\t[N]ão: ");
		inputUsuario.LerEntrada("Confirma", mensagemConfirmacao.ToString());

		if (inputUsuario.EntradasUsuario["Confirma"] is not "S") return;

		var foiDeletado = await BaseDeDados.Materias.Remover(materia.Id);

		var mensagemOperacao = foiDeletado
			? $"{nameof(Materia)} deletada com sucesso."
			: $"Não foi possível deletar a {nameof(Materia)}.";

		ExibirMensagemErro(mensagemOperacao);
	}

	public override void Visualizar()
	{
		var verMaterias = BaseDeDados.Materias.ObterTodos();

		if (verMaterias.Modelo!.Count is 0)
		{
			ExibirMensagemErro("Nenhuma matéria cadastrada.");
			
			return;
		}

		RelatorioView<Materia> relatorioView
			= new("Visualizar Matérias", verMaterias.Modelo);
		
		relatorioView.ConstruirLayout();

		ExibirMensagemErro(relatorioView.Layout.ToString());
	}

	public override void VerDetalhes()
	{
		var materia = ObterDetalhesMateria();
		if (materia == null) return;

		var detalhes
			= UtilitarioTipos.ObterPropriedades(
				materia, ["Nome", "Turno", "CargaHoraria", "Id"]);

		DetalhesView detalhesMateria = new("Matéria Encontrada", detalhes);
		detalhesMateria.ConstruirLayout();

		ExibirMensagemErro(detalhesMateria.Layout.ToString());
	}
}
