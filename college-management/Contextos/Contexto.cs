using System.Text;
using college_management.Constantes;
using college_management.Dados.Modelos;

namespace college_management.Contextos;

public abstract class Contexto
{
    private readonly Type _tipoRecurso;

    protected Contexto(Type tipoRecurso)
    {
        _tipoRecurso = tipoRecurso;
    }
    
    public void ListarOpcoes() 
    {
        StringBuilder mensagem = new();

        var opcoes = _tipoRecurso.Name switch
        {
            nameof(Usuario) => OperacoesRecurso.OperacoesUsuarios,
            nameof(Curso) => OperacoesRecurso.OperacoesCursos,
            nameof(Materia) => OperacoesRecurso.OperacoesMaterias,
            nameof(Cargo) => OperacoesRecurso.OperacoesCargos,
            _ => throw new InvalidOperationException("Não há contexto definido para este tipo")
        };

        mensagem.AppendLine(
            $"Bem vindo ao recuso de {_tipoRecurso.Name}.\n"
            + $"Selecione uma das opções abaixo.\n");
        
        for (var i = 0; i < opcoes.Length; i++)
        {
            mensagem.AppendLine($"{i + 1}. {opcoes[i]}");
        }
        
        Console.WriteLine(mensagem.ToString());
    }
}
