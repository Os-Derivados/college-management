using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoUsuarios : Contexto<Usuario>, IContextoUsuarios
{
	public ContextoUsuarios(BaseDeDados baseDeDados, Usuario usuarioContexto) :
		base(baseDeDados, usuarioContexto)
	{
	}

	public void VerMatricula() { throw new NotImplementedException(); }

	public void VerBoletim() { throw new NotImplementedException(); }

	public override async Task Cadastrar()
	{
		InputView inputUsuario = new("Cadastrar Usuário");
		inputUsuario.ConstruirLayout();

		if (!ValidarPermissoes()) return;

		CadastroUsuarioView cadastroUsuarioView = new();

		var confirmaCadastro = cadastroUsuarioView.ObterDados();
		var dadosUsuario     = cadastroUsuarioView.CadastroUsuario;

		if (confirmaCadastro is not 's') return;

		var novoUsuario = Usuario.CriarUsuario(dadosUsuario!);
		novoUsuario.GerarCredenciais(dadosUsuario!["Senha"]);
		
		var cadastroUsuario = await BaseDeDados.Usuarios.Adicionar(novoUsuario);

		var foiAdicionado = cadastroUsuario.Status is StatusResposta.Sucesso;

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Usuario)} cadastrado com sucesso."
			: $"Não foi possível cadastrar novo {nameof(Usuario)}.";

		View.Aviso(mensagemOperacao);
	}

	public override async Task Editar()
	{
		if (!ValidarPermissoes()) return;

		BuscaModeloView<Usuario>
			buscaUsuario = new("Buscar Usuário", ["Login"]);

		var (opcao, chaveBusca) = buscaUsuario.Buscar();

		var obterUsuario = opcao is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(uint.Parse(chaveBusca));

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Usuário não encontrado na base de dados.");

			return;
		}

		EditarUsuarioView editarUsuarioView = new(obterUsuario.Modelo!);
		var               usuarioEditado    = editarUsuarioView.Editar();

		ConfirmaView confirmaEdicao = new("Editar Usuário");

		if (confirmaEdicao.Confirmar("Editar Usuário").ToString().ToLower() is
		    not "s")
		{
			View.Aviso($"Editar {nameof(Usuario)}: Operação cancelada.");

			return;
		}

		var atualizarUsuario
			= await BaseDeDados.Usuarios.Atualizar(usuarioEditado);

		var mensagemOperacao = atualizarUsuario.Status switch
		{
			StatusResposta.Sucesso => $"{nameof(Usuario)} editado com sucesso.",
			_ => $"Não foi possível editar o {nameof(Usuario)}."
		};

		View.Aviso(mensagemOperacao);
	}

	public override async Task Excluir()
	{
		if (!ValidarPermissoes()) return;

		BuscaModeloView<Usuario> buscaUsuario
			= new("Buscar Usuário", ["Login", "Id"]);

		var (opcao, chaveBusca) = buscaUsuario.Buscar();

		var obterUsuario = opcao is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(uint.Parse(chaveBusca));

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Usuário não encontrado.");

			return;
		}


		DetalhesView detalhesUsuario = new("Excluir Usuário",
		                                   UtilitarioTipos.ObterPropriedades(
			                                   obterUsuario,
			                                   [
				                                   "Nome", "Login", "Id",
				                                   "CargoId"
			                                   ]));
		detalhesUsuario.ConstruirLayout();

		ConfirmaView confirmaExclusao = new("Excluir Usuário");

		if (confirmaExclusao.Confirmar($"{detalhesUsuario.Layout}") is not 's')
			return;

		var foiExcluido
			= await BaseDeDados.Usuarios.Remover(obterUsuario.Modelo!.Id);

		var mensagemOperacao = foiExcluido.Status switch
		{
			StatusResposta.Sucesso =>
				$"{nameof(Usuario)} excluído com sucesso.",
			StatusResposta.ErroNaoEncontrado =>
				$"{nameof(Usuario)} não encontrado.",
			_ => $"Não foi possível excluir o {nameof(Usuario)}."
		};

		View.Aviso(mensagemOperacao);
	}

	public override void Visualizar()
	{
		RelatorioView<Usuario> relatorioView;

		if (TemAcessoRestrito)
		{
			var verUsuarios = BaseDeDados.Usuarios.ObterTodos();

			relatorioView
				= new RelatorioView<Usuario>("Visualizar Usuários",
				                             verUsuarios.Modelo!.ToList());
		}
		else
		{
			relatorioView = new RelatorioView<Usuario>("Minha Conta",
				[UsuarioContexto]);
		}

		PaginaView paginaView = new(relatorioView);
		paginaView.ConstruirLayout();
		paginaView.LerEntrada(true);
	}

	public override void VerDetalhes()
	{
		if (!TemAcessoRestrito)
		{
			DetalhesView detalhesContexto = new("Detalhes da Conta",
			                                    UtilitarioTipos
				                                    .ObterPropriedades(
					                                    UsuarioContexto,
					                                    [
						                                    "Login", "Nome",
						                                    "CargoId", "Id"
					                                    ]));
			detalhesContexto.ConstruirLayout();
			detalhesContexto.Exibir();


			return;
		}

		BuscaModeloView<Usuario>
			buscaUsuario = new("Buscar Usuário", ["Login"]);

		var (opcao, chaveBusca) = buscaUsuario.Buscar();

		var obterUsuario = opcao is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(uint.Parse(chaveBusca));

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Usuário não encontrado.");

			return;
		}

		var detalhes = UtilitarioTipos.ObterPropriedades(obterUsuario.Modelo,
		[
			"Login", "Nome", "Credenciais", "CargoId", "Id"
		]);

		DetalhesView detalhesUsuario = new("Usuário Encontrado", detalhes);
		detalhesUsuario.ConstruirLayout();
		detalhesUsuario.Exibir();
	}
}
