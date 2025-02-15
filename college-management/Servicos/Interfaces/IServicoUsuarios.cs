using college_management.Dados;
using college_management.Dados.Modelos;


namespace college_management.Servicos.Interfaces;


public interface IServicoUsuarios
{
	public RespostaRecurso<Usuario> BuscarUsuario(int modoBusca,
	                                              string chaveBusca);
}
