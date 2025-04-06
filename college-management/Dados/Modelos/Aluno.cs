namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login, string nome, CredenciaisUsuario credenciais) :
		base(login, nome, credenciais)
	{
	}
}
