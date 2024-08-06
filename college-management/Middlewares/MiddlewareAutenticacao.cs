using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Modelos;

namespace college_management.Middlewares;

public static class MiddlewareAutenticacao
{
    public static Usuario Login(bool modoDesenvolvimento,
                                RepositorioUsuarios baseDeUsuarios)
    {
        var estadoAtual = EstadoDoApp.Login;
        Usuario usuarioLogado;

        do
        {
            if (modoDesenvolvimento)
            {
                var usuarioTeste =
                    baseDeUsuarios.ObterPorLogin("master.admin");

                if (usuarioTeste is null)
                    throw new InvalidOperationException(
                        "Usuário de teste não foi encontrado");

                estadoAtual = EstadoDoApp.Contexto;

                return usuarioTeste;
            }

            // 1. Pedir o login dele
            // 2. Verificar se usuario existe
            // 2.1. Se o usuário existe, você pede a senha dele
            // 2.1.1. Autenticar o usário 

            estadoAtual = EstadoDoApp.Contexto;
        } while (estadoAtual is EstadoDoApp.Login);

        return new Usuario("thiago.santos",
                           "Thiago",
                           new Cargo(CargosDeAcesso.CargoAlunos),
                           "12345");
    }
}
