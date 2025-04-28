using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class EditarCursoView : View, IEditarModeloView<Curso>
{
	public EditarCursoView(Curso curso,
	                       Repositorio<Materia> repositorioMaterias) : base(
		"Editar Curso")
	{
		Curso                = curso;
		_repositorioMaterias = repositorioMaterias;
		_nome                = curso.Nome;
	}

	private readonly IRepositorio<Materia> _repositorioMaterias;
	private          string?               _nome;
	private          Curso                 Curso { get; }

	public Curso Editar()
	{
		MenuView camposEditaveis = new("Editar Curso",
		                               $"""
		                                {ObterDetalhes()}

		                                Os campos editáveis estão abaixo.
		                                """,
		                               ["Nome", "Grade Curricular"]);

		camposEditaveis.ConstruirLayout();
		camposEditaveis.LerEntrada();

		while (camposEditaveis.OpcaoEscolhida is not 0)
		{
			Console.Clear();

			switch (camposEditaveis.OpcaoEscolhida)
			{
				case 1:
					EditarNome();
					break;

				case 2:
					EditarGradeCurricular();
					break;
			}

			camposEditaveis = new MenuView("Editar Curso",
			                               $"""
			                                {ObterDetalhes()}

			                                Os campos editáveis estão abaixo.
			                                """,
			                               ["Nome", "Grade Curricular"]);

			camposEditaveis.ConstruirLayout();
			camposEditaveis.LerEntrada();
		}

		ConfirmaView confirmaView = new("Editar Curso");
		confirmaView.ConstruirLayout();
		confirmaView.Layout.AppendLine(ObterDetalhes());

		if (confirmaView.Confirmar("Deseja aplicar as alterações?")
		                .ToString()
		                .ToLower() is not "s") return Curso;

		Curso.Nome = _nome;

		return Curso;
	}

	private string ObterDetalhes()
	{
		DetalhesView detalhesCurso = new("Editar Curso", []);

		detalhesCurso.ConstruirLayout();
		detalhesCurso.Layout.AppendLine($"Nome: {_nome}");
		detalhesCurso.Layout.AppendLine("GradeCurricular:");

		foreach (var materia in Curso.Materias)
		{
			detalhesCurso.Layout.AppendLine($"\t{materia.Nome}");
		}

		return detalhesCurso.Layout.ToString();
	}

	private void EditarNome()
	{
		var mensagemCampo = "Insira um novo valor para \"Nome\": ";

		InputView inputEdicao = new("Editar Curso");
		inputEdicao.LerEntrada("Nome", mensagemCampo);

		if (string.IsNullOrEmpty(inputEdicao.ObterEntrada("Nome").Trim()))
		{
			Aviso("Nome não pode estar vazio.");
			return;
		}

		_nome = inputEdicao.ObterEntrada("Nome").Trim();
	}

	private void EditarGradeCurricular()
	{
		MenuView acaoView = new("Selecione a ação desejada.",
		                        string.Empty,
		                        ["Adicionar", "Remover"]);
		acaoView.ConstruirLayout();
		acaoView.LerEntrada();

		if (acaoView.OpcaoEscolhida is 0)
		{
			return;
		}

		while (true)
		{
			var acao = acaoView.OpcaoEscolhida is 1 ? "Adicionar" : "Remover";

			var titulo
				= $"{acao} matéria para a grade curricular\n{string.Join("\n", Curso.Materias.Select(i => i.Nome).ToList())}\n";

			InputView inputMateria = new(titulo);

			inputMateria.LerEntrada("MateriaNome",
			                        "Deixe vazio para sair. Insira o Nome ou Id da matéria: ");
			if (string.IsNullOrEmpty(inputMateria.ObterEntrada("MateriaNome")
			                                     .Trim()))
				break;

			var nome = inputMateria.ObterEntrada("MateriaNome");

			var respostaNome = _repositorioMaterias.ObterPorNome(nome);

			var materia = respostaNome.Status is StatusResposta.Sucesso
				? respostaNome.Modelo
				: null;

			if (materia is null)
			{
				Aviso(
					$"Matéria com o identificador \"{nome}\" não encontrada. Tente novamente.");
				continue;
			}

			if (acaoView.OpcaoEscolhida is 1)
				Curso.Materias.Add(materia);
			else
				Curso.Materias.Remove(materia);
		}
	}
}
