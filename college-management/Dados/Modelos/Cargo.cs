using System.Globalization;
using System.Text;


namespace college_management.Dados.Modelos;


public sealed class Cargo : Modelo
{
	
	public Cargo(string nome, List<string> permissoes)
	{
		Nome       = nome;
		Permissoes = permissoes;
	}

	public string?       Nome        { get; set; }
	public List<string>  Permissoes  { get; set; }

	public bool TemPermissao(string permissao)
	{
		return Permissoes.Any(p => p == permissao);
	}

	public string VerPermissoes()
	{
		StringBuilder permissoes = new();

		foreach (var p in Permissoes)
		{
			var permissaoFormatada = p.Replace("Acesso", "");
			permissoes.Append($"{permissaoFormatada}; ");
		}

		return permissoes.ToString();
	}

	public override string ToString()
	{
		return
			$"| {Nome,-16} | {VerPermissoes(),-16} | {Id,-16} |";
	}
}
