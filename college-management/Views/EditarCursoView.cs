using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class EditarCursoView : View, IEditarModeloView<Curso>
{
	public EditarCursoView(Curso curso)
		: base("Editar Curso")
	{
		_curso = curso;
	}

	private Curso _curso { get; }

	public Curso Editar()
	{
		MenuView camposEditaveis = new("Editar Curso",
		                               "Selecione um dos campos para editar.",
		                               ["Nome"]);

		camposEditaveis.ConstruirLayout();
		camposEditaveis.LerEntrada();

		while (camposEditaveis.OpcaoEscolhida is not 0)
		{
			Console.Clear();

			var indiceOpcao    = camposEditaveis.OpcaoEscolhida;
			var opcaoEscolhida = camposEditaveis.Opcoes[indiceOpcao - 1];
			var mensagemCampo
				= $"Insira um novo valor para \"{opcaoEscolhida}\": ";

			InputView inputEdicao = new("Editar Curso");
			inputEdicao.LerEntrada(opcaoEscolhida, mensagemCampo);

			switch (camposEditaveis.OpcaoEscolhida)
			{
				case 1:
				{
					_curso.Nome = inputEdicao.ObterEntrada("Nome");

					break;
				}
			}

			DetalhesView detalhesCurso = new("Editar Curso",
			                                   UtilitarioTipos
				                                   .ObterPropriedades(_curso,
				                                   [
					                                   "Nome", "GradeCurricular",
					                                   "MatriculasIds",
				                                   ]));

			detalhesCurso.ConstruirLayout();

			camposEditaveis = new MenuView("Editar Curso",
			                               $"""
			                                {detalhesCurso.Layout}

			                                Os campos editáveis estão abaixo.
			                                """,
			                               ["Nome"]);

			camposEditaveis.ConstruirLayout();
			camposEditaveis.LerEntrada();
		}

		return _curso;
	}
}
