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
			? new RespostaRecurso<Usuario>(
				null, StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<Usuario>(usuario, StatusResposta.Sucesso);
	}

	public override bool Existe(Usuario modelo)
	{
		var loginExistente = ObterPorLogin(modelo.Login);
		var idExistente    = ObterPorId(modelo.Id);

		return loginExistente is not null
		       || idExistente.Status is StatusResposta.Sucesso;
	}
}
