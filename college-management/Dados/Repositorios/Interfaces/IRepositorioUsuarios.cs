using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioUsuarios : IRepositorio<Usuario>
{
	public RespostaRecurso<Usuario> ObterPorLogin(string login);
}
