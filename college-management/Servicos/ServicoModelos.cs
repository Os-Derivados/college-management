using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;


namespace college_management.Servicos;


public abstract class ServicoModelos<T> : IServicoModelos<T> where T : Modelo
{
	protected ServicoModelos(IRepositorio<T> repositorio)
	{
		Repositorio = repositorio;
	}

	protected readonly IRepositorio<T> Repositorio;

	public RespostaRecurso<T> Buscar(CriterioBusca modoBusca, string chaveBusca)
	{
		switch (modoBusca)
		{
			case CriterioBusca.Nome:
			{
				return Repositorio.ObterPorNome(chaveBusca);
			}
			case CriterioBusca.Id:
			{
				var tentativaCast = Guid.TryParse(chaveBusca, out var id);

				if (tentativaCast) return Repositorio.ObterPorId(id);

				return new RespostaRecurso<T>(
					null, StatusResposta.ErroInvalido);
			}
			case CriterioBusca.Login:
			{
				var repositorioUsuarios = (IRepositorioUsuarios)Repositorio;

				var obterPorLogin
					= repositorioUsuarios.ObterPorLogin(chaveBusca);

				if (obterPorLogin.Status is not StatusResposta.Sucesso)
				{
					return new RespostaRecurso<T>(null, obterPorLogin.Status);
				}

				Modelo modeloGenerico = obterPorLogin.Modelo!;

				return new RespostaRecurso<T>((T)modeloGenerico,
				                              obterPorLogin.Status);
			}
			default:
			{
				return new RespostaRecurso<T>(null, StatusResposta.ErroInterno);
			}
		}
	}
}

public enum CriterioBusca
{
	Nome,
	Id,
	Login
}
