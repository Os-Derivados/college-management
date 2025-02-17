using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public abstract class ServicoModelos<T> : IServicoModelos<T> where T : Modelo
{
	protected ServicoModelos(IRepositorio<T> repositorio)
	{
		_repositorio = repositorio;
	}

	private readonly IRepositorio<T> _repositorio;

	public RespostaRecurso<T> Buscar(CriterioBusca modoBusca, string chaveBusca)
	{
		switch (modoBusca)
		{
			case CriterioBusca.Nome:
			{
				return _repositorio.ObterPorNome(chaveBusca);
			}
			case CriterioBusca.Id:
			{
				var tentativaCast = Guid.TryParse(chaveBusca, out var id);

				if (tentativaCast) return _repositorio.ObterPorId(id);

				return new RespostaRecurso<T>(
					null, StatusResposta.ErroInvalido);
			}
			case CriterioBusca.Login:
			{
				var repositorioUsuarios = (IRepositorioUsuarios)_repositorio;

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

	public bool ValidarResposta(RespostaRecurso<T> resposta, ModoOperacao modo)
	{
		if (modo is ModoOperacao.Leitura)
		{
			if (resposta.Status is StatusResposta.ErroNaoEncontrado)
			{
				View.Aviso(
					$"Falha ao buscar {typeof(T).Name}: registro não encontrado na base de dados.");

				return true;
			}

			if (resposta.Status is StatusResposta.ErroInvalido)
			{
				View.Aviso(
					$"Falha ao buscar {typeof(T).Name}: O valor da chave de busca é inválido.");

				return true;
			}

			return false;
		}

		if (resposta.Status is StatusResposta.ErroDuplicata)
		{
			View.Aviso(
				$"Falha ao gravar informações de {typeof(T).Name}: já existe um registro com a mesma chave primária.");

			return true;
		}

		if (resposta.Status is StatusResposta.ErroNaoEncontrado)
		{
			View.Aviso(
				$"Falha ao remover {typeof(T).Name}: registro não encontrado na base de dados.");

			return true;
		}

		return false;
	}

	public abstract T? Pesquisar();
}

public enum CriterioBusca
{
	Nome,
	Id,
	Login
}

public enum ModoOperacao
{
	Leitura,
	Escrita
}
