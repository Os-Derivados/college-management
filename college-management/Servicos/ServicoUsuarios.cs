using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;


namespace college_management.Servicos;


public class ServicoUsuarios : IServicoUsuarios
{
	public ServicoUsuarios(IRepositorioUsuarios repositorioUsuarios)
	{
		_repositorioUsuarios = repositorioUsuarios;
	}

	private readonly IRepositorioUsuarios _repositorioUsuarios;

	public RespostaRecurso<Usuario> BuscarUsuario(int modoBusca,
	                                              string chaveBusca)
	{
		if (modoBusca is 1)
		{
			return _repositorioUsuarios.ObterPorLogin(chaveBusca);
		}

		var tentativaCast = ulong.TryParse(chaveBusca, out var id);

		return tentativaCast
			? _repositorioUsuarios.ObterPorId(id)
			: new RespostaRecurso<Usuario>(null, StatusResposta.ErroInvalido);
	}
}
