using System.Globalization;
using System.Text.Json.Serialization;
using college_management.Constantes;
using college_management.Dados.Repositorios;
using college_management.Utilitarios.Atributos;


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
	[PropriedadeModelo(TipoPropriedade.Privada)]
	public CredenciaisUsuario? Credenciais { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string loginUsuario,
	                                  string senhaUsuario)
	{
		var obterUsuario = repositorio.ObterPorLogin(loginUsuario);

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado) return null;

		return obterUsuario.Modelo!.Credenciais!.Validar(senhaUsuario)
			? obterUsuario.Modelo
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

}
