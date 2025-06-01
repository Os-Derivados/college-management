using college_management.Dados.Modelos;

namespace college_management.Contextos.Interfaces;


public interface IContextoCursos : IContexto<Curso>
{
	public Task VerMatricula();
	public Task CriarMatricula();
	public Task VerGradeCurricular();
	public Task CriarGradeCurricular();
	public Task EditarGradeCurricular();
}
