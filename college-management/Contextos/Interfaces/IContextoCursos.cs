using college_management.Dados.Modelos;

namespace college_management.Contextos.Interfaces;


public interface IContextoCursos : IContexto<Curso>
{
	public Task VerMatriculas();
	public Task PesquisarMatricula();
	public Task CriarMatricula();
	public Task EditarMatricula();
	public Task VerGradeCurricular();
	public Task CriarGradeCurricular();
	public Task EditarGradeCurricular();
}
