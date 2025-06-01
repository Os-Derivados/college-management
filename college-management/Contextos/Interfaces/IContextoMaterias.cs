using college_management.Dados.Modelos;

namespace college_management.Contextos.Interfaces;

public interface IContextoMaterias : IContexto<Materia>
{
	public Task VerNotas();
	public Task LancarNota();
	public Task EditarNota();
	public Task VerCorpoDocente();
	public Task EditarCorpoDocente();
}
