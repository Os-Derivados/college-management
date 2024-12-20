using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioUsuarios : Repositorio<Usuario>,
                                   IRepositorioUsuarios
{
	public RespostaRecurso<Usuario> ObterPorLogin(string login)
	{
		var usuario = BaseDeDados.FirstOrDefault(u => u.Login == login);
		
		return usuario is null
		       ? new RespostaRecurso<Usuario>(usuario, StatusResposta.ErroNaoEncontrado)
		       : new RespostaRecurso<Usuario>(usuario, StatusResposta.Sucesso);
	}

	public override bool Existe(Usuario modelo)
	{
		RespostaRecurso<Usuario> loginExistente = ObterPorLogin(modelo.Login),
		        idExistente                     = ObterPorId(modelo.Id);

		return loginExistente.Status is StatusResposta.Sucesso
		       || idExistente.Status is StatusResposta.Sucesso;
	}
}
