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
        var opcaoContexto = EscolherContexto(usuario);

        if (opcaoContexto is "") return;

        var contexto = ObterContexto(opcaoContexto);
        
        AcessarContexto(contexto, opcaoContexto, baseDeDados, usuario);
    }
    
    private static object ObterContexto(string opcaoContexto)
    {
        return opcaoContexto switch
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
    }

    private static void AcessarContexto(dynamic contexto,
                                        string opcaoContexto,
                                        BaseDeDados baseDeDados,
                                        Usuario usuario)
    {
        var estadoAtual = EstadoDoApp.Recurso;
        
        do
        {
            Console.Clear();
            
            contexto.ListarOpcoes();

            var opcaoEscolhida = Console.ReadKey();

            if (opcaoEscolhida.Key is not ConsoleKey.D0)
            {
                var recursoEscolhido =
                    ConverterParaMetodo(opcaoContexto,
                                        opcaoEscolhida);

                Console.Clear();

                contexto.AcessarRecurso(recursoEscolhido,
                                        baseDeDados,
                                        usuario);
            }
            else
            {
                estadoAtual = EstadoDoApp.Sair;
            }
        } while (estadoAtual is EstadoDoApp.Recurso);
    }


    private static string ConverterParaMetodo(string opcao,
                                              ConsoleKeyInfo indice)
    {
        var recursosDisponiveis = opcao switch
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

        _ = int.TryParse(indice.KeyChar.ToString(), out var i);

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
            var contextos = ListarContextos(usuario);

            var opcaoEscolhida = Console.ReadKey();
            var opcaoValida = int.TryParse(opcaoEscolhida.KeyChar
                                                         .ToString(),
                                           out var opcaoUsuario);

            if (!opcaoValida) continue;

            if (opcaoUsuario is 0) break;

            _ = contextos.TryGetValue(opcaoUsuario,
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

        Console.Clear();

        mensagem.AppendLine(
            "Bem-vindo(a). Selecione um dos contextos abaixo.\n");

        var opcoes = usuario.Cargo.Nome switch
        {
            CargosPadrao.CargoAlunos => OperacoesContexto.AcessoAlunos,
            CargosPadrao.CargoGestores
                or CargosPadrao.CargoAdministradores =>
                OperacoesContexto.AcessoGestoresAdministradores,
            _ => throw new InvalidOperationException(
                     "O usuário não possui um cargo validado")
        };

        for (var i = 0; i < opcoes.Length; i++)
        {
            mensagem.AppendLine($"[{i + 1}] {opcoes[i]}");
            dicionarioOpcoes.Add(i + 1, opcoes[i]);
        }

        mensagem.Append("\nSua opção (somente números): ");

        Console.Write(mensagem);

        return dicionarioOpcoes;
    }
}
