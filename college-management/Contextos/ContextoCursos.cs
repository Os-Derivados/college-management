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
	                      Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

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

        var obterLayoutAluno = (Aluno aluno) =>
        {
            var curso = BaseDeDados.Cursos.ObterTodos()
                .Where(i => i.MatriculasIds?.Contains(aluno.MatriculaId) ?? false)
                .FirstOrDefault() ?? throw new InvalidOperationException("O atual usuário não está matriculado em nenhum curso.");
            return obterLayout(curso);
        };

        InputView inputRelatorio = new("Ver Grade Curricular");

		if (CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita) ||
            CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores))
		{
            MenuView menuPesquisa = new("Pesquisar Curso",
                                        "Escolha o método de pesquisa.",
                                        ["Nome", "ID"]);

            menuPesquisa.ConstruirLayout();
            menuPesquisa.LerEntrada();

            (string Campo, string Mensagem)? campoPesquisa = menuPesquisa.OpcaoEscolhida switch
            {
                1 => ("Nome", "Insira o Nome do curso: "),
                2 => ("ID", "Insira o ID do curso: "),
                _ => ("Campo", "Campo inválido. Tente novamente.")
            };

            InputView inputPesquisa = new("Ver Grade Curricular: Pesquisar Curso");

            inputPesquisa.LerEntrada(campoPesquisa?.Campo!, campoPesquisa?.Mensagem);

            Curso? curso = null;

            if (menuPesquisa.OpcaoEscolhida == 1)
            {
                var nome = inputPesquisa.ObterEntrada("Nome");
				curso = BaseDeDados.Cursos.ObterPorNome(nome);
            }
            else if (menuPesquisa.OpcaoEscolhida == 2)
            {
                var id = inputPesquisa.ObterEntrada("ID");
                curso = BaseDeDados.Cursos.ObterPorId(id);
            }
			else
			{
				return;
			}

            inputPesquisa.LerEntrada("Sair", obterLayout(curso));
			return;
		}

        string layout = string.Empty;

        try
        {
            Aluno? aluno = UsuarioContexto as Aluno ?? throw new InvalidOperationException("O atual usuário não é um aluno.");
            layout = obterLayoutAluno(aluno);
        }
        catch (InvalidOperationException e)
        {
            inputRelatorio.LerEntrada("Erro", e.Message);
            return;
        }

        inputRelatorio.LerEntrada("Sair", layout);
	}

	public override async Task Cadastrar() { throw new NotImplementedException(); }

	public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir() { throw new NotImplementedException(); }

	public override void Visualizar()
	{
        RelatorioView<Curso> relatorioView = new("Visualizar Cursos", BaseDeDados.Cursos.ObterTodos());
        relatorioView.ConstruirLayout();

        InputView inputRelatorio = new(relatorioView.Titulo);
        inputRelatorio.LerEntrada("Sair", relatorioView.Layout.ToString());
    }

	public override void VerDetalhes()
	{
        var naoTemRestricao = CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores)
                      || CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita);

        if (!naoTemRestricao) { }

        MenuView menuPesquisa = new("Pesquisar Curso",
                                    "Escolha o método de pesquisa.",
                                    ["Nome", "ID"]);

        menuPesquisa.ConstruirLayout();
        menuPesquisa.LerEntrada();

        (string Campo, string Mensagem)? campoPesquisa = menuPesquisa.OpcaoEscolhida switch
        {
            1 => ("Nome", "Insira o Nome do curso: "),
            2 => ("ID", "Insira o ID do curso: "),
            _ => ("Campo", "Campo inválido. Tente novamente.")
        };

        InputView inputPesquisa = new("Ver Grade Curricular: Pesquisar Curso");

        inputPesquisa.LerEntrada(campoPesquisa?.Campo!, campoPesquisa?.Mensagem);

        Curso? curso = null;

        if (menuPesquisa.OpcaoEscolhida == 1)
        {
            var nome = inputPesquisa.ObterEntrada("Nome");
            curso = BaseDeDados.Cursos.ObterPorNome(nome);
        }
        else if (menuPesquisa.OpcaoEscolhida == 2)
        {
            var id = inputPesquisa.ObterEntrada("ID");
            curso = BaseDeDados.Cursos.ObterPorId(id);
        }
        else
        {
            return;
        }

        Dictionary<string, string> detalhes =
            UtilitarioTipos.ObterPropriedades(curso,
                                              ["Nome", "GradeCurricular", "MatriculasIds"]);

        detalhes.Add("CargaHoraria", $"{curso.ObterCargaHoraria()}h");

        DetalhesView detalhesCurso = new("Curso Encontrado", detalhes);
        detalhesCurso.ConstruirLayout();

        inputPesquisa.LerEntrada("Sair", detalhesCurso.Layout.ToString());
    }
}
