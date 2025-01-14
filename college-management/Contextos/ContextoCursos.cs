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
		var obterLayout = (Curso curso) =>
		{
			return $"Curso: {curso.Nome}\n" +
			       $"Ano: {DateTime.Today.Year}\n\n" +
			       $"{string.Join('\n', curso.GradeCurricular.Select(i => i.Nome))}";
		};

		InputView inputRelatorio = new("Ver Grade Curricular");

		Curso? curso = null;

		if (TemAcessoRestrito)
		{
			curso = PesquisarCurso();
			if (curso is null)
				return;

			inputRelatorio.LerEntrada("Sair", obterLayout(curso));
			return;
		}

		var layout = string.Empty;

		var aluno = UsuarioContexto as Aluno;

		if (aluno is null)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O usuário atual não é um aluno.");
			return;
		}

		curso = BaseDeDados.Cursos.ObterTodos()
		                   .Where(i => i.MatriculasIds?.Contains(
			                          aluno.MatriculaId) ?? false)
		                   .FirstOrDefault();

		if (curso is null)
		{
			inputRelatorio.LerEntrada(
				"Erro", "O aluno não está matriculado em nenhum curso.");
			return;
		}

		layout = obterLayout(curso);

		inputRelatorio.LerEntrada("Sair", layout);
	}

	public override async Task Cadastrar()
	{
		throw new NotImplementedException();
	}

	public override async Task Editar()
	{
		var curso = PesquisarCurso();
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

			if (mudanca != valor)
			{
				detalhes.AppendLine(
					$"{propriedade.Name}: {valor} => {mudanca}");
				mudancas.Add(propriedade.Name, mudanca.Trim());
			}
		}

		if (mudancas.Count <= 0)
		{
			inputView.LerEntrada("Erro", "Nenhuma edição foi feita.");
			return;
		}

		if (Confirmar(detalhes.ToString(), "Deseja aplicar mudanças?"))
			foreach ((var propriedade, var valor) in mudancas)
				EditarPropriedade(curso, propriedade, valor);
	}

	public override async Task Excluir()
	{
		var curso = PesquisarCurso();
		if (curso is null)
			return;

		DetalhesView detalhesCurso = new(string.Empty, ObterDetalhes(curso));
		detalhesCurso.ConstruirLayout();

		var confimacao = Confirmar(detalhesCurso.Layout.ToString(),
		                           "Tem certeza que deseja excluir esse curso?");
		if (confimacao)
			await BaseDeDados.Cursos.Remover(curso.Id);
	}

	public override void Visualizar()
	{
		var cursos = BaseDeDados.Cursos.ObterTodos();

		InputView inputRelatorio = new("Visualizar Cursos");

		if (cursos.Count > 0)
		{
			RelatorioView<Curso> relatorioView
				= new(inputRelatorio.Titulo, cursos);
			relatorioView.ConstruirLayout();

			inputRelatorio.LerEntrada("Sair", relatorioView.Layout.ToString());
		}
		else
		{
			inputRelatorio.LerEntrada("Erro", "Nenhum curso cadastrado.");
		}
	}

	public override void VerDetalhes()
	{
		var curso = PesquisarCurso();
		if (curso is null)
			return;

		DetalhesView detalhesCurso
			= new("Curso Encontrado", ObterDetalhes(curso));
		detalhesCurso.ConstruirLayout();

		new InputView("Cursos: Ver Detalhes").LerEntrada(
			"Sair", detalhesCurso.Layout.ToString());
	}

	private Curso? PesquisarCurso()
	{
		MenuView menuPesquisa = new("Pesquisar Curso",
		                            "Escolha o método de pesquisa.",
		                            ["Nome", "Id"]);

		InputView inputPesquisa = new("Ver Grade Curricular: Pesquisar Curso");

		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		(string Campo, string Mensagem)? campoPesquisa
			= menuPesquisa.OpcaoEscolhida switch
			{
				1 => ("Nome", "Insira o Nome do curso: "),
				2 => ("Id", "Insira o Id do curso: "),
				_ => null
			};

		if (campoPesquisa is null) return null;

		Curso? curso = null;

		inputPesquisa.LerEntrada(campoPesquisa?.Campo!,
		                         campoPesquisa?.Mensagem);
		curso = menuPesquisa.OpcaoEscolhida switch
		{
			1 => BaseDeDados.Cursos.ObterPorNome(
				inputPesquisa.ObterEntrada("Nome")),
			2 => BaseDeDados.Cursos.ObterPorNome(
				inputPesquisa.ObterEntrada("Id")),
			_ => null
		};

		if (curso is null)
		{
			inputPesquisa.LerEntrada("Erro", "Curso não encontrado.");
			return PesquisarCurso();
		}

		return curso;
	}

	private Dictionary<string, string> ObterDetalhes(Curso curso)
	{
		var detalhes = UtilitarioTipos.ObterPropriedades(curso, ["Nome"]);

		detalhes.Add("MateriasId",
		             $"{string.Join(", ", curso.MatriculasIds ?? new List<string>())}");
		detalhes.Add("GradeCurricular",
		             $"{string.Join(", ", curso.GradeCurricular.Select(i => i.Nome))}");
		detalhes.Add("CargaHoraria", $"{curso.ObterCargaHoraria()}h");

		return detalhes;
	}

	private bool EditarPropriedade(Curso curso,
	                               string propriedade,
	                               string? valor)
	{
		switch (propriedade)
		{
			case "Nome":
			{
				curso.Nome = valor ?? curso.Nome;
				return true;
			}

			default:
				return false;
		}
	}

	private bool Confirmar(string layout, string mensagem)
	{
		var confirmacao = -1;

		do
		{
			InputView inputView = new("Confirmar Remoção");
			inputView.LerEntrada("Confirmação", layout +
			                                    $"\n\n{mensagem} (S/N)");

			confirmacao = inputView.ObterEntrada("Confirmação")
			                       .ToLower()
			                       .FirstOrDefault() switch
			{
				'n' => 0,
				's' => 1,
				_   => -1
			};
		} while (confirmacao < 0);

		return confirmacao == 1;
	}
}
