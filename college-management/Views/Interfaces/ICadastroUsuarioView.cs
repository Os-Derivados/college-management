namespace college_management.Views.Interfaces;


public interface ICadastroUsuarioView
{
	public void Cadastrar();

	public (string nome, string login, string cargo, string senha)
		ObterDadosBase();
}