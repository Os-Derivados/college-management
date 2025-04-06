using college_management.Dados.Contexto;
using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioMaterias : Repositorio<Materia>
{
	public RepositorioMaterias(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Materia modelo)
	{
		var obterPorNome = ObterPorNome(modelo.Nome);
		var obterPorId = ObterPorId(modelo.Id);

		return obterPorNome.Status is StatusResposta.Sucesso
		       || obterPorId.Status is StatusResposta.Sucesso;
	}
}
