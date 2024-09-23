namespace college_management.Dados.Modelos;


public sealed class Funcionario : Usuario
{
	public Funcionario(string login,
	                   string nome,
	                   string cargoId,
	                   string senha)
		: base(login,
		       nome,
		       cargoId,
		       senha) { }
}
