using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Views;

namespace college_management.Contextos;

public abstract class Contexto<T> : IContexto<T> where T : Modelo
{
    public void ListarOpcoes()
    {
        var opcoes = ObterOpcoes();

        MenuView menuRecursos = new("Menu Recursos",
                                    $"Bem vindo ao recuso de {typeof(T).Name}.",
                                    opcoes);

        menuRecursos.ConstruirLayout();
        menuRecursos.Exibir();
    }

    private string[] ObterOpcoes()
    {
        return typeof(T).Name switch
        {
            nameof(Usuario) => OperacoesRecurso.OperacoesUsuarios,
            nameof(Curso)   => OperacoesRecurso.OperacoesCursos,
            nameof(Materia) => OperacoesRecurso.OperacoesMaterias,
            nameof(Cargo)   => OperacoesRecurso.OperacoesCargos,
            _ => throw new InvalidOperationException(
                     "Não há contexto definido para este tipo")
        };
    }

    public void AcessarRecurso(string nomeRecurso,
                               BaseDeDados baseDeDados,
                               Usuario usuario)
    {
        var interfacesContexto = GetType().GetInterfaces();

        var recurso =
            interfacesContexto.Select(t => t.GetMethod(nomeRecurso))
                              .FirstOrDefault(t => t is not null);

        if (recurso is null)
            throw new InvalidOperationException("Recurso inexistente");

        dynamic repositorio = typeof(T).Name switch
        {
            nameof(Usuario) => baseDeDados.usuarios,
            nameof(Cargo)   => baseDeDados.cargos,
            nameof(Materia) => baseDeDados.materias,
            nameof(Curso)   => baseDeDados.cursos,
            _ => throw new InvalidOperationException(
                     "Repositorio inexistente")
        };

        recurso.Invoke(this, [repositorio, usuario]);
    }

    public abstract Task Cadastrar(Repositorio<T> repositorio,
                                   Usuario usuario);

    public abstract Task Editar(Repositorio<T> repositorio,
                                Usuario usuario);

    public abstract Task Excluir(Repositorio<T> repositorio,
                                 Usuario usuario);

    public abstract void Visualizar(Repositorio<T> repositorio,
                                    Usuario usuario);
}
