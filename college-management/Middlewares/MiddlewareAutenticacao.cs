using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Utilitarios;


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

	private static Usuario ObterUsuarioTeste(
		RepositorioUsuarios repositorioUsuarios)
	{
		_ = UtilitarioAmbiente.Variaveis
		                      .TryGetValue(VariaveisAmbiente
			                                   .UsuarioTesteLogin,
		                                   out var loginTeste);

		return repositorioUsuarios.ObterPorLogin(loginTeste);
	}

	private static Usuario Login(
		RepositorioUsuarios repositorioUsuarios)
	{
		// TODO: Desenvolver um algoritmo para autenticar um usuário
		// [REQUISITO]: O usuário deve existir na base de dados.
		// [REQUISITO]: O login e senha devem ser validados, avisando o usuário
		// sobre credenciais inválidas, caso qualquer um dos dois campos
		// esteja incorretamente digitado

		throw new
			InvalidOperationException("Não foi possível obter usuário");
	}
}
