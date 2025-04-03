using System.Text;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoMaterias : Contexto<Materia>
{
	public ContextoMaterias(BaseDeDados baseDeDados, Usuario usuarioContexto) :
		base(baseDeDados, usuarioContexto)
	{
	}

	private Dictionary<string, string> ObterEntradasUsuario(string titulo)
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

		return inputUsuario.EntradasUsuario;
	}

	private async Task<bool> ValidarECadastrarMateria(
		Dictionary<string, string> dadosMateria)
	{
		if (!Enum.TryParse(dadosMateria["Turno"], out Turno turnoEscolhido))
		{
			View.Aviso("O Turno inserido não foi encontrado.");

			return false;
		}

		if (!int.TryParse(dadosMateria["CargaHoraria"], out var cargaHoraria))
		{
			View.Aviso("A carga horária inserida não é válida.");

			return false;
		}

		Materia novaMateria
			= new(dadosMateria["Nome"], turnoEscolhido, cargaHoraria);

		var cadastroMateria = await BaseDeDados.Materias.Adicionar(novaMateria);

		return cadastroMateria.Status is StatusResposta.Sucesso;
	}

	private async Task<bool> ValidarEAtualizarMateria(Materia materia,
		Dictionary<string, string> editarMateria)
	{
		if (!string.IsNullOrEmpty(editarMateria["Nome"]))
		{
			materia.Nome = editarMateria["Nome"];
		}

		if (!string.IsNullOrEmpty(editarMateria["Turno"]))
		{
			if (!Enum.TryParse(editarMateria["Turno"],
			                   out Turno turnoEscolhido))
			{
				View.Aviso("O Turno inserido não foi encontrado.");

				return false;
			}

			materia.Turno = turnoEscolhido;
		}

		if (!string.IsNullOrEmpty(editarMateria["CargaHoraria"]))
		{
			if (!int.TryParse(editarMateria["CargaHoraria"],
			                  out var cargaHoraria))
			{
				View.Aviso("A carga horária inserida não é válida.");
				return false;
			}

			materia.CargaHoraria = cargaHoraria;
		}

		var atualizarMateria = await BaseDeDados.Materias.Atualizar(materia);

		return atualizarMateria.Status is StatusResposta.Sucesso;
	}

	public override async Task Cadastrar()
	{
		if (!TemAcessoRestrito)
		{
			View.Aviso(
				"Você não tem permissão para acessar esse recurso.");

			return;
		}

		var cadastroMateria
			= ObterEntradasUsuario("Cadastrar Matéria");

		DetalhesView detalhesMateria
			= new("Detalhes da Matéria", cadastroMateria);
		detalhesMateria.ConstruirLayout();

		ConfirmaView confirmarCadastro = new("Cadastrar Materia");
		var confirmacao
			= confirmarCadastro.Confirmar(detalhesMateria.Layout.ToString());

		if (confirmacao.ToString().ToLower() is not "s") return;

		var foiAdicionado = await ValidarECadastrarMateria(cadastroMateria);

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Materia)} cadastrada com sucesso."
			: $"Não foi possível cadastrar uma nova {nameof(Materia)}.";

		View.Aviso(mensagemOperacao);
	}

	public override async Task Editar()
	{
		BuscaModeloView<Materia> buscaModelo = new("Buscar Matéria", ["Nome"]);
		var              chaveBusca   = buscaModelo.Buscar();

		var obterMateria = chaveBusca.Key is 1
			? BaseDeDados.Materias.ObterPorNome(chaveBusca.Value)
			: BaseDeDados.Materias.ObterPorId(chaveBusca.Value);

		if (obterMateria.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Matéria não encontrada.");

			return;
		}

		var editarMateria = ObterEntradasUsuario("Editar Matéria");

		DetalhesView detalhesMateria
			= new("Detalhes da Matéria", editarMateria);
		detalhesMateria.ConstruirLayout();

		ConfirmaView confirmarCadastro = new("Editar Materia");
		var confirmacao
			= confirmarCadastro.Confirmar(detalhesMateria.Layout.ToString());

		if (confirmacao.ToString().ToLower() is not "s") return;

		var foiAtualizado
			= await ValidarEAtualizarMateria(obterMateria.Modelo!,
			                                 editarMateria);

		var mensagemOperacao = foiAtualizado
			? $"{nameof(Materia)} atualizada com sucesso."
			: $"Não foi possível atualizar a {nameof(Materia)}.";

		View.Aviso(mensagemOperacao);
	}

	public override async Task Excluir()
	{
		BuscaModeloView<Materia> buscaModelo = new("Buscar Matéria", ["Nome"]);
		var              chaveBusca   = buscaModelo.Buscar();

		var obterMateria = chaveBusca.Key is 1
			? BaseDeDados.Materias.ObterPorNome(chaveBusca.Value)
			: BaseDeDados.Materias.ObterPorId(chaveBusca.Value);

		if (obterMateria.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Matéria não encontrada.");

			return;
		}

		DetalhesView detalhesMateria
			= new("Detalhes da Matéria", UtilitarioTipos.ObterPropriedades(
				      obterMateria.Modelo,
				      ["Nome", "Id", "CargaHoraria", "Turno"]));
		detalhesMateria.ConstruirLayout();

		ConfirmaView confirmarCadastro = new("Excluir Materia");
		var confirmacao
			= confirmarCadastro.Confirmar(detalhesMateria.Layout.ToString());

		if (confirmacao.ToString().ToLower() is not "s") return;

		var excluirMateria
			= await BaseDeDados.Materias.Remover(obterMateria.Modelo!.Id);

		var mensagemOperacao = excluirMateria.Status switch
		{
			StatusResposta.Sucesso =>
				$"{nameof(Materia)} deletada com sucesso.",
			StatusResposta.ErroNaoEncontrado => "Matéria não encontrada.",
			_ => "Não foi possível deletar a matéria."
		};

		View.Aviso(mensagemOperacao);
	}

	public override void Visualizar()
	{
		var verMaterias = BaseDeDados.Materias.ObterTodos();

		if (verMaterias.Modelo!.Count is 0)
		{
			View.Aviso("Nenhuma matéria cadastrada.");

			return;
		}

		RelatorioView<Materia> relatorioView
			= new("Visualizar Matérias", verMaterias.Modelo);

		var paginaView = new PaginaView(relatorioView);
		paginaView.ConstruirLayout();
		paginaView.LerEntrada(true);
	}

	public override void VerDetalhes()
	{
		BuscaModeloView<Materia> buscaModelo = new("Buscar Matéria", ["Nome"]);
		var              chaveBusca   = buscaModelo.Buscar();

		var obterMateria = chaveBusca.Key is 1	
			? BaseDeDados.Materias.ObterPorNome(chaveBusca.Value)
			: BaseDeDados.Materias.ObterPorId(chaveBusca.Value);

		if (obterMateria.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Matéria não encontrada.");

			return;
		}

		var detalhes = UtilitarioTipos.ObterPropriedades(
			obterMateria.Modelo, ["Nome", "Turno", "CargaHoraria", "Id"]);

		DetalhesView detalhesMateria = new("Matéria Encontrada", detalhes);
		detalhesMateria.ConstruirLayout();
		detalhesMateria.Exibir();
	}
}
