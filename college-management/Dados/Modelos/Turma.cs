using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace college_management.Dados.Modelos;


[Table("Turmas")]
public class Turma : Modelo
{
	[Required]
	public Turno Turno { get; set; }

	public uint? MateriaId { get; set; }
	public virtual Materia? Materia { get; set; }
	public uint? DocenteId { get; set; }
	public virtual Docente? Docente { get; set; }

	[NotMapped]
	public string Codigo => $"{MateriaId}{DocenteId}-{Turno}";

	public virtual ICollection<Aluno> Alunos { get; set; } = [];
	public virtual ICollection<TurmaAluno> TurmaAlunos { get; set; } = [];
}
