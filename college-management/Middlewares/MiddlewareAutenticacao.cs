using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Funcionalidades;
using college_management.Utilitarios;

namespace college_management.Middlewares;

public static class MiddlewareAutenticacao
{
    public static Usuario Login(bool modoDesenvolvimento,
                                RepositorioUsuarios repositorioUsuarios)
    {
        var autenticacao = new Autenticacao(repositorioUsuarios);
        EstadoDoApp estadoAtual;
        Usuario usuarioLogado;

        do
        {
            if (modoDesenvolvimento)
            {
                _ = UtilitarioAmbiente.Variaveis.TryGetValue(
                    VariaveisDeAmbiente.UsuarioTesteLogin,
                    out var loginTeste);
                usuarioLogado = repositorioUsuarios
                    .ObterPorLogin(loginTeste);

                if (usuarioLogado is null)
                    throw new InvalidOperationException(
                        "Usuário de teste não foi encontrado");

                estadoAtual = EstadoDoApp.Contexto;

                break;
            }

            usuarioLogado = autenticacao.Login();

            estadoAtual = EstadoDoApp.Contexto;
        } while (estadoAtual is EstadoDoApp.Login);

        return usuarioLogado;
    }
}
