using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;


namespace college_management.Dados.Repositorios;


public class RepositorioMaterias : Repositorio<Materia>, IRepositorioMaterias
{
	public RepositorioMaterias(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Materia modelo)
	{
		var obterPorNome = ObterPorNome(modelo.Nome);
		var obterPorId   = ObterPorId(modelo.Id);

		return obterPorNome.Status is StatusResposta.Sucesso
		       || obterPorId.Status is StatusResposta.Sucesso;
	}

	public RespostaRecurso<IEnumerable<Materia>> ObterGradeCurricular(
		uint cursoId)
	{
		var existe = BancoDeDados.Cursos.Any(c => c.Id == cursoId);

		if (!existe)
		{
			return new RespostaRecurso<IEnumerable<Materia>>(
				null,
				StatusResposta.ErroNaoEncontrado);
		}

		var idMaterias = BancoDeDados.GradeCurricular
		                             .Where(gc => gc.CursoId == cursoId)
		                             .Select(r => r.MateriaId);

		var materias = BancoDeDados.Materias
		                           .Where(m => idMaterias.Contains(m.Id))
		                           .ToList();

		return new RespostaRecurso<IEnumerable<Materia>>(
			materias,
			StatusResposta.Sucesso);
	}
}
