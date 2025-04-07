namespace college_management.Dados.Modelos;


public abstract class Modelo
{
	protected Modelo() { }

	protected Modelo(string nome) { Nome = nome; }

	public string? Nome     { get; set; }
	public uint    Id       { get; set; }
	public uint    GestorId { get; set; }
	public Gestor  Gestor   { get; set; } = null!;
}
