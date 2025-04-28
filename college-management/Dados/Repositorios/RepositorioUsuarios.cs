using college_management.Constantes;
using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace college_management.Dados.Repositorios;


public class RepositorioUsuarios : Repositorio<Usuario>, IRepositorioUsuarios
{
	public RepositorioUsuarios(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public RespostaRecurso<Usuario> ObterPorLogin(string login)
	{
		var usuario = Dados.AsNoTracking()
		                   .FirstOrDefault(aluno => aluno.Login == login);

		return usuario is not null
			? new RespostaRecurso<Usuario>(usuario, StatusResposta.Sucesso)
			: new RespostaRecurso<Usuario>(null,
			                               StatusResposta.ErroNaoEncontrado);
	}

	public RespostaRecurso<Aluno> ObterAluno(uint id)
	{
		var existe = BancoDeDados.Matriculas.Any(
			matricula => matricula.AlunoId == id);

		if (!existe)
		{
			return new RespostaRecurso<Aluno>(null,
			                                  StatusResposta.ErroNaoEncontrado);
		}

		var usuario = Dados.AsNoTracking()
		                   .FirstOrDefault(usuario => usuario.Id == id);

		return new RespostaRecurso<Aluno>((Aluno)usuario!,
		                                  StatusResposta.Sucesso);
	}

	public RespostaRecurso<IEnumerable<Aluno>> ObterPorTurma(uint turmaId)
	{
		var existe = BancoDeDados.Turmas.AsNoTracking()
		                         .Any(turma => turma.Id == turmaId);

		if (!existe)
		{
			return new RespostaRecurso<IEnumerable<Aluno>>(null,
				StatusResposta.ErroNaoEncontrado);
		}

		var idAlunos = BancoDeDados.TurmaAluno.AsNoTracking()
		                           .Where(turmaAluno =>
			                                  turmaAluno.TurmaId == turmaId)
		                           .Select(turma => turma.AlunoId);

		var alunos = Dados.AsNoTracking()
		                  .Where(usuario => idAlunos.Contains(usuario.Id))
		                  .Select(usuario => (Aluno)usuario);

		return new RespostaRecurso<IEnumerable<Aluno>>(alunos,
			StatusResposta.Sucesso);
	}

	public override bool Existe(Usuario modelo)
	{
		var obterPorLogin = ObterPorLogin(modelo.Login!);
		var obterPorId    = ObterPorId(modelo.Id);

		return obterPorLogin.Status is StatusResposta.Sucesso
		       && obterPorId.Status is StatusResposta.Sucesso;
	}
}
