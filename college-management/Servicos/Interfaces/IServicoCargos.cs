using college_management.Dados;
using college_management.Dados.Modelos;


namespace college_management.Servicos.Interfaces;


public interface IServicoCargos
{
	public Cargo? BuscarPorNome(string nomeCargo);
}
