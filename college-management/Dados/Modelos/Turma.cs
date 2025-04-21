using System.ComponentModel.DataAnnotations;


namespace college_management.Dados.Modelos;


public class Turma
{
	[Required]
	public Turno Turno { get; set; }

	public uint     MateriaId { get; set; }
	public Materia? Materia   { get; set; }

	public uint     DocenteId { get; set; }
	public Docente? Docente   { get; set; }

	public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
}
