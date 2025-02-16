using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Servicos;


public class ServicoMaterias : ServicoModelos<Materia>
{
	public ServicoMaterias(IRepositorio<Materia> repositorio) : base(repositorio)
	{
	}
}
