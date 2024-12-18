using System.Text;
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

		if (CargoContexto.TemPermissao(PermissoesAcesso
			                               .AcessoEscrita))
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

		if (CargoContexto.TemPermissao(PermissoesAcesso
			                               .AcessoEscrita))
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
		if (!AcessoRestrito()) return;

		CadastroUsuarioView inputCadastro = new("Cadastro de Usuário");
		inputCadastro.Cadastrar();

		DetalhesView detalhesView
			= new("Confirmar Cadastro", inputCadastro.DadosCadastro);

		ConfirmacaoView confirmarCadastro = new("Confirmar Cadastro");
		confirmarCadastro.Confirmar(detalhesView.ConstruirLayout());

		if (confirmarCadastro.Confirmacao is not "s") return;

		var (nome, login, cargo, senha) = inputCadastro.ObterDadosBase();

		var cargoEscolhido = BaseDeDados.Cargos.ObterPorNome(cargo);

		ValidacaoView<Cargo> validacaoCargo = new("Validação Cargo", cargoEscolhido);

		if (! validacaoCargo.ValidarModelo()) return;

		var novaMatricula = cargoEscolhido.Nome is CargosPadrao.CargoAlunos
			? CriarMatricula(inputCadastro.DadosCadastro)
			: null;

		var cursoEscolhido = novaMatricula is not null
			? BaseDeDados
			  .Cursos
			  .ObterPorNome(inputCadastro.DadosCadastro["Curso"])
			: null;

		Usuario novoUsuario = cargoEscolhido.Nome switch
		{
			CargosPadrao.CargoAlunos => new Aluno(login,
			                                      nome,
			                                      new CredenciaisUsuario(senha),
			                                      cargoEscolhido.Id!,
			                                      novaMatricula!.Id!),
			_ => new Funcionario(login,
			                     nome,
			                     new CredenciaisUsuario(senha),
			                     cargoEscolhido.Id!)
		};

		var foiAdicionado = await BaseDeDados.Usuarios.Adicionar(novoUsuario);

		if (foiAdicionado && novaMatricula is not null
		                  && cursoEscolhido is not null)
		{
			novaMatricula.AlunoId = novoUsuario.Id;
			novaMatricula.CursoId = cursoEscolhido.Id;

			foiAdicionado
				= await BaseDeDados.Matriculas.Adicionar(novaMatricula);
		}

		var mensagemOperacao = foiAdicionado
			? $"{nameof(Usuario)} cadastrado com sucesso."
			: $"Não foi possível cadastrar novo {nameof(Usuario)}.";

		InputView inputSucesso = new("Usuário Cadastrado");
		inputSucesso.ConstruirLayout();

		inputSucesso.LerEntrada("Sair", mensagemOperacao);
	}

	private Matricula CriarMatricula(Dictionary<string, string> cadastroUsuario)
	{
		var conversaoValida = int.TryParse(cadastroUsuario["Periodo"],
		                                   out var periodoCurso);

		if (!conversaoValida) return null;

		var modalidadeCurso =
			cadastroUsuario["Modalidade"] switch
			{
				"Ead"        => Modalidade.Ead,
				"Presencial" => Modalidade.Presencial,
				"Hibrido"    => Modalidade.Hibrido,
				_            => Modalidade.Invalido
			};

		if (modalidadeCurso is Modalidade.Invalido) return null;

		Matricula novaMatricula = new(periodoCurso, modalidadeCurso);

		return novaMatricula;
	}

	public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir()
	{
		throw new NotImplementedException();
	}

	public override void Visualizar()
	{
		var naoTemRestricao
			= CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita)
			  || CargoContexto.TemPermissao(
				  PermissoesAcesso.AcessoAdministradores);

		RelatorioView<Usuario> relatorioView;

		if (naoTemRestricao)
			relatorioView = new RelatorioView<Usuario>("Visualizar Usuários",
				BaseDeDados.Usuarios.ObterTodos());
		else
			relatorioView
				= new RelatorioView<Usuario>("Minha Conta", [UsuarioContexto]);


		InputView inputRelatorio = new(relatorioView.Titulo);
		inputRelatorio.LerEntrada("Sair", relatorioView.ConstruirLayout());
	}

	public override void VerDetalhes()
	{
		var temPermissao
			= CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores)
			  || CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita);

		if (!temPermissao)
		{
			var detalhesContexto = UtilitarioTipos.ObterPropriedades(
				UsuarioContexto,
				[
					"Login", "Nome",
					"Credenciais", "CargoId",
					"Id"
				]);

			DetalhesView detalhesView
				= new("Detalhes Usuário", detalhesContexto);

			detalhesView.ConstruirLayout();
			detalhesView.Exibir();

			return;
		}

		MenuView menuPesquisa = new("Pesquisar Usuário",
		                            "Selecione um dos campos para pesquisar.",
		                            ["Login", "Id"]);

		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		KeyValuePair<string, string>? campoPesquisa
			= menuPesquisa.OpcaoEscolhida switch
			{
				1 => new KeyValuePair<string, string>("Login",
					"Insira o Login do Usuario: "),
				2 => new KeyValuePair<string, string>("Id",
					"Insira o Id do Usuario: "),
				_ => null
			};

		InputView inputPesquisa = new("Ver Detalhes: Pesquisar Usuario");

		if (campoPesquisa is null)
		{
			inputPesquisa.LerEntrada("Campo",
			                         "Campo inválido. Tente novamente.");

			return;
		}

		inputPesquisa.LerEntrada(campoPesquisa?.Key!,
		                         campoPesquisa?.Value);

		Usuario? usuario = null;

		switch (menuPesquisa.OpcaoEscolhida)
		{
			case 1:
			{
				var login = inputPesquisa.ObterEntrada("Login");
				usuario = BaseDeDados.Usuarios.ObterPorLogin(login);
				break;
			}
			case 2:
			{
				var id = inputPesquisa.ObterEntrada("Id");
				usuario = BaseDeDados.Usuarios.ObterPorId(id);
				break;
			}
		}

		if (usuario is null)
		{
			inputPesquisa.LerEntrada("Usuario",
			                         "Usuário não encontrado.");

			return;
		}

		var detalhes =
			UtilitarioTipos.ObterPropriedades(usuario,
			[
				"Login", "Nome", "Credenciais", "CargoId", "Id"
			]);

		DetalhesView detalhesUsuario = new("Usuário Encontrado", detalhes);
		detalhesUsuario.ConstruirLayout();
		detalhesUsuario.Exibir();
	}
}
