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
    
    private void AcessarGradeHoraria() { throw new NotImplementedException(); }

    private void AcessarGradeCurricular() { throw new NotImplementedException(); }

    private void AcessarNotas() { throw new NotImplementedException(); }

    private void AcessarFinanceiro() { throw new NotImplementedException(); }

    private void AcessarMatricula() { throw new NotImplementedException(); }

    private void AcessarCursos() { throw new NotImplementedException(); }

    private void AcessarCargos() { throw new NotImplementedException(); }

    private void AcessarMaterias() { throw new NotImplementedException(); }

    private void AcessarUsuarios() { throw new NotImplementedException(); }
}
