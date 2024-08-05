using System.Text;
using college_management.Constantes;
using college_management.Modelos;

namespace college_management.Middlewares;

public static class Contexto
{
    public static void Inicializar(Usuario usuario)
    {
        var estadoAtual = EstadoDoApp.Contexto;
        int contextoEscolhido;
        
        do
        {
            Console.WriteLine("Bem-vindo(a). Selecione um dos contextos abaixo.");
            ListarContextos(usuario);

            var opcaoEscolhida = Console.ReadLine();

            var opcaoValida = int.TryParse(opcaoEscolhida, out contextoEscolhido);

            if (!opcaoValida) continue;

            if (contextoEscolhido is 0)
                estadoAtual = EstadoDoApp.Sair;
    
            Console.Clear();
        } while (estadoAtual is EstadoDoApp.Contexto);
        
        Console.WriteLine("Saindo...");
    }
    
    private static void ListarContextos(Usuario usuario)
    {
        StringBuilder contextos = new();
        contextos.AppendLine("0. Sair");

        switch (usuario.Cargo.Nome)
        {
            case CargosDeAcesso.CargoAlunos:
                contextos.AppendLine("1. Cursos");

                break;
            
            case CargosDeAcesso.CargoGestores or CargosDeAcesso.CargoAdministradores:
                contextos.AppendLine("1. Cursos");
                contextos.AppendLine("2. Cadastros");

                break;
            
            default:
                throw new InvalidOperationException("O usuário não possui um cargo válido");
        }
        
        Console.WriteLine(contextos);
    }
}