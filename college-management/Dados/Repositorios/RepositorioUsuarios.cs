using college_management.Dados.Repositorios.Interfaces;
using college_management.Modelos;

namespace college_management.Dados.Repositorios;

public class RepositorioUsuarios : Repositorio<Usuario>, IRepositorioUsuarios
{
    public async Task<Usuario> ObterPorLogin(string login)
    {
        if (_baseDeDados.Count is 0)
            _baseDeDados = await _servicoDeArquivos.CarregarAssincrono();

        return _baseDeDados.FirstOrDefault(u => u.Login == login);
    }
}