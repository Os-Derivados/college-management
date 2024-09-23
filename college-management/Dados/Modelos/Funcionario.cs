namespace college_management.Dados.Modelos;


public sealed class Funcionario : Usuario
{
	public Funcionario(string login,
	                   string nome,
	                   string senha,
	                   string cargoId)
		: base(login,
		       nome,
		       senha,
		       cargoId) { }
}
