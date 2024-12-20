using System.Globalization;
using System.Text.Json.Serialization;
using college_management.Constantes;
using college_management.Dados.Repositorios;
namespace college_management.Dados.Modelos;

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

		if (usuarioExistente.Status is StatusResposta.ErroNaoEncontrado) return null;

		return usuarioExistente.Modelo!.Credenciais!.Validar(senhaUsuario)
			       ? usuarioExistente.Modelo
			       : null;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {CargoId,-16} | {Credenciais?.ToString().Remove(13) + "...",-16} | {Id,-16} |";
	}
}
