using System.Globalization;
using System.Text.Json.Serialization;
using college_management.Dados.Repositorios;


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
		Login   = login;
		Nome    = nome;
		Senha   = senha;
		CargoId = cargoId;

		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		_contagemId++;
	}

	private static long _contagemId = 10000000000;

	public string? Login   { get; set; }
	public string? Nome    { get; set; }
    // TODO: Realizar um hash na senha do usuário. Idealmente, um salt também deverá ser implementado.
    public string? Senha   { get; set; }
	public string  CargoId { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string              loginUsuario,
	                                  string              senhaUsuario)
	{
		// master.admin
		var usuarioExistente = repositorio.ObterPorLogin(loginUsuario);

		if (usuarioExistente is null) return null;

		return usuarioExistente.Senha == senhaUsuario 
			       ? usuarioExistente 
			       : null;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {"x",-16} | {CargoId,-16} | {Id,-16} |";
	}
}
