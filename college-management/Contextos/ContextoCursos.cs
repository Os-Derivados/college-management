using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCursos : Contexto<Curso>,
                              IContextoCursos
{
	public ContextoCursos(BaseDeDados baseDeDados,
	                      Usuario usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto)
	{
	}

	public void VerGradeHoraria()
	{
		// TODO: Desenvolver um algoritmo para visualização de grade horária
		// [REQUISITO]: A visualização deve ser em formato de relatório
		// 
		// Ex.: Ver Grade Horária do Curso "Ciência da Computação", 
		// 2o semestre
		//
		// Curso: Ciência da Computação
		// Semestre Atual: 2
		//
		// Grade Horária:
		//
		// | DIA           | MATÉRIA            | SALA | HORÁRIO |
		// | Segunda-Feira | Álgebra Linear     | 03   | 19:15   | 
		// | Terça-Feira   | Sistemas Digitais  | 03   | 19:15   |
		// ...

		if (CargoContexto.TemPermissao(PermissoesAcesso
			                               .AcessoEscrita))
			// [REQUISITO]: A visualização do gestor deve solicitar a busca
			// de um Curso em específico na base de dados
			//
			// Ex.: Ver Grade Horária do Curso "Ciência da Computação" 
			//
			// [Ver Grade Horária]
			// Selecione um abaixo campo para realizar a busca
			//
			// [1] Nome
			// [2] Id
			// 
			// Sua opção: 1 <- Opção que o usuário escolheu 
			// ...
			//
			// Digite o nome do Curso: "Ciência da Computação"
			// ...
			//
			// [REQUISITO]: O gestor deve selecionar qual semestre do curso
			// este deseja visualizar a grade horária
			//
			// Ex.: O curso "Ciência da Computação" tem 8 semestres
			//
			// Selecione um semestre a ser visualizado (somente números).
			//
			// [1, 2, 3, 4, 5, 6, 7, 8]: 
			throw new NotImplementedException();

		// [REQUISITO]: A visualização do Aluno deve permitir somente
		// a visualização da grade horária do curso no qual ele
		// atualmente esteja vinculado
		throw new NotImplementedException();
	}

	public void VerGradeCurricular()
	{
		string ObterLayout(Curso curso)
		{
			return $"Curso: {curso.Nome}\n" +
			       $"Ano: {DateTime.Today.Year}\n\n" +
			       $"{string.Join('\n', curso.GradeCurricular.Select(i => i.Nome))}";
		}

		InputView inputRelatorio = new("Ver Grade Curricular");

		Curso? curso;

		if (TemAcessoRestrito)
		{
			curso = PesquisarCurso();

			if (curso is null) return;

			inputRelatorio.LerEntrada("Sair", ObterLayout(curso));

			return;
		}

		if (UsuarioContexto is not Aluno aluno)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O usuário atual não é um aluno.");

			return;
		}

		var verCursos = BaseDeDados.Cursos.ObterTodos();

		curso = verCursos
		        .Modelo!
		        .FirstOrDefault(
			        i => i.MatriculasIds?.Contains(aluno.MatriculaId)
			             ?? false);

		if (verCursos.Modelo!.Count is 0 || curso is null)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O aluno não está matriculado em nenhum curso.");

			return;
		}

		var layout = ObterLayout(curso);

		inputRelatorio.LerEntrada("Sair", layout);
	}

	public override async Task Cadastrar()
	{
		var view = new CadastroCursoView();
		if (view.ObterDados().ToString().ToLower() != "s")
			return;

		List<Materia> materias = new();
		foreach (var moniker in view.GradeCurricular)
		{
			Materia? materia = null;
			
			var respostaNome = BaseDeDados.Materias.ObterPorNome(moniker);
			var respostaId = BaseDeDados.Materias.ObterPorId(moniker);

			materia = respostaNome.Status is StatusResposta.Sucesso
					  ? respostaNome.Modelo
					  : (respostaId.Status is StatusResposta.Sucesso
						 ? respostaId.Modelo
						 : null);

			if (materia is null)
			{
				View.Aviso($"Matéria com o identificador \"{materia}\" não encontrada.");
				break;
			}
			else
				materias.Add(respostaNome.Modelo!);
		}
		
		var curso = new Curso(view.Nome, materias.ToArray());
		var respostaAdicionar = await BaseDeDados.Cursos.Adicionar(curso);
		View.Aviso(respostaAdicionar.Status is StatusResposta.Sucesso
			? "Curso cadastrado com sucesso!"
			: $"Não foi possível cadastrar curso. ({respostaAdicionar.Status.ToString()})");
	}

	public override async Task Editar()
	{
		var curso = PesquisarCurso();

		if (curso is null)
			return;

		curso = new EditarCursoView(curso).Editar();
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
		relatorioView.ConstruirLayout();
		relatorioView.Exibir();
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
		
		var busca = new BuscaModeloView<Curso>("Buscar Curso", ["Nome"]).Buscar();
		var resposta = busca.Key switch
		{
			1 => BaseDeDados.Cursos.ObterPorNome(busca.Value),
			2 => BaseDeDados.Cursos.ObterPorId(busca.Value),
			_ => null
		};
		
		return resposta.Status is StatusResposta.Sucesso ? resposta.Modelo : MostrarAviso();
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