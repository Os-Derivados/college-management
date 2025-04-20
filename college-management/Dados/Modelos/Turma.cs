namespace college_management.Dados.Modelos;


public class Turma
{
	public Turno   Turno     { get; set; }
	public uint    MateriaId { get; set; }
	public uint    AlunoId   { get; set; }
	public uint    DocenteId { get; set; }
	public Docente Docente   { get; set; } = null!;
}
