using college_management.Dados.Repositorios.Interfaces;
using college_management.Modelos;

namespace college_management.Dados.Repositorios;

public class RepositorioUsuarios : Repositorio<Usuario>, IRepositorioUsuarios
{
    public Usuario ObterPorLogin(string login)
    {
        return _baseDeDados.FirstOrDefault(u => u.Login == login);
    }
}