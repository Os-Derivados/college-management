using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios.Interfaces;


public interface IRepositorioUsuarios
{
	public RespostaRecurso<Usuario> ObterPorLogin(string login);
}
