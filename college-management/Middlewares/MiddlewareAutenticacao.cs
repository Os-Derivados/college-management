using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

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
            // 2 pedir a senha dele
            // 2.1. Autenticar o usuário

            // O valor abaixo deve ser substituído por um valor
            // que seja obtido pelo processo de login;

            usuarioLogado =
                new Aluno("thiago.santos",
                          "Thiago Rodrigues",
                          new Cargo(CargosDeAcesso.CargoAlunos),
                          "aluno12345",
                          new Matricula(2412130152,
                                        2024,
                                        new Curso(
                                            "Ciência da Computação",
                                            [
                                                new Materia(
                                                    "Sistemas Digitais",
                                                    Turno
                                                        .Noturno,
                                                    60)
                                            ]),
                                        Modalidade
                                            .Presencial));
            
            // O estado atual só deve mudar se o login for validado.
            // Caso contrário, o loop deve continuar
            
            estadoAtual = EstadoDoApp.Contexto;
        } while (estadoAtual is EstadoDoApp.Login);

        if (usuarioLogado is null)
            throw new InvalidOperationException(
                "Credenciais inválidas: não foi possível obter usuário");

        return usuarioLogado;
    }
}
