using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioCursoMateria : Repositorio<CursoMateria>
{
	public override bool Existe(CursoMateria modelo)
	{
		var obterPorId = ObterPorId(modelo.Id);

		return obterPorId.Status is StatusResposta.Sucesso;
	}
}
