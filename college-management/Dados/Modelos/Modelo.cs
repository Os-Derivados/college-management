using System.ComponentModel.DataAnnotations;
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


	public string? CriadoPor  { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }

	public string? CabecalhoRelatorio(IEnumerable<string> campos)
	{
		StringBuilder sb = new();
		var tipoModelo = GetType();

		if (!campos.Any()) return "Nenhum Campo Informado";

		foreach (var campo in campos)
		{
			if (tipoModelo.GetProperty(campo) is PropertyInfo propriedade)
			{
				sb.Append($"| {propriedade.Name,-16} ");
			}
		}

		sb.AppendLine("|");

		foreach (var campo in campos)
		{
			if (tipoModelo.GetProperty(campo) is PropertyInfo propriedade)
			{
				sb.Append($"| {new string('-', 16)} ");
			}
		}

		sb.AppendLine("|");

		return sb.ToString();
	}

	public string? EntradaRelatorio(IEnumerable<string> campos)
	{
		throw new NotImplementedException();
	}
}
