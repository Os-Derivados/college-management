using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoCargos : IServicoCargos
{
	public ServicoCargos(IRepositorio<Cargo> repositorio)
	{
		_repositorio = repositorio;
	}

	private readonly IRepositorio<Cargo> _repositorio;

	public Cargo? BuscarPorNome(string nomeCargo)
	{
		var obterPorNome = _repositorio.ObterPorNome(nomeCargo);

		if (obterPorNome.Status is not StatusResposta.ErroNaoEncontrado)
			return obterPorNome.Modelo;

		View.Aviso("O Cargo inserido não foi encontrado na base de dados.");

		return null;
	}
}
