using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Funcionalidades;

public sealed class Contexto
{
    private readonly BaseDeDados _baseDeDados;
    private readonly Usuario _usuario;

    public Contexto(BaseDeDados baseDeDados, Usuario usuario)
    {
        _baseDeDados = baseDeDados;
        _usuario = usuario;
    }

    public void AcessarRecurso(string recurso)
    {
        switch (recurso)
        {
            case OperacoesDeContexto.AcessarGradeHoraria:
                AcessarGradeHoraria();
                break;

            case OperacoesDeContexto.AcessarGradeCurricular:
                AcessarGradeCurricular();
                break;

            case OperacoesDeContexto.AcessarNotas:
                AcessarNotas();
                break;

            case OperacoesDeContexto.AcessarFinanceiro:
                AcessarFinanceiro();
                break;

            case OperacoesDeContexto.AcessarMatricula:
                AcessarMatricula();
                break;

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
        }
    }

    private void AcessarGradeHoraria()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele
        // Se o usuário for um Funcionário:
        // Peça para selecionar um curso em específico: ID, Nome
        throw new NotImplementedException();
    }

    private void AcessarGradeCurricular()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um curso específico: ID, Nome
        throw new NotImplementedException();
    }

    private void AcessarNotas()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Aluno em específico:  Matricula, Nome, Login, Id...

        throw new NotImplementedException();
    }

    private void AcessarFinanceiro()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Aluno em específico: Matricula, Nome, Login, Id...

        throw new NotImplementedException();
    }

    private void AcessarMatricula()
    {
        // Se o usuário for um Aluno:
        // Mostre somente informações dele

        // Se o usuário for um Funcionário:
        // Peça para selecionar um Aluno em específico: Matricula, Nome, Login, Id...

        throw new NotImplementedException();
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
