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
		var loginExistente = ObterPorLogin(modelo.Login);
		var idExistente    = ObterPorId(modelo.Id);

		return loginExistente is not null 
		       || idExistente.Status is StatusResposta.Sucesso;
	}
}
