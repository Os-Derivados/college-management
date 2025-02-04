namespace college_management.Dados.Modelos;


public class CursoMateria : Modelo
{
	public CursoMateria(ulong cursoId, ulong materiaId)
	{
		CursoId = cursoId;
		MateriaId = materiaId;
		
		Id = _contagemId++;
	}
	
	private static ulong _contagemId = 10000000000;
	
	public ulong? CursoId { get; set; }
	public ulong? MateriaId { get; set; }
}
