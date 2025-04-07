using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioUsuarios : Repositorio<Usuario>, IRepositorioUsuarios
{
	public RepositorioUsuarios(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public RespostaRecurso<Usuario> ObterPorLogin(string login)
	{
		var usuario = Dados.FirstOrDefault(u => u.Login == login);

		return usuario is null
			? new RespostaRecurso<Usuario>(null,
			                               StatusResposta.ErroNaoEncontrado)
			: new RespostaRecurso<Usuario>(usuario, StatusResposta.Sucesso);
	}

	public override bool Existe(Usuario modelo)
	{
		var obterPorLogin = ObterPorLogin(modelo.Login!);
		var obterPorId    = ObterPorId(modelo.Id);

		return obterPorLogin.Status is StatusResposta.Sucesso ||
		       obterPorId.Status is StatusResposta.Sucesso;
	}
}
