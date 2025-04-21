using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace college_management.Dados.Modelos;


public class Turma : Modelo
{
	[Required]
	public Turno Turno { get; set; }

	public uint?    MateriaId { get; set; }
	public Materia? Materia   { get; set; }

	public uint?    DocenteId { get; set; }
	public Docente? Docente   { get; set; }

	[NotMapped]
	public string Codigo => $"{MateriaId}{DocenteId}-{Turno.ToString()}";

	public ICollection<Aluno>      Alunos      { get; set; } = [];
	public ICollection<TurmaAluno> TurmaAlunos { get; set; } = [];
}
