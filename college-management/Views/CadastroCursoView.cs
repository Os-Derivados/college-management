using System.Text;
using college_management.Constantes;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class CadastroCursoView : ICadastroView
{
	public string Nome;
	public List<string> GradeCurricular = new();

	public char ObterDados()
	{
		InputView inputNome = new("Cadastro de Curso: Nome");
		inputNome.LerEntrada("Nome", "Insira o nome do curso: ");
		Nome = inputNome.ObterEntrada("Nome");

		while (true)
		{
			InputView inputMateria = new($"Cadastro de Curso: Grade Curricular\n{string.Join("\n", GradeCurricular)}\n");
			inputMateria.LerEntrada("Nome", "Deixe vazio para sair. Insira o Nome ou Id da matéria: ");
			if (string.IsNullOrEmpty(inputMateria.ObterEntrada("Nome")))
				break;
			
			GradeCurricular.Add(inputMateria.ObterEntrada("Nome"));
		}
		
		DetalhesView detalhesView = new("Confirmar Cadastro",
			inputNome.EntradasUsuario);
		detalhesView.ConstruirLayout();

		StringBuilder mensagemConfirmacao = new();
		mensagemConfirmacao.Append(detalhesView.Layout);

		ConfirmaView confirmarCadastro = new("Cadastrar Usuário");

		return confirmarCadastro.Confirmar(mensagemConfirmacao.ToString());
	}
}