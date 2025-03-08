using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoUsuarios : Contexto<Usuario>,
                                IContextoUsuarios
{
	public ContextoUsuarios(BaseDeDados baseDeDados,
	                        Usuario usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto)
	{
	}

	public void VerMatricula()
	{
		// TODO: Desenvolver um algoritmo para visualizar Matricula de um Aluno
		// [REQUISITO]: A visualização deve ser no formato descritivo
		// 
		// Ex.: Ver Matricula 2401123415
		//
		// Nome: Thiago
		// Matricula: 2401123415
		// Curso: Ciência da Computação
		// Período: 2

		if (ValidarPermissoes())
			// [REQUISITO]: A visualização do Gestor deve permitir a busca
			// de um Aluno em específico na base de dados
			//
			// Ex.: Ver Matricula do Aluno com Login == "thiago.santos" 
			//
			// [Ver Grade Horária]
			// Selecione um campo abaixo campo para realizar a busca
			//
			// [1] Login
			// [2] Id
			// [3] Matricula
			// 
			// Sua opção: 1 <- Opção que o usuário escolheu 
			// ...
			//
			// Digite o Login do Aluno: thiago.santos <- Nome
			// digitado pelo Gestor
			// ...
			throw new NotImplementedException();

		// [REQUISITO]: A visualização do Aluno deve ser somente
		// da Matricula vinculada a ele

		throw new NotImplementedException();
	}

	public void VerBoletim()
	{
		// TODO: Desenvolver um algoritmo para visualizar as Notas de um Aluno
		// [REQUISITO]: A visualização deve ser no formato relatório
		// 
		// Ex.: Ver Boletim do Aluno com Matricula 2401123415
		//
		// | MATERIA        | NOTA FINAL | STATUS   |
		// |----------------|------------|----------|
		// | Calculo 1      |    9.0     | Aprovado |
		// | Algebra Linear |    N/A     |   N/A    |

		if (ValidarPermissoes())
			// [REQUISITO]: A visualização do Gestor deve permitir a busca
			// de uma Aluno em específico na base de dados
			//
			// Ex.: Ver Boletim do Aluno com Login == "thiago.santos" 
			//
			// Selecione um campo abaixo campo para realizar a busca
			//
			// [1] Login
			// [2] Id
			// [3] Matricula
			// 
			// Sua opção: 1 <- Opção que o usuário escolheu 
			// ...
			//
			// Digite o Login do Aluno: thiago.santos <- Nome
			// digitado pelo Gestor
			// ...
			throw new NotImplementedException();

		// [REQUISITO]: A visualização do Aluno deve ser somente
		// da Matricula vinculada a ele

		throw new NotImplementedException();
	}

	public void VerFinanceiro() { throw new NotImplementedException(); }

	public override async Task Cadastrar()
	{
		InputView inputUsuario = new("Cadastrar Usuário");
		inputUsuario.ConstruirLayout();

		if (!ValidarPermissoes()) return;

		CadastroUsuarioView cadastroUsuarioView = new();

		var confirmaCadastro = cadastroUsuarioView.ObterDados();
		var dadosUsuario     = cadastroUsuarioView.CadastroUsuario;

		if (confirmaCadastro is not 's') return;

		var obterCargoPorNome = BaseDeDados
		                        .Cargos
		                        .ObterPorNome(dadosUsuario["Cargo"]);

		if (obterCargoPorNome.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("O Cargo inserido não foi encontrado na base de dados.");

			return;
		}

		var novaMatricula = obterCargoPorNome.Modelo!.Nome
			is CargosPadrao.CargoAlunos
			? Matricula.CriarMatricula(dadosUsuario)
			: null;

		var cursoEscolhido = novaMatricula is not null
			? BaseDeDados
			  .Cursos
			  .ObterPorNome(dadosUsuario["Curso"])
			: null;

		var novoUsuario = Usuario.CriarUsuario(obterCargoPorNome.Modelo,
		                                       dadosUsuario,
		                                       novaMatricula!);

		var cadastroUsuario = await BaseDeDados
		                            .Usuarios
		                            .Adicionar(novoUsuario);

		var foiAdicionado = cadastroUsuario.Status is StatusResposta.Sucesso;

		if (foiAdicionado
		    && novaMatricula is not null
		    && cursoEscolhido is not null)
		{
			novaMatricula.AlunoId = novoUsuario.Id;
			novaMatricula.CursoId = cursoEscolhido.Modelo!.Id;

			var cadastroMatricula
				= await BaseDeDados.Matriculas.Adicionar(novaMatricula);

			foiAdicionado = foiAdicionado &&
			                cadastroMatricula.Status is StatusResposta.Sucesso;
		}

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Usuario)} cadastrado com sucesso."
			: $"Não foi possível cadastrar novo {nameof(Usuario)}.";

		View.Aviso(mensagemOperacao);
	}

	public override async Task Editar()
	{
		if (!ValidarPermissoes()) return;

		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		var obterUsuario = resultadoBusca.Key is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(chaveBusca);

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso("Usuário não encontrado na base de dados.");

			return;
		}

		EditarUsuarioView editarUsuarioView
			= new(obterUsuario.Modelo!, BaseDeDados.Cargos);
		var usuarioEditado = editarUsuarioView.Editar();

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

		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		var obterUsuario = resultadoBusca.Key is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(chaveBusca);

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
				                             verUsuarios.Modelo!);
		}
		else
		{
			relatorioView = new RelatorioView<Usuario>("Minha Conta",
				[UsuarioContexto]);
		}

		relatorioView.ConstruirLayout();
		relatorioView.Exibir();
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

		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		var obterUsuario = resultadoBusca.Key is 1
			? BaseDeDados.Usuarios.ObterPorLogin(chaveBusca)
			: BaseDeDados.Usuarios.ObterPorId(chaveBusca);

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
