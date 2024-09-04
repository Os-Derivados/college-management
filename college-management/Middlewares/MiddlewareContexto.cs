using System.Text;
using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Contextos;
using college_management.Views;

namespace college_management.Middlewares;

public static class MiddlewareContexto
{
    public static void Inicializar(BaseDeDados baseDeDados,
                                   Usuario usuario)
    {
        var opcaoContexto = EscolherContexto(usuario);

        if (opcaoContexto is "") return;

        var contexto = ObterContexto(opcaoContexto);

        AcessarContexto(contexto,
                        opcaoContexto,
                        baseDeDados,
                        usuario);
    }

    private static object ObterContexto(string opcaoContexto)
    {
        return opcaoContexto switch
        {
            AcessosContexto.AcessoContextoCursos =>
                new ContextoCursos(),
            AcessosContexto.AcessoContextoMaterias =>
                new ContextoMaterias(),
            AcessosContexto.AcessoContextoCargos =>
                new ContextoCargos(),
            AcessosContexto.AcessoContextoUsuarios =>
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
            AcessosContexto.AcessoContextoUsuarios =>
                OperacoesRecurso.OperacoesUsuarios,
            AcessosContexto.AcessoContextoCursos =>
                OperacoesRecurso.OperacoesCursos,
            AcessosContexto.AcessoContextoMaterias =>
                OperacoesRecurso.OperacoesMaterias,
            AcessosContexto.AcessoContextoCargos =>
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
            var opcoesContextos = ObterOpcoesContextos(usuario);

            MenuView menuContextos = new("Menu Contextos",
                                         "Bem-vindo(a).",
                                         opcoesContextos);

            menuContextos.ConstruirLayout();
            menuContextos.Exibir();

            var opcaoEscolhida = Console.ReadKey();
            var opcaoValida = int.TryParse(opcaoEscolhida.KeyChar
                                                         .ToString(),
                                           out var opcaoUsuario);

            if (!opcaoValida) continue;

            if (opcaoUsuario is 0) break;

            contextoEscolhido = opcoesContextos[opcaoUsuario];

            estadoAtual = EstadoDoApp.Recurso;
        } while (estadoAtual is EstadoDoApp.Contexto);

        return contextoEscolhido;
    }

    private static string[] ObterOpcoesContextos(Usuario usuario)
    {
        return usuario.Cargo.Nome switch
        {
            CargosPadrao.CargoAlunos => AcessosContexto.AcessoAlunos,
            CargosPadrao.CargoGestores
                or CargosPadrao.CargoAdministradores =>
                AcessosContexto.AcessoGestoresAdministradores,
            _ => throw new InvalidOperationException(
                     "O usuário não possui um cargo validado")
        };
    }
}
