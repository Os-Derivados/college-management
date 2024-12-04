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

    private static Usuario ObterUsuarioTeste(RepositorioUsuarios repositorioUsuarios)
    {
        _ = UtilitarioAmbiente.Variaveis
            .TryGetValue(VariaveisAmbiente.LoginTeste,
                out var loginTeste);

        return repositorioUsuarios.ObterPorLogin(loginTeste);
    }

    private static Usuario Login(RepositorioUsuarios repositorioUsuarios)
    {
        // TODO: Desenvolver um algoritmo para autenticar um usuário
        // [REQUISITO]: O usuário deve existir na base de dados.
        // [REQUISITO]: O login e senha devem ser validados, avisando o usuário
        // sobre credenciais inválidas, caso qualquer um dos dois campos
        // esteja incorretamente digitado

        string loginUsuario,
            senhaUsuario;

        Console.Write("Login: ");
        loginUsuario = Console.ReadLine() ?? "";

        Console.Clear();

        Console.Write("Senha: ");
        senhaUsuario = Console.ReadLine() ?? "";

        var autenticacao = Usuario.Autenticar(repositorioUsuarios, loginUsuario, senhaUsuario);

        if (autenticacao != null)
        {
            Console.WriteLine("Login efetuado com sucesso!");
            return autenticacao;
        }

        Console.WriteLine("Login ou senha incorretos!");
        return null;
    }
}