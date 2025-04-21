namespace college_management.Dados.Modelos;


public class CorpoDocente : IRastreavel
{
	public uint     MateriaId { get; set; }
	public Materia? Materia   { get; set; }
	public uint     DocenteId { get; set; }
	public Docente? Docente   { get; set; }
	public uint?    GestorId  { get; set; }
	public Gestor?  Gestor    { get; set; }
}
