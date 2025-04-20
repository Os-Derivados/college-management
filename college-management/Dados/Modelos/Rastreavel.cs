namespace college_management.Dados.Modelos;


public abstract class Rastreavel
{
	public uint?   GestorId { get; set; }
	public Gestor? Gestor   { get; set; }
}
