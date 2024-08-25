using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Utilitarios;

namespace college_management.Middlewares;

public static class MiddlewareAutenticacao
{
    public static Usuario Login(bool modoDesenvolvimento,
                                RepositorioUsuarios repositorioUsuarios)
    {
        return modoDesenvolvimento 
                   ? ObterUsuarioTeste(repositorioUsuarios) 
                   : Login(repositorioUsuarios);
    }

    private static Usuario ObterUsuarioTeste(RepositorioUsuarios repositorioUsuarios)
    {
        _ = UtilitarioAmbiente.Variaveis.TryGetValue(
            VariaveisAmbiente.UsuarioTesteLogin,
            out var loginTeste);
        var usuarioLogado = repositorioUsuarios
            .ObterPorLogin(loginTeste);

        if (usuarioLogado is null)
            throw new InvalidOperationException(
                "Usuário de teste não foi encontrado");

        return usuarioLogado;
    }
    
    private static Usuario Login(RepositorioUsuarios repositorioUsuarios)
    {
        // TODO:
        // 1. Pedir o login
        // 2 Pedir a senha
        // 3. Verificar se o login existe
        // 3.1. Se não existir -> Login ou a senha estão inválidos
        // 3.2. Se o login está válido -> Validar a senha
        // 3.3. Se a senha estiver errada -> Login ou senha estão inválidos
        // 3.4. Se a senha estiver válida -> Retornar o usuário encontrado na base de dados

        throw new InvalidOperationException(
            "Não foi possível obter usuário");
    }
}
