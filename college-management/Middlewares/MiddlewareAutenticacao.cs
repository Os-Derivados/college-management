using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Middlewares;


public static class MiddlewareAutenticacao
{
	public static Usuario Autenticar(bool modoDesenvolvimento,
	                                 RepositorioUsuarios
		                                 repositorioUsuarios)
	{
		return modoDesenvolvimento
			? ObterUsuarioTeste(repositorioUsuarios)
			: Login(repositorioUsuarios);
	}

	private static Usuario? ObterUsuarioTeste(
		RepositorioUsuarios repositorioUsuarios)
	{
		_ = UtilitarioAmbiente.Variaveis
		                      .TryGetValue(VariaveisAmbiente.LoginTeste,
		                                   out var loginTeste);

		return repositorioUsuarios.ObterPorLogin(loginTeste!).Modelo;
	}

	private static Usuario Login(RepositorioUsuarios repositorioUsuarios)
	{
		InputView inputView = new("Login: Preencha com as credenciais do usuário.");
		inputView.LerEntrada("Login", "Insira o login: ");
		inputView.LerEntrada("Senha", "Insira a senha: ");

		var loginUsuario = inputView.ObterEntrada("Login");
		var senhaUsuario = inputView.ObterEntrada("Senha");
		
		var autenticacao
			= Usuario.Autenticar(repositorioUsuarios, loginUsuario,
				senhaUsuario);

		if (autenticacao is not null)
		{
			View.Aviso("Login efetuado com sucesso!");
			return autenticacao;
		}

		View.Aviso("Login ou senha incorretos!");
		return Login(repositorioUsuarios);
	}
}
