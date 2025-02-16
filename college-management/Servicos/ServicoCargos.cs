using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoCargos : ServicoModelos<Cargo>
{
	public ServicoCargos(IRepositorio<Cargo> repositorio) : base(repositorio)
	{
	}
}
