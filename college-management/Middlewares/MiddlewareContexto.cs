using System.Text;
using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Contextos;

namespace college_management.Middlewares;

public static class MiddlewareContexto
{
    public static void Inicializar(BaseDeDados baseDeDados,
                                   Usuario usuario)
    {
        var contextoEscolhido = EscolherContexto(usuario);

        if (contextoEscolhido is not "")
            AcessarContexto(contextoEscolhido, baseDeDados, usuario);

        Console.Clear();
        Console.WriteLine("Saindo...");
    }

    private static void AcessarContexto(string contextoEscolhido,
                                        BaseDeDados baseDeDados,
                                        Usuario usuario)
    {
        var estadoAtual = EstadoDoApp.Recurso;

        do
        {
            Console.Clear();

            dynamic contexto = contextoEscolhido switch
            {
                OperacoesContexto.AcessarCursos => new ContextoCursos(),
                OperacoesContexto.AcessarMaterias =>
                    new ContextoMaterias(),
                OperacoesContexto.AcessarCargos => new ContextoCargos(),
                OperacoesContexto.AcessarUsuarios =>
                    new ContextoUsuarios(),
                _ => throw new InvalidOperationException(
                         "Contexto inválido")
            };

            contexto.ListarOpcoes();

            var opcaoEscolhida = Console.ReadKey();

            if (opcaoEscolhida.Key is ConsoleKey.D0)
            {
                estadoAtual = EstadoDoApp.Sair;

                break;
            }

            var recursoEscolhido =
                ConverterOpcaoEmMetodo(contextoEscolhido,
                                       opcaoEscolhida);

            Console.Clear();

            contexto.AcessarRecurso(recursoEscolhido,
                                    baseDeDados,
                                    usuario);
        } while (estadoAtual is EstadoDoApp.Recurso);
    }

    private static string ConverterOpcaoEmMetodo(
        string contextoEscolhido,
        ConsoleKeyInfo opcaoEscolhida)
    {
        var recursosDisponiveis = contextoEscolhido switch
        {
            OperacoesContexto.AcessarUsuarios =>
                OperacoesRecurso.OperacoesUsuarios,
            OperacoesContexto.AcessarCursos =>
                OperacoesRecurso.OperacoesCursos,
            OperacoesContexto.AcessarMaterias =>
                OperacoesRecurso.OperacoesMaterias,
            OperacoesContexto.AcessarCargos =>
                OperacoesRecurso.OperacoesCargos,
            _ => throw new InvalidOperationException(
                     "Não há contexto definido para este tipo")
        };

        _ = int.TryParse(opcaoEscolhida.KeyChar.ToString(), out var i);

        var recursoEscolhido = recursosDisponiveis
                               .Select(r => r.Trim().Replace(" ", ""))
                               .ElementAt(i - 1);

        return recursoEscolhido;
    }

    

    private static string EscolherContexto(Usuario usuario)
    {
        var estadoAtual = EstadoDoApp.Contexto;
        var contextoEscolhido = "";

        do
        {
            Console.WriteLine(
                "Bem-vindo(a). Selecione um dos contextos abaixo.");

            var recursos = ListarContextos(usuario);
            var opcaoEscolhida = Console.ReadLine();
            var opcaoValida = int
                .TryParse(opcaoEscolhida, out var opcaoUsuario);

            if (!opcaoValida) continue;

            if (opcaoUsuario is 0)
                break;

            Console.Clear();

            _ = recursos.TryGetValue(opcaoUsuario,
                                     out contextoEscolhido);

            estadoAtual = EstadoDoApp.Recurso;
        } while (estadoAtual is EstadoDoApp.Contexto);

        return contextoEscolhido;
    }

    private static Dictionary<int, string> ListarContextos(
        Usuario usuario)
    {
        StringBuilder mensagem = new();
        Dictionary<int, string> dicionarioOpcoes = new();

        var opcoes = usuario.Cargo.Nome switch
        {
            CargosPadrao.CargoAlunos =>
                OperacoesContexto.AcessoAlunos,
            CargosPadrao.CargoGestores
                or CargosPadrao.CargoAdministradores =>
                OperacoesContexto.AcessoGestoresAdministradores,
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
