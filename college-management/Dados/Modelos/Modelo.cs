using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;


namespace college_management.Dados.Modelos;


public abstract class Modelo : IRastreavel
{
	protected Modelo() { }

	protected Modelo(string nome) { Nome = nome; }

	[Required]
	[StringLength(128)]
	public string? Nome { get; set; }

	[Key]
	public uint Id { get; set; }


	public string? CriadoPor { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }

	[NotMapped]
	protected virtual string[] CamposRelatorio => [
		"Id", "Nome", "CriadoPor", "CriadoEm", "EditadoPor", "EditadoEm"
	];

	public string? CabecalhoRelatorio()
	{
		StringBuilder sb = new();
		var tipoModelo = GetType();

		if (CamposRelatorio.Length is 0) return "Nenhum Campo Informado";

		foreach (var campo in CamposRelatorio)
		{
			if (tipoModelo.GetProperty(campo) is PropertyInfo propriedade)
			{
				sb.Append($"| {propriedade.Name,-16} ");
			}
		}

		sb.AppendLine("|");

		foreach (var campo in CamposRelatorio)
		{
			if (tipoModelo.GetProperty(campo) is PropertyInfo propriedade)
			{
				sb.Append($"| {new string('-', 16)} ");
			}
		}

		sb.Append('|');

		return sb.ToString();
	}

	public string? EntradaRelatorio(IEnumerable<string> campos)
	{
		throw new NotImplementedException();
	}
}
