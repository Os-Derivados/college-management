namespace college_management.Dados.Modelos;


public class CursoMateria : Modelo
{
	public CursoMateria(Guid? cursoId, Guid? materiaId)
	{
		CursoId = cursoId;
		MateriaId = materiaId;
	}
	
	public Guid? CursoId { get; set; }
	public Guid? MateriaId { get; set; }
}
