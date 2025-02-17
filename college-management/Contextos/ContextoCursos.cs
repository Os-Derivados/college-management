using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Servicos;
using college_management.Servicos.Interfaces;
using college_management.Utilitarios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCursos : Contexto<Curso>,
                              IContextoCursos
{
	public ContextoCursos(BaseDeDados baseDeDados,
	                      Usuario usuarioContexto,
	                      IServicoModelos<Curso> servicoCursos)
		: base(baseDeDados, usuarioContexto)
	{
		_servicoCursos = servicoCursos;
	}

	private readonly IServicoModelos<Curso> _servicoCursos;

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
			var obterMateriasPorCurso
				= BaseDeDados.CursosMaterias.ObterPorCurso(curso.Id);

			if (obterMateriasPorCurso.Status is not StatusResposta.Sucesso)
				return "";

			var materias = obterMateriasPorCurso.Modelo!.Select(mc =>
			{
				return BaseDeDados.Materias
				                  .ObterPorId(mc.MateriaId!.Value)
				                  .Modelo!;
			});

			return $"Curso: {curso.Nome}\n" +
			       $"Ano: {DateTime.Today.Year}\n\n" +
			       $"{string.Join('\n', materias.Select(i => i.Nome))}";
		}

		InputView inputRelatorio = new("Ver Grade Curricular");

		if (TemAcessoRestrito)
		{
			var cursoBuscado = _servicoCursos.Pesquisar();

			if (cursoBuscado is null) return;

			inputRelatorio.LerEntrada("Sair", ObterLayout(cursoBuscado));

			return;
		}

		if (UsuarioContexto is not Aluno aluno)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O usuário atual não é um aluno.");

			return;
		}

		var obterMatriculaAluno
			= BaseDeDados.Matriculas.ObterPorAluno(aluno.Id);
		var cursoId = obterMatriculaAluno.Modelo?.ToArray()[0].CursoId;

		var obterCursoAluno = BaseDeDados.Cursos.ObterPorId(cursoId!.Value);


		if (obterCursoAluno.Status is StatusResposta.ErroNaoEncontrado)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O aluno não está matriculado em nenhum curso.");

			return;
		}

		var layout = ObterLayout(obterCursoAluno.Modelo!);

		inputRelatorio.LerEntrada("Sair", layout);
	}

	public override async Task Cadastrar()
	{
		throw new NotImplementedException();
	}

	public override async Task Editar()
	{
		var curso = _servicoCursos.Pesquisar();

		if (curso is null)
			return;

		var propriedades = curso.GetType().GetProperties().ToList();
		// Essas propriedades devem ser editadas por outros meios.
		propriedades.RemoveAll(i => i.Name == "GradeCurricular");
		propriedades.RemoveAll(i => i.Name == "MatriculasIds");
		// Essa aqui nem se fala. Deveríamos adicionar um método de filtrar essas propriedades.
		propriedades.RemoveAll(i => i.Name == "Id");

		InputView inputView = new("Editar Curso");

		StringBuilder detalhes
			= new("As seguintes mudanças serão aplicadas:\n\n");

		Dictionary<string, string> mudancas = new();

		foreach (var propriedade in propriedades)
		{
			var valor = propriedade.GetValue(curso)?.ToString() ?? string.Empty;
			inputView.LerEntrada(propriedade.Name,
			                     $"Insira um novo valor para {propriedade.Name} [Vazio para \"{valor}\"]:");

			var entrada = inputView.ObterEntrada(propriedade.Name).Trim();
			var mudanca = string.IsNullOrEmpty(entrada)
				? valor
				: entrada;

			if (mudanca == valor) continue;

			detalhes.AppendLine(
				$"{propriedade.Name}: {valor} => {mudanca}");

			mudancas.Add(propriedade.Name, mudanca.Trim());
		}

		if (mudancas.Count <= 0)
		{
			View.Aviso("Nenhuma edição foi feita.");

			return;
		}

		ConfirmaView confirmacao = new("Editar Curso");

		if (confirmacao.Confirmar(detalhes.ToString())
		               .ToString()
		               .ToLower() is not "s")
		{
			View.Aviso($"Editar {nameof(Curso)}: Operação cancelada.");

			return;
		}

		var novoCurso = EditarPropriedade(curso, mudancas.Keys.First(),
		                                  mudancas.Values.First());

		if (novoCurso is null)
		{
			View.Aviso("Não foi possível editar Curso: Propriedade inválida.");

			return;
		}
		
		var editarCurso = await BaseDeDados.Cursos.Atualizar(novoCurso);

		if (_servicoCursos.ValidarResposta(editarCurso, ModoOperacao.Escrita))
		{
			return;
		}
		
		View.Aviso("O Curso atualizado com sucesso.");
	}

	public override async Task Excluir()
	{
		var curso = _servicoCursos.Pesquisar();

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
		var curso = _servicoCursos.Pesquisar();

		if (curso is null) return;

		DetalhesView detalhesCurso
			= new("Curso Encontrado", ObterDetalhes(curso));
		
		detalhesCurso.ConstruirLayout();
		detalhesCurso.Exibir();
	}

	private Dictionary<string, string> ObterDetalhes(Curso curso)
	{
		var detalhesCurso = UtilitarioTipos.ObterPropriedades(curso, ["Nome"]);
		var obterMateriasPorCurso
			= BaseDeDados.CursosMaterias.ObterPorCurso(curso.Id);

		if (obterMateriasPorCurso.Status is not StatusResposta.Sucesso)
			return detalhesCurso;

		var materiasCurso = obterMateriasPorCurso.Modelo!
		                                         .Select(mc =>
		                                         {
			                                         return BaseDeDados
				                                         .Materias
				                                         .ObterPorId(
					                                         mc.MateriaId!
						                                         .Value)
				                                         .Modelo!;
		                                         })
		                                         .ToArray();

		var materiasId   = materiasCurso.Select(i => i.Id.ToString());
		var materiasNome = materiasCurso.Select(i => i.Nome);
		var cargaHoraria = materiasCurso.Sum(m => m.CargaHoraria);

		detalhesCurso.Add("MateriasId", $"{string.Join(", ", materiasId)}");
		detalhesCurso.Add("GradeCurricular",
		                  $"{string.Join(", ", materiasNome)}");
		detalhesCurso.Add("CargaHoraria", $"{cargaHoraria}h");

		return detalhesCurso;
	}

	private Curso? EditarPropriedade(Curso curso,
	                                 string propriedade,
	                                 string? valor)
	{
		var novoCurso = curso;

		switch (propriedade)
		{
			case "Nome":
			{
				novoCurso.Nome = valor ?? curso.Nome;

				return novoCurso;
			}

			default:
				return null;
		}
	}
}
