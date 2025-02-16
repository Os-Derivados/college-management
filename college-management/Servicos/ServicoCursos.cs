using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Servicos;


public class ServicoCursos : ServicoModelos<Curso>
{
	public ServicoCursos(IRepositorio<Curso> repositorio) : base(repositorio)
	{
	}
}
