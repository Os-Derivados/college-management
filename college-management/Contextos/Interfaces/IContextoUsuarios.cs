using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos.Interfaces;

public interface IContextoUsuarios
{
    public void VerMatricula(Repositorio<Usuario> repositorio,
                             Usuario usuario);

    public void VerBoletim(Repositorio<Usuario> repositorio,
                           Usuario usuario);

    public void VerFinanceiro(Repositorio<Usuario> repositorio,
                              Usuario usuario);
}
