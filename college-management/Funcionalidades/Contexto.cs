using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Funcionalidades;

public sealed class Contexto
{
    private readonly BaseDeDados _baseDeDados;
    private readonly Usuario _usuario;
    private readonly string _recurso;

    public Contexto(BaseDeDados baseDeDados, 
                    Usuario usuario, 
                    string recurso)
    {
        _baseDeDados = baseDeDados;
        _usuario = usuario;
        _recurso = recurso;
    }

    public void AcessarRecurso()
    {
        switch (_recurso)
        {
            case OperacoesDeContexto.AcessarCursos:
                AcessarCursos();
                break;

            case OperacoesDeContexto.AcessarCargos:
                AcessarCargos();
                break;

            case OperacoesDeContexto.AcessarMaterias:
                AcessarMaterias();
                break;

            case OperacoesDeContexto.AcessarUsuarios:
                AcessarUsuarios();
                break;
            
            default: 
                Console.WriteLine("Nenhum contexto foi selecionado. Saindo...");
                
                break;
        }
    }

    private void AcessarCursos()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Curso em específico: ID, Nome

        throw new NotImplementedException();
    }

    private void AcessarCargos()
    {
        // Se o usuário for um Aluno:
        // Recurse a solicitação

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Cargo em específico: ID, Nome

        throw new NotImplementedException();
    }

    private void AcessarMaterias()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele: Matérias do curso em que está matriculado

        // Se o usuário for um Funcionário:
        // Peça para selecionar uma Matéria em específico: ID, Nome

        throw new NotImplementedException();
    }

    private void AcessarUsuarios()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Usuário em específico: ID, Nome, Login

        throw new NotImplementedException();
    }
}
