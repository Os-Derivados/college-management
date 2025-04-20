using System.ComponentModel.DataAnnotations;


namespace college_management.Dados.Modelos;


public abstract class Modelo : Rastreavel
{
	protected Modelo() { }

	protected Modelo(string nome) { Nome = nome; }

	[Required]
	[StringLength(128)]
	public string? Nome     { get; set; }
	
	[Key]
	public uint    Id       { get;  set; }
}
