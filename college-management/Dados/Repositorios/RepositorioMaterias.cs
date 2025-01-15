using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioMaterias : Repositorio<Materia>
{
	public override bool Existe(Materia modelo)
	{
		var nomeExistente = ObterPorNome(modelo.Nome);

		var idExistente = ObterPorId(modelo.Id);

		return nomeExistente.Status is StatusResposta.Sucesso
		       || idExistente.Status is StatusResposta.Sucesso;
	}
}
