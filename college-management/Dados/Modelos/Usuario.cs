using System.Globalization;
using System.Text.Json.Serialization;
using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;

public class CredenciaisUsuario
{
	public string Senha { get; set; }
	public string Sal   { get; set; }
	
	public CredenciaisUsuario(string senha, string? sal = null)
	{
		Senha = senha;
		Sal = sal ?? Utilitarios.UtilitarioCriptografia.GerarSal();
	}

	public static CredenciaisUsuario? TryParse(string input)
	{
		if (string.IsNullOrEmpty(input))
			return null;

		var split = input.Split('+', 2);
		if (split.Length <= 1) return null;

		(string senha, string sal) = (split[0], split[1]);
		return new CredenciaisUsuario(senha, sal);
	}

	public bool Validar(string senha)
	{
        return Utilitarios.UtilitarioCriptografia.CriptografarSha256(senha, Sal) == Senha;
	}

	public override string ToString()
	{
		return $"{Senha}+{Sal}";
	}
}

[JsonDerivedType(typeof(Usuario), "base")]
[JsonDerivedType(typeof(Aluno), "aluno")]
[JsonDerivedType(typeof(Funcionario), "funcionario")]
public class Usuario : Modelo
{
	public Usuario(string login,
	               string nome,
	               CredenciaisUsuario credenciais,
	               string cargoId)
	{
		Login   = login;
		Nome    = nome;
		CargoId = cargoId;
		Credenciais = credenciais;

		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		_contagemId++;
	}

	private static long _contagemId = 10000000000;

	public string? Login   { get; set; }
	public string? Nome    { get; set; }
	public string  CargoId { get; set; }
	public CredenciaisUsuario? Credenciais { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string              loginUsuario,
	                                  string              senhaUsuario)
	{
		// master.admin
		var usuarioExistente = repositorio.ObterPorLogin(loginUsuario);

		if (usuarioExistente is null) return null;

		return usuarioExistente.Credenciais.Validar(senhaUsuario)
			       ? usuarioExistente
			       : null;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {CargoId,-16} | {Credenciais?.ToString().Remove(13) + "...",-16} | {Id,-16} |";
	}
}
