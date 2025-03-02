using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioMatriculas : Repositorio<Matricula>,
                                     IRepositorioMatriculas
{
	public override bool Existe(Matricula modelo)
	{
		return ObterPorId(modelo.Id).Status is StatusResposta.Sucesso;
	}

	public RespostaRecurso<IEnumerable<Matricula>> ObterPorAluno(Guid alunoId)
	{
		var matriculasComAluno
			= BaseDeDados.Where(m => m.AlunoId == alunoId).ToArray();

		if (matriculasComAluno.Length == 0)
		{
			return new RespostaRecurso<IEnumerable<Matricula>>(
				null, StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<IEnumerable<Matricula>>(matriculasComAluno,
		                                                   StatusResposta.Sucesso);
	}


	public RespostaRecurso<IEnumerable<Matricula>> ObterPorCurso(Guid cursoId)
	{
		var matriculasComCurso
			= BaseDeDados.Where(m => m.CursoId == cursoId).ToArray();

		if (matriculasComCurso.Length == 0)
		{
			return new RespostaRecurso<IEnumerable<Matricula>>(
				null, StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<IEnumerable<Matricula>>(matriculasComCurso,
		                                                   StatusResposta.Sucesso);
	}
}
