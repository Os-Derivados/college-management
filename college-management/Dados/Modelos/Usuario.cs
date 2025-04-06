using System.Text.Json.Serialization;
using college_management.Constantes;
using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


[JsonDerivedType(typeof(Usuario), "Base")]
[JsonDerivedType(typeof(Aluno), "Aluno")]
[JsonDerivedType(typeof(Docente), "Docente")]
[JsonDerivedType(typeof(Gestor), "Gestor")]
public class Usuario : Modelo
{
	public Usuario(string login,
	               string nome,
	               CredenciaisUsuario credenciais) : base(nome)
	{
		Login       = login;
		Nome        = nome;
		Credenciais = credenciais;
	}

	public string?             Login       { get; set; }
	public CredenciaisUsuario? Credenciais { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string loginUsuario,
	                                  string senhaUsuario)
	{
		var obterUsuario = repositorio.ObterPorLogin(loginUsuario);

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
			return null;

		return obterUsuario.Modelo!.Credenciais!.Validar(senhaUsuario)
			? obterUsuario.Modelo
			: null;
	}

	public static Usuario CriarUsuario(TipoUsuario tipo,
	                                   Dictionary<string, string> cadastro)
	{
		Usuario novoUsuario = tipo switch
		{
			TipoUsuario.Aluno => new Aluno(cadastro["Login"],
			                               cadastro["Nome"],
			                               new CredenciaisUsuario(
				                               cadastro["Senha"])),
			TipoUsuario.Docente => new Docente(cadastro["Login"],
			                                   cadastro["Nome"],
			                                   new CredenciaisUsuario(
				                                   cadastro["Senha"])),
			TipoUsuario.Gestor => new Gestor(cadastro["Login"],
			                                 cadastro["Nome"],
			                                 new CredenciaisUsuario(
				                                 cadastro["Senha"])),
		};

		return novoUsuario;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {Credenciais?.ToString().Remove(13) + "...",-16} | {Id,-16} |";
	}
}
