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
		Console.Write("Login: ");
		var loginUsuario = Console.ReadLine() ?? "";

		Console.Clear();

		Console.Write("Senha: ");
		var senhaUsuario = Console.ReadLine() ?? "";

		var autenticacao
			= Usuario.Autenticar(repositorioUsuarios, loginUsuario,
			                     senhaUsuario);

		if (autenticacao != null)
		{
			View.Aviso("Login efetuado com sucesso!");
			
			return autenticacao;
		}

		View.Aviso("Login ou senha incorretos!");
		
		return null;
	}
}
