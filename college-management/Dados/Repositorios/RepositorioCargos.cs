using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioCargos : Repositorio<Cargo>
{
	public override bool Existe(Cargo modelo)
	{
		var obterPorNome = ObterPorNome(modelo.Nome);

		var obterPorId = ObterPorId(modelo.Id);

		return obterPorNome.Status is StatusResposta.Sucesso ||
		       obterPorId.Status is StatusResposta.Sucesso;
	}
}
