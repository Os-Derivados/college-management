using System.Text;
using college_management.Constantes;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class CadastroUsuarioView : ICadastroView
{
	public Dictionary<string, string> CadastroUsuario;

	public char ObterDados()
	{
		InputView inputCadastro = new("Cadastro de Usuário");

		KeyValuePair<string, string?>[] mensagensUsuario =
		[
			new("Nome", "Insira o Nome: "),
			new("Login", "Insira o Login: "),
			new("Senha", "Insira a Senha: "),
			new("Cargo", "Insira o Cargo: ")
		];

		foreach (var mensagem
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
		    is TipoUsuario.CargoAlunos)
			foreach (var mensagem in mensagensAluno)
				inputCadastro.LerEntrada(mensagem.Key, mensagem.Value);

		CadastroUsuario = inputCadastro.EntradasUsuario;


		DetalhesView detalhesView = new("Confirmar Cadastro",
		                                inputCadastro.EntradasUsuario);
		detalhesView.ConstruirLayout();

		StringBuilder mensagemConfirmacao = new();
		mensagemConfirmacao.Append(detalhesView.Layout);

		ConfirmaView confirmarCadastro = new("Cadastrar Usuário");

		return confirmarCadastro.Confirmar(mensagemConfirmacao.ToString());
	}
}
