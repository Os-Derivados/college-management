using System.Globalization;
using System.Text.Json.Serialization;


namespace college_management.Dados.Modelos;


[JsonDerivedType(typeof(Usuario), "base")]
[JsonDerivedType(typeof(Aluno), "aluno")]
[JsonDerivedType(typeof(Funcionario), "funcionario")]
public class Usuario : Modelo
{
	public Usuario(string login,
	               string nome,
	               string senha,
	               string cargoId)
	{
		Login = login;
		Nome = nome;
		Senha = senha;
		CargoId = cargoId;

		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		_contagemId++;
	}

	private static long _contagemId = 10000000000;

	public string? Login   { get; set; }
	public string? Nome    { get; set; }
	public string? Senha   { get; set; }
	public string  CargoId { get; set; }

	public static bool Autenticar(Usuario usuario,
	                              string  loginUsuario,
	                              string  senhaUsuario)
	{
		return usuario.Login == loginUsuario
		       && usuario.Senha == senhaUsuario;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {CargoId,-16} | {"x",-16} | {Id,-16} |";
	}
}
