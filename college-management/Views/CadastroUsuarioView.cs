using System.Text;
using college_management.Constantes;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class CadastroUsuarioView : ICadastroUsuarioView
{
	public Dictionary<string, string> CadastroUsuario;

	public void ObterDados()
	{
		InputView inputCadastro = new("Cadastro de Usuário");
		
		KeyValuePair<string, string?>[] mensagensUsuario =
		[
			new("Nome", "Insira o Nome: "),
			new("Login", "Insira o Login: "),
			new("Senha", "Insira a Senha: "),
			new("Cargo", "Insira o Cargo: ")
		];

		foreach (KeyValuePair<string, string?> mensagem
		         in mensagensUsuario)
			inputCadastro.LerEntrada(mensagem.Key,
			                        mensagem.Value);

		KeyValuePair<string, string?>[] mensagensAluno =
		[
			new("Periodo", "Insira o Período: "),
			new("Curso", "Insira o nome do Curso: "),
			new("Modalidade", "Insira a Modalidade: ")
		];

		if (inputCadastro.ObterEntrada("Cargo")
		    is CargosPadrao.CargoAlunos)
			foreach (KeyValuePair<string, string?> mensagem
			         in mensagensAluno)
				inputCadastro.LerEntrada(mensagem.Key,
				                        mensagem.Value);

		DetalhesView detalhesView = new("Confirmar Cadastro",
		                                inputCadastro
			                                .EntradasUsuario);

		detalhesView.ConstruirLayout();

		StringBuilder mensagemConfirmacao = new();
		mensagemConfirmacao.AppendLine(detalhesView.Layout
		                                           .ToString());

		mensagemConfirmacao.AppendLine("Confirma o Cadastro?\n");
		mensagemConfirmacao.Append("[S]im\t[N]ão: ");

		inputCadastro.LerEntrada("Confirma",
		                        mensagemConfirmacao.ToString());

		CadastroUsuario = inputCadastro.EntradasUsuario;
	}
}
