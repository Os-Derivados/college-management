using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Repositorios;


public class RepositorioCursos : Repositorio<Curso>, IRepositorioCursos
{
	public RepositorioCursos(BancoDeDados bancoDeDados) : base(bancoDeDados) { }

	public override bool Existe(Curso modelo)
	{
		var obterPorNome = ObterPorNome(modelo.Nome);
		var obterPorId   = ObterPorId(modelo.Id);

		return obterPorNome.Status is StatusResposta.Sucesso
		       || obterPorId.Status is StatusResposta.Sucesso;
	}

	public RespostaRecurso<Curso> ObterComMaterias(
		uint? cursoId = null,
		string? nomeCurso = null)
	{
		if (cursoId is null && nomeCurso is null)
		{
			return new RespostaRecurso<Curso>(null,
			                                  StatusResposta.ErroInvalido);
		}

		var curso = Dados.Include(c => c.Materias)
		                 .FirstOrDefault(
			                 c => c.Id == cursoId || c.Nome == nomeCurso);

		if (curso is null)
		{
			return new RespostaRecurso<Curso>(null,
			                                  StatusResposta.ErroNaoEncontrado);
		}

		return new RespostaRecurso<Curso>(curso, StatusResposta.Sucesso);
	}
}
