using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioCursoMateria : Repositorio<CursoMateria>,
                                       IRepositorioCursoMateria
{
	public override bool Existe(CursoMateria modelo)
	{
		var obterPorId = ObterPorId(modelo.Id);

		return obterPorId.Status is StatusResposta.Sucesso;
	}

	public RespostaRecurso<IEnumerable<CursoMateria>> ObterPorCurso(
		Guid cursoId)
	{
		var cursosMaterias = BaseDeDados.Where(cm => cm.CursoId == cursoId).ToArray();

		if (cursosMaterias.Length is 0)
		{
			return new RespostaRecurso<IEnumerable<CursoMateria>>(
				null, StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<IEnumerable<CursoMateria>>(
			cursosMaterias, StatusResposta.Sucesso);
	}

	public RespostaRecurso<IEnumerable<CursoMateria>> ObterPorMateria(
		Guid materiaId)
	{
		var cursosMaterias = BaseDeDados.Where(cm => cm.MateriaId == materiaId).ToArray();

		if (cursosMaterias.Length is 0)
		{
			return new RespostaRecurso<IEnumerable<CursoMateria>>(
				null, StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<IEnumerable<CursoMateria>>(
			cursosMaterias, StatusResposta.Sucesso);
	}
}
