using System.ComponentModel.DataAnnotations.Schema;
using college_management.Constantes;
using college_management.Dados.Repositorios;
using college_management.Utilitarios;


namespace college_management.Dados.Modelos;


public abstract class Usuario : Modelo
{
	protected Usuario(string login, string nome) : base(nome)
	{
		Login = login;
		Nome  = nome;
	}

	public string? Login { get; set; }

	public string? Senha { get; set; }
	public string? Sal   { get; set; }

	public static Usuario? Autenticar(RepositorioUsuarios repositorio,
	                                  string loginUsuario,
	                                  string senhaUsuario)
	{
		var obterUsuario = repositorio.ObterPorLogin(loginUsuario);

		if (obterUsuario.Status is StatusResposta.ErroNaoEncontrado)
			return null;

		return obterUsuario.Modelo!.Validar(senhaUsuario)
			? obterUsuario.Modelo
			: null;
	}

	public static Usuario CriarUsuario(Dictionary<string, string> cadastro)
	{
		var tipo = Enum.Parse<TipoUsuario>(cadastro["Tipo"]);

		Usuario novoUsuario = tipo switch
		{
			TipoUsuario.Aluno => new Aluno(cadastro["Login"], cadastro["Nome"]),
			TipoUsuario.Docente => new Docente(cadastro["Login"],
			                                   cadastro["Nome"]),
			TipoUsuario.Gestor => new Gestor(cadastro["Login"],
			                                 cadastro["Nome"]),
			_ => throw new ArgumentOutOfRangeException(
				nameof(cadastro),
				tipo,
				null)
		};

		return novoUsuario;
	}

	public override string ToString()
	{
		return
			$"| {Login,-16} | {Nome,-16} | {Crendenciais.Remove(13) + "...",-16} | {Id,-16} |";
	}

	public void GerarCredenciais(string senha, string? sal = null)
	{
		Sal = sal ?? UtilitarioCriptografia.GerarSal();
		Senha = senha.Length >= 64
			? senha
			: UtilitarioCriptografia.CriptografarSha256(senha, Sal);
	}

	public bool Validar(string senha)
	{
		return UtilitarioCriptografia.CriptografarSha256(senha, Sal) == Senha;
	}

	public string Crendenciais => $"{Senha}+{Sal}";
}
