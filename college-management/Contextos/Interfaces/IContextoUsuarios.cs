using college_management.Dados.Modelos;

namespace college_management.Contextos.Interfaces;


public interface IContextoUsuarios : IContexto<Usuario>
{
	public Task AlterarSenha();
}
