namespace college_management.Dados.Modelos;


public class Gestor : Usuario
{
	public Gestor(string login, string nome, CredenciaisUsuario credenciais) :
		base(login, nome, credenciais)
	{
	}

	public Cargo Cargo { get; set; }
}

public enum Cargo
{
	Operador,
	Administrador
}
