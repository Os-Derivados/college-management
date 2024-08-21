using System.Text;
using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Funcionalidades;

namespace college_management.Middlewares;

public static class MiddlewareContexto
{
    public static void Inicializar(BaseDeDados baseDeDados, Usuario usuario)
    {
        var contextoAtual = new Contexto(baseDeDados, usuario);
        var estadoAtual = EstadoDoApp.Contexto;

        do
        {
            Console.WriteLine(
                "Bem-vindo(a). Selecione um dos contextos abaixo.");
            var recursos = ListarContextos(usuario);

            var opcaoEscolhida = Console.ReadLine();

            var opcaoValida =
                int.TryParse(opcaoEscolhida, out var opcaoUsuario);

            if (!opcaoValida) continue;

            if (opcaoUsuario is 0)
                estadoAtual = EstadoDoApp.Sair;

            Console.Clear();

            _ = recursos.TryGetValue(opcaoUsuario,
                                     out var recursoSelecionado);
            
            contextoAtual.AcessarRecurso(recursoSelecionado);
        } while (estadoAtual is EstadoDoApp.Contexto);

        Console.WriteLine("Saindo...");
    }

    private static Dictionary<int, string> ListarContextos(Usuario usuario)
    {
        StringBuilder mensagem = new();
        Dictionary<int, string> dicionarioOpcoes = new(); 

        var opcoes = usuario.Cargo.Nome switch
        {
            CargosDeAcesso.CargoAlunos =>
                OperacoesDeContexto.AcessoAlunos,
            CargosDeAcesso.CargoGestores
                or CargosDeAcesso.CargoAdministradores =>
                OperacoesDeContexto.AcessoGestoresAdministradores,
            _ => throw new InvalidOperationException(
                     "O usuário não possui um cargo validado")
        };

        for (var i = 0; i < opcoes.Length; i++)
        {
            mensagem.AppendLine($"{i + 1} - {opcoes[i]}");
            dicionarioOpcoes.Add(i + 1, opcoes[i]);
        }   
        
        Console.WriteLine(mensagem);

        return dicionarioOpcoes;
    }
}
