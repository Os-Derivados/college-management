using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos.Interfaces;

public interface IContextoUsuarios
{
    public void VerMatricula();

    public void VerBoletim();

    public void VerFinanceiro();
}
