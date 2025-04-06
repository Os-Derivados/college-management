using college_management.Dados.Contexto;
using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioCursos : Repositorio<Curso>
{
	public RepositorioCursos(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Curso modelo)
	{
		var obterPorNome = ObterPorNome(modelo.Nome);
		var obterPorId = ObterPorId(modelo.Id);

		return obterPorNome.Status is StatusResposta.Sucesso
		       || obterPorId.Status is StatusResposta.Sucesso;
	}
}
