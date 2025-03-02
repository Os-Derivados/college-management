using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class EditarCursoView : View, IEditarModeloView<Curso>
{
	private IRepositorio<Materia> _repositorioMaterias;

	public EditarCursoView(Curso curso,
		Repositorio<Materia> repositorioMaterias)
		: base("Editar Curso")
	{
		_curso = curso;
		_repositorioMaterias = repositorioMaterias;
	}

	private Curso _curso { get; }
	
	public Curso Editar()
	{
		MenuView camposEditaveis = new MenuView("Editar Curso",
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

			if (camposEditaveis.OpcaoEscolhida is 1) // Nome
			{
				EditarNome();
			}
			else if (camposEditaveis.OpcaoEscolhida is 2) // GradeCurricular
			{
				EditarGradeCurricular();
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

		return _curso;
	}

	private string ObterDetalhes()
	{
		DetalhesView detalhesCurso = new("Editar Curso",
			UtilitarioTipos
				.ObterPropriedades(_curso,
				[
					"Nome",
				]));

		detalhesCurso.ConstruirLayout();
		detalhesCurso.Layout.AppendLine("GradeCurricular:");
		foreach (var materia in _curso.GradeCurricular)
		{
			detalhesCurso.Layout.AppendLine($"\t{materia.Nome}");
		}

		detalhesCurso.Layout.AppendLine("MatriculaIds:");
		foreach (var matricula in _curso.MatriculasIds)
		{
			detalhesCurso.Layout.AppendLine($"\t{matricula}");
		}

		return detalhesCurso.Layout.ToString();
	}

	private void EditarNome()
	{
		var mensagemCampo
			= $"Insira um novo valor para \"Nome\": ";

		InputView inputEdicao = new("Editar Curso");
		inputEdicao.LerEntrada("Nome", mensagemCampo);
		if (string.IsNullOrEmpty(inputEdicao.ObterEntrada("Nome").Trim()))
		{
			View.Aviso("Nome não pode estar vazio.");
			return;
		}

		_curso.Nome = inputEdicao.ObterEntrada("Nome").Trim();
	}

	private void EditarGradeCurricular()
	{
		MenuView acaoView = new("Selecione a ação desejada.",
			string.Empty,
			["Adicionar", "Remover"]);
		acaoView.ConstruirLayout();
		acaoView.LerEntrada();

		switch (acaoView.OpcaoEscolhida)
		{
			case 0:
				return;

			default:
			{
				List<Materia> antigoGradeCurricular = _curso.GradeCurricular.ToList();

				while (true)
				{
					InputView inputMateria = new($"{(acaoView.OpcaoEscolhida is 1 ? "Adicionar" : "Remover")} matéria para a grade curricular\n{string.Join("\n", antigoGradeCurricular.Select(i => i.Nome).ToList())}\n");
					inputMateria.LerEntrada("MateriaNome", "Deixe vazio para sair. Insira o Nome ou Id da matéria: ");
					if (string.IsNullOrEmpty(inputMateria.ObterEntrada("MateriaNome").Trim()))
						break;

					string moniker = inputMateria.ObterEntrada("MateriaNome");

					Materia? materia = null;

					var respostaNome = _repositorioMaterias.ObterPorNome(moniker);
					var respostaId = _repositorioMaterias.ObterPorId(moniker);

					materia = respostaNome.Status is StatusResposta.Sucesso
						? respostaNome.Modelo
						: (respostaId.Status is StatusResposta.Sucesso
							? respostaId.Modelo
							: null);

					if (materia is null)
					{
						View.Aviso($"Matéria com o identificador \"{moniker}\" não encontrada. Tente novamente.");
						continue;
					}
					else
					{
						if (acaoView.OpcaoEscolhida is 1)
							antigoGradeCurricular.Add(materia);
						else
							antigoGradeCurricular.Remove(materia);
					}
				}

				_curso.GradeCurricular = antigoGradeCurricular.ToArray();
			} break;
		}
	}
}
