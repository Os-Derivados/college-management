using college_management.Dados.Modelos;

namespace college_management.Contexto.Interfaces;

public interface IContextoFuncionarios
{
    public void AcessarCursos(Funcionario funcionario);
    public void AcessarCargos(Funcionario funcionario);
    public void AcessarMaterias(Funcionario funcionario);
    public void AcessarUsuarios(Funcionario funcionario);
}
