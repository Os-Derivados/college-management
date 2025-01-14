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
		var dadosUsuario  = cadastroUsuarioView.CadastroUsuario;

		if (confirmaCadastro is not 's') return;

		var cargoEscolhido = BaseDeDados
		                     .Cargos
		                     .ObterPorNome(dadosUsuario["Cargo"]);

		if (cargoEscolhido is null)
		{
			inputUsuario.LerEntrada("Erro",
			                        "O Cargo inserido não foi "
			                        + "encontrado na base de dados."
			                        + "Pressione Enter para continuar.");

			return;
		}

		var novaMatricula = cargoEscolhido.Nome
			is CargosPadrao.CargoAlunos
			? Matricula.CriarMatricula(dadosUsuario)
			: null;

		var cursoEscolhido = novaMatricula is not null
			? BaseDeDados
			  .Cursos
			  .ObterPorNome(dadosUsuario["Curso"])
			: null;

		var novoUsuario = Usuario.CriarUsuario(cargoEscolhido,
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
			novaMatricula.CursoId = cursoEscolhido.Id;

			var cadastroMatricula
				= await BaseDeDados.Matriculas.Adicionar(novaMatricula);
			
			foiAdicionado = foiAdicionado && cadastroMatricula.Status is StatusResposta.Sucesso;
		}

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Usuario)} cadastrado com sucesso."
			: $"Não foi possível cadastrar novo {nameof(Usuario)}.";

		inputUsuario.LerEntrada("Sair", mensagemOperacao);
	}

	public override async Task Editar()
	{
		if (!ValidarPermissoes()) return;

		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		var usuario = resultadoBusca.Key switch
		{
			1 => BaseDeDados.Usuarios.ObterPorLogin(chaveBusca),
			2 => BaseDeDados.Usuarios.ObterPorId(chaveBusca),
			_ => null
		};

		if (usuario is null)
		{
			InputView inputPesquisa = new("Erro ao buscar Usuario");

			inputPesquisa.LerEntrada("Usuario", "Usuário não encontrado.");

			return;
		}

		EditarUsuarioView editarUsuarioView = new(usuario, BaseDeDados.Cargos);
		var               usuarioEditado    = editarUsuarioView.Editar();

		ConfirmaView confirmaEdicao = new("Editar Usuário");

		if (confirmaEdicao.Confirmar("Editar Usuário") is not 's') return;

		var foiEditado = await BaseDeDados.Usuarios.Atualizar(usuarioEditado);

		var mensagemOperacao = foiEditado
			? $"{nameof(Usuario)} editado com sucesso."
			: $"Não foi possível editar o {nameof(Usuario)}.";

		InputView inputConfirmacao = new("Editar Usuário");

		inputConfirmacao.LerEntrada("Sair", mensagemOperacao);
	}

	public override async Task Excluir()
	{
		if (!ValidarPermissoes()) return;

		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		var usuario = resultadoBusca.Key switch
		{
			1 => BaseDeDados.Usuarios.ObterPorLogin(chaveBusca),
			2 => BaseDeDados.Usuarios.ObterPorId(chaveBusca),
			_ => null
		};

		if (usuario is null)
		{
			InputView inputPesquisa = new("Erro ao buscar Usuario");

			inputPesquisa.LerEntrada("Usuario", "Usuário não encontrado.");

			return;
		}


		DetalhesView detalhesUsuario = new("Excluir Usuário",
		                                   UtilitarioTipos.ObterPropriedades(
			                                   usuario,
			                                   [
				                                   "Nome", "Login", "Id",
				                                   "CargoId"
			                                   ]));
		detalhesUsuario.ConstruirLayout();

		ConfirmaView confirmaExclusao = new("Excluir Usuário");

		if (confirmaExclusao.Confirmar($"{detalhesUsuario.Layout}") is not 's')
			return;

		var foiExcluido = await BaseDeDados.Usuarios.Remover(usuario.Id);

		var mensagemOperacao = foiExcluido
			? $"{nameof(Usuario)} excluído com sucesso."
			: $"Não foi possível excluir o {nameof(Usuario)}.";

		InputView inputConfirmacao = new("Excluir Usuário");
		inputConfirmacao.LerEntrada("Sair", mensagemOperacao);
	}

	public override void Visualizar()
	{
		RelatorioView<Usuario> relatorioView;

		if (ValidarPermissoes())
		{
			var verUsuarios = BaseDeDados.Usuarios.ObterTodos(); 
			
			relatorioView = new RelatorioView<Usuario>("Visualizar Usuários", verUsuarios.Modelo!);
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
		if (!ValidarPermissoes())
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

		var usuario = resultadoBusca.Key switch
		{
			1 => BaseDeDados.Usuarios.ObterPorLogin(chaveBusca),
			2 => BaseDeDados.Usuarios.ObterPorId(chaveBusca),
			_ => null
		};

		if (usuario is null)
		{
			InputView inputErro = new("Erro ao buscar Usuario");

			inputErro.LerEntrada("Usuario",
			                     "Usuário não encontrado.");

			return;
		}

		var detalhes = UtilitarioTipos.ObterPropriedades(usuario,
		[
			"Login", "Nome", "Credenciais", "CargoId", "Id"
		]);

		DetalhesView detalhesUsuario = new("Usuário Encontrado", detalhes);
		detalhesUsuario.ConstruirLayout();
		detalhesUsuario.Exibir();
	}
}
