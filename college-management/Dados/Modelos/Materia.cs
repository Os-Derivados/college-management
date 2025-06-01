using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace college_management.Dados.Modelos;


[Table("Materias")]
[Index(nameof(Nome), IsUnique = true)]
public class Materia : Modelo
{
	public Materia(string nome) : base(nome) { }

	[NotMapped]
	public uint CargaHoraria { get; set; }

	public virtual ICollection<Turma> Turmas { get; set; } = [];
	public virtual ICollection<Aluno> Alunos { get; set; } = [];
	public virtual ICollection<Curso> Cursos { get; set; } = [];
	public virtual ICollection<Docente> Docentes { get; set; } = [];
	public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = [];
	public virtual ICollection<GradeCurricular> GradesCurriculares { get; set; } = [];
	public virtual ICollection<CorpoDocente> CorpoDocente { get; set; } = [];

	[NotMapped]
	protected override string[] CamposRelatorio => [
		"Id", "Nome", "CargaHoraria", "CriadoPor", "CriadoEm", "EditadoPor", "EditadoEm"
	];

	public override string ToString()
	{
		return
			$"| {Id,-16} | {Nome,-16} | {CargaHoraria.ToString() + 'h',-16} | {CriadoPor,-16} | {CriadoEm,-16} | {EditadoPor,-16} | {EditadoEm,-16} |";
	}
}

public enum Turno
{
	Matutino,
	Vespertino,
	Noturno
}
