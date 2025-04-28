using System.Text;
using college_management.Constantes;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class CadastroUsuarioView : ICadastroView
{
	public Dictionary<string, string>? CadastroUsuario;

	public char ObterDados()
	{
		InputView inputCadastro = new("Cadastro de Usuário");

		KeyValuePair<string, string?>[] mensagensUsuario =
		[
			new("Nome", "Insira o Nome: "),
			new("Login", "Insira o Login: "),
			new("Senha", "Insira a Senha: "),
			new("Tipo", "Insira o Tipo: ")
		];

		foreach (var mensagem in mensagensUsuario)
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
