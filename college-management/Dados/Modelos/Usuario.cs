using college_management.Constantes;
using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


public abstract class Usuario : Modelo
{
	protected Usuario(string login,
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
			_ => throw new ArgumentOutOfRangeException(nameof(tipo), tipo, null)
		};

		return novoUsuario;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {Credenciais?.ToString().Remove(13) + "...",-16} | {Id,-16} |";
	}
}
