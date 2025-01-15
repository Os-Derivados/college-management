using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioCursos : Repositorio<Curso>
{
	public override bool Existe(Curso modelo)
	{
		var nomeExistente = ObterPorNome(modelo.Nome);

		var idExistente = ObterPorId(modelo.Id);

		return nomeExistente.Status is StatusResposta.Sucesso
		       || idExistente.Status is StatusResposta.Sucesso;
	}
}
