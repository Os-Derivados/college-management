namespace college_management.Dados.Modelos;


public sealed class Funcionario : Usuario
{
	public Funcionario(string login,
	                   string nome,
	                   CredenciaisUsuario credenciais,
	                   Guid cargoId)
		: base(login, nome, credenciais, cargoId)
	{
	}
}
