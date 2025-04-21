using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


public class GradeCurricular : IRastreavel
{
	public uint    CursoId   { get; set; }
	public uint    MateriaId { get; set; }
	public uint?   GestorId  { get; set; }
	public Gestor? Gestor    { get; set; }
}
