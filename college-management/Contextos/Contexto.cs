using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public abstract class Contexto<T> : IContexto<T> where T : Modelo
{
    public void ListarOpcoes()
    {
        StringBuilder mensagem = new();

        var opcoes = typeof(T).Name switch
        {
            nameof(Usuario) => OperacoesRecurso.OperacoesUsuarios,
            nameof(Curso)   => OperacoesRecurso.OperacoesCursos,
            nameof(Materia) => OperacoesRecurso.OperacoesMaterias,
            nameof(Cargo)   => OperacoesRecurso.OperacoesCargos,
            _ => throw new InvalidOperationException(
                     "Não há contexto definido para este tipo")
        };

        mensagem.AppendLine(
            $"Bem vindo ao recuso de {typeof(T).Name}.\n"
            + $"Selecione uma das opções abaixo.\n");

        for (var i = 0; i < opcoes.Length; i++)
            mensagem.AppendLine($"{i + 1}. {opcoes[i]}");

        Console.WriteLine(mensagem.ToString());
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

    public async Task Cadastrar(Repositorio<T> repositorio, Usuario usuario) { throw new NotImplementedException(); }

    public async Task Editar(Repositorio<T> repositorio, Usuario usuario) { throw new NotImplementedException(); }

    public async Task Excluir(Repositorio<T> repositorio, Usuario usuario) { throw new NotImplementedException(); }

    public void Visualizar(Repositorio<T> repositorio, Usuario usuario) { throw new NotImplementedException(); }
}
