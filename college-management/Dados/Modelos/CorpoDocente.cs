namespace college_management.Dados.Modelos;


public class CorpoDocente : IRastreavel
{
	public uint    MateriaId { get; set; }
	public uint    DocenteId { get; set; }
	public uint?   GestorId  { get; set; }
	public Gestor? Gestor    { get; set; }
}
