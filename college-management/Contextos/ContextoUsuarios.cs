using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoUsuarios : Contexto<Usuario>,
                                IContextoUsuarios
{
	public ContextoUsuarios(BaseDeDados baseDeDados,
	                        Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

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

		if (UsuarioContexto.Cargo.TemPermissao(PermissoesAcesso
			                                       .PermissaoAcessoEscrita))
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

		if (UsuarioContexto.Cargo.TemPermissao(PermissoesAcesso
			                                       .PermissaoAcessoEscrita))
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

	public void VerFinanceiro()
	{
		throw new NotImplementedException();
	}

	public override async Task Cadastrar()
	{
		InputView inputUsuario =
			new("Cadastrar Usuário", "Insira o Nome: ");

		inputUsuario.ConstruirLayout();

		KeyValuePair<string, string?>[] camposUsuario = [
			new("Nome", null),
			new("Login", "Insira o Login: "),
			new("Senha", "Insira a Senha: "),
			new("Cargo", "Insira o Cargo: ")
		];

		foreach (KeyValuePair<string, string?> campo 
		         in camposUsuario)
		{
			inputUsuario.LerEntrada(campo.Key, campo.Value);
		}

		KeyValuePair<string, string?>[] camposAluno = [
			new("Matricula", "Insira a Matrícula: "),
			new("Periodo", "Insira o Período: "),
			new("Curso", "Insira o nome do Curso: "),
			new("Cargo", "Insira o Cargo: ")
		];
		
		if (inputUsuario.ObterEntrada("Cargo") is CargosPadrao
			    .CargoAlunos)
		{
			foreach (KeyValuePair<string, string?> campo 
			         in camposAluno)
			{
				inputUsuario.LerEntrada(campo.Key, campo.Value);
			}
		}

		DetalhesView detalhesView = new("Confirmar Cadastro",
		                                inputUsuario.EntradasUsuario);
		
		detalhesView.ConstruirLayout();

		StringBuilder mensagemConfirmacao = new();
		mensagemConfirmacao.AppendLine(detalhesView.Layout.ToString());
		mensagemConfirmacao.Append("[S] Sim\t[N] Não: ");
		
		inputUsuario.LerEntrada("Confirma", 
		                        mensagemConfirmacao.ToString());
	}

	public override async Task Editar()
	{
		throw new NotImplementedException();
	}

	public override async Task Excluir()
	{
		throw new NotImplementedException();
	}

	public override void Visualizar()
	{
		throw new NotImplementedException();
	}
}
