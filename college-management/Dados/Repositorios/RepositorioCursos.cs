using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioCursos : Repositorio<Curso>
{
	public override bool Existe(Curso modelo)
	{
		var nomeExistente = ObterTodos()
			.FirstOrDefault(c => c.Nome == modelo.Nome);

		var idExistente = ObterPorId(modelo.Id);

		return nomeExistente is not null
		       || idExistente is not null;
	}
}
