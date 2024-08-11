using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Utilitarios;

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
                _ = Ambiente.Variaveis.TryGetValue(
                    VariaveisDeAmbiente.UsuarioTesteLogin,
                    out var loginTeste);
                usuarioLogado = baseDeUsuarios
                    .ObterPorLogin(loginTeste);

                if (usuarioLogado is null)
                    throw new InvalidOperationException(
                        "Usuário de teste não foi encontrado");

                estadoAtual = EstadoDoApp.Contexto;

                break;
            }

            // 1. Pedir o login dele
            // 2 Pedir a senha dele
            // 3. Verificar se o login existe
            // 3.1. Se não existir -> Login ou a senha estão inválidos
            // 3.2. Se o login está válido -> Validar a senha
            // 3.3. Se a senha estiver errada -> Login ou senha estão inválidos
            // 3.4. Se a senha estiver válida -> Retornar o usuário encontrado na base de dados

            // O valor abaixo é para fins de teste e deve ser substituído por um valor
            // que seja obtido pelo processo de login:

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
                                                    Turno.Noturno, 60)
                                            ]),
                                        Modalidade.Presencial));

            // O estado atual só deve mudar se o login for validado.
            // Caso contrário, o loop deve pular a iteração

            estadoAtual = EstadoDoApp.Contexto;
        } while (estadoAtual is EstadoDoApp.Login);

        if (usuarioLogado is null)
            throw new InvalidOperationException(
                "Credenciais inválidas: não foi possível obter usuário");

        return usuarioLogado;
    }
}
