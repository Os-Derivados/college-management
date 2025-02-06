using college_management.Utilitarios;


namespace college_management.Dados;


public class CredenciaisUsuario
{
	public CredenciaisUsuario(string senha, string? sal = null)
	{
		Sal = sal ?? UtilitarioCriptografia.GerarSal();
		Senha = senha.Length >= 64
			? senha
			: UtilitarioCriptografia.CriptografarSha256(senha, Sal);
	}

	public string Senha { get; set; }
	public string Sal   { get; set; }

	public bool Validar(string senha)
	{
		return UtilitarioCriptografia.CriptografarSha256(senha, Sal) == Senha;
	}

	public override string ToString() { return $"{Senha}+{Sal}"; }
}
