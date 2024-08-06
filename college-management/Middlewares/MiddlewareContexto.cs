using System.Text;
using college_management.Constantes;
using college_management.Modelos;

namespace college_management.Middlewares;

public static class MiddlewareContexto
{
    public static void Inicializar(Usuario usuario)
    {
        var estadoAtual = EstadoDoApp.Contexto;
        int contextoEscolhido;

        do
        {
            Console.WriteLine(
                "Bem-vindo(a). Selecione um dos contextos abaixo.");
            ListarContextos(usuario);

            var opcaoEscolhida = Console.ReadLine();

            var opcaoValida =
                int.TryParse(opcaoEscolhida, out contextoEscolhido);

            if (!opcaoValida) continue;

            if (contextoEscolhido is 0)
                estadoAtual = EstadoDoApp.Sair;

            Console.Clear();
        } while (estadoAtual is EstadoDoApp.Contexto);

        Console.WriteLine("Saindo...");
    }

    private static void ListarContextos(Usuario usuario)
    {
        StringBuilder contexto = new();

        var opcoes = usuario.Cargo.Nome switch
        {
            CargosDeAcesso.CargoAlunos =>
                AcessosDeContexto.AcessoAlunos,
            CargosDeAcesso.CargoGestores
                or CargosDeAcesso.CargoAdministradores =>
                AcessosDeContexto.AcessoGestoresAdministradores,
            _ => throw new InvalidOperationException(
                     "O usuário não possui um cargo validado")
        };

        foreach (var opcao in opcoes) contexto.AppendLine(opcao);

        Console.WriteLine(contexto);
    }
}
