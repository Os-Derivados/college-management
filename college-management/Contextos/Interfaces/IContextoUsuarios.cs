using college_management.Dados.Modelos;

namespace college_management.Contextos.Interfaces;


public interface IContextoUsuarios : IContexto<Usuario>
{
	public Task CadastrarGestor();
	public Task CadastrarDocente();
	public Task CadastrarAluno();
	public Task AlterarSenha();
}
