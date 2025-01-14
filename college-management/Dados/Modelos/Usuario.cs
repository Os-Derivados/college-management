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
	private static long _contagemId = 10000000000;

	public Usuario(string login,
	               string nome,
	               CredenciaisUsuario credenciais,
	               string cargoId)
	{
		Login       = login;
		Nome        = nome;
		CargoId     = cargoId;
		Credenciais = credenciais;

		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		_contagemId++;
	}

	public string?             Login       { get; set; }
	public string?             Nome        { get; set; }
	public string              CargoId     { get; set; }
	public CredenciaisUsuario? Credenciais { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string loginUsuario,
	                                  string senhaUsuario)
	{
		// master.admin
		var usuarioExistente = repositorio.ObterPorLogin(loginUsuario);

		if (usuarioExistente is null) return null;

		return usuarioExistente.Credenciais.Validar(senhaUsuario)
			? usuarioExistente
			: null;
	}

	public static Usuario CriarUsuario(Cargo cargoEscolhido,
	                                   Dictionary<string, string> cadastro,
	                                   Matricula novaMatricula)
	{
		Usuario novoUsuario = cargoEscolhido.Nome switch
		{
			CargosPadrao.CargoAlunos => new Aluno(cadastro["Login"],
			                                      cadastro["Nome"],
			                                      new CredenciaisUsuario(
				                                      cadastro["Senha"]),
			                                      cargoEscolhido.Id!,
			                                      novaMatricula.Id!),
			_ => new Funcionario(cadastro["Login"],
			                     cadastro["Nome"],
			                     new CredenciaisUsuario(
				                     cadastro["Senha"]),
			                     cargoEscolhido.Id!)
		};

		return novoUsuario;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {CargoId,-16} | {Credenciais?.ToString().Remove(13) + "...",-16} | {Id,-16} |";
	}
}
