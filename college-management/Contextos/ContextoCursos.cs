using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCursos : Contexto<Curso>, IContextoCursos
{
	public ContextoCursos(BaseDeDados baseDeDados, Usuario usuarioContexto) :
		base(baseDeDados, usuarioContexto)
	{
	}

	public async Task VerGradeCurricular()
	{
		string ObterLayout(Curso curso)
		{
			return $"Curso: {curso.Nome}\n"
			       + $"Ano: {DateTime.Today.Year}\n\n"
			       + $"{string.Join('\n', curso.Materias.Select(i => i.Nome))}";
		}

		InputView inputRelatorio = new("Ver Grade Curricular");

		if (TemAcessoRestrito)
		{
			var pesquisarCurso = PesquisarCurso();

			if (pesquisarCurso is null) return;

			inputRelatorio.LerEntrada("Sair", ObterLayout(pesquisarCurso));

			return;
		}

		if (UsuarioContexto is not Aluno aluno)
		{
			inputRelatorio.LerEntrada("Erro",
			                          "O usuário atual não é um aluno.");

			return;
		}

		var cursoAluno
			= await BaseDeDados.Cursos.Buscar(c => c.Alunos.Contains(aluno));

		if (cursoAluno.Status is not StatusResposta.Sucesso)
		{
			inputRelatorio.LerEntrada("Erro",
			                          "O aluno não está matriculado em nenhum curso.");

			return;
		}

		if (cursoAluno.Modelo != null)
		{
			var layout = ObterLayout(cursoAluno.Modelo.First());

			inputRelatorio.LerEntrada("Sair", layout);
		}
	}

	public override async Task Cadastrar()
	{
		var view = new CadastroCursoView();
		if (view.ObterDados().ToString().ToLower() != "s")
			return;

		if (string.IsNullOrEmpty(view.Nome))
		{
			View.Aviso("Nome vazio. Tente novamente.");
			return;
		}

		List<Materia> materias = new();

		foreach (var moniker in view.GradeCurricular)
		{
			Materia? materia = null;

			var respostaNome = BaseDeDados.Materias.ObterPorNome(moniker);
			var respostaId   = BaseDeDados.Materias.ObterPorId(moniker);

			materia = respostaNome.Status is StatusResposta.Sucesso
				? respostaNome.Modelo
				: (respostaId.Status is StatusResposta.Sucesso
					? respostaId.Modelo
					: null);

			if (materia is null)
			{
				View.Aviso(
					$"Matéria com o identificador \"{moniker}\" não encontrada. Tente novamente.");
				goto FimDeLogica;
			}
			else
				materias.Add(materia);
		}

		var curso             = new Curso(view.Nome, materias.ToArray());
		var respostaAdicionar = await BaseDeDados.Cursos.Adicionar(curso);
		View.Aviso(respostaAdicionar.Status is StatusResposta.Sucesso
			           ? "Curso cadastrado com sucesso!"
			           : $"Não foi possível cadastrar curso. ({respostaAdicionar.Status.ToString()})");
		FimDeLogica: ; // É feio, mas é prático.
	}

	public override async Task Editar()
	{
		var curso = PesquisarCurso();

		if (curso is null)
			return;

		curso = new EditarCursoView(curso, BaseDeDados.Materias).Editar();
	}

	public override async Task Excluir()
	{
		var curso = PesquisarCurso();

		if (curso is null) return;

		DetalhesView detalhesCurso = new(string.Empty, ObterDetalhes(curso));
		detalhesCurso.ConstruirLayout();

		ConfirmaView confirmacao = new("Excluir Curso");

		if (confirmacao.Confirmar(detalhesCurso.Layout.ToString())
		               .ToString()
		               .ToLower() is not "s")
		{
			View.Aviso($"Excluir {nameof(Curso)}: Operação cancelada.");
			return;
		}

		var removerCurso = await BaseDeDados.Cursos.Remover(curso.Id);
		var mensagemResultado = removerCurso.Status is StatusResposta.Sucesso
			? "Curso removido com sucesso."
			: "Erro ao remover curso.";

		View.Aviso(mensagemResultado);
	}

	public override void Visualizar()
	{
		var verCursos = BaseDeDados.Cursos.ObterTodos();

		InputView inputRelatorio = new("Visualizar Cursos");

		if (verCursos.Modelo!.Count is 0)
		{
			View.Aviso("Nenhum curso cadastrado.");

			return;
		}

		RelatorioView<Curso> relatorioView
			= new(inputRelatorio.Titulo, verCursos.Modelo);
		PaginaView paginaView = new(relatorioView);
		paginaView.ConstruirLayout();
		paginaView.LerEntrada(true);
	}

	public override void VerDetalhes()
	{
		var curso = PesquisarCurso();

		if (curso is null)
		{
			View.Aviso("Curso não encontrado.");

			return;
		}

		DetalhesView detalhesCurso
			= new("Curso Encontrado", ObterDetalhes(curso));
		detalhesCurso.ConstruirLayout();
		detalhesCurso.Exibir();
	}

	private Curso? PesquisarCurso()
	{
		Curso? MostrarAviso()
		{
			View.Aviso("Curso não encontrado.");
			return null;
		}

		var busca
			= new BuscaModeloView<Curso>("Buscar Curso", ["Nome"]).Buscar();
		var resposta = busca.Key switch
		{
			1 => BaseDeDados.Cursos.ObterPorNome(busca.Value),
			2 => BaseDeDados.Cursos.ObterPorId(busca.Value),
			_ => null
		};

		return resposta.Status is StatusResposta.Sucesso
			? resposta.Modelo
			: MostrarAviso();
	}

	private Dictionary<string, string> ObterDetalhes(Curso curso)
	{
		var detalhes = UtilitarioTipos.ObterPropriedades(curso, ["Nome"]);

		detalhes.Add("MateriasId",
		             $"{string.Join(", ", curso.MatriculasIds ?? [])}");
		detalhes.Add("GradeCurricular",
		             $"{string.Join(", ", curso.GradeCurricular.Select(i => i.Nome))}");
		detalhes.Add("CargaHoraria", $"{curso.ObterCargaHoraria()}h");

		return detalhes;
	}
}
