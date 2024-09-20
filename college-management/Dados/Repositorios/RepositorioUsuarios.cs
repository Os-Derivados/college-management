using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioUsuarios : Repositorio<Usuario>,
                                   IRepositorioUsuarios
{
	public Usuario ObterPorLogin(string login)
	{
		return BaseDeDados.FirstOrDefault(u => u.Login == login);
	}

	public override bool Existe(Usuario modelo)
	{
		Usuario loginExistente = ObterPorLogin(modelo.Login),
		        idExistente    = ObterPorId(modelo.Id);

		return loginExistente is not null
		       || idExistente is not null;
	}
}
