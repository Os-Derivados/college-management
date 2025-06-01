using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace college_management.Dados.Modelos;

[Table("Cursos")]
[Index(nameof(Nome), IsUnique = true)]
public class Curso : Modelo
{
	public Curso(string nome) : base(nome) { }

	public virtual ICollection<Aluno> Alunos { get; set; } = [];
	public virtual ICollection<Materia> Materias { get; set; } = [];
	public virtual ICollection<Matricula> Matriculas { get; set; } = [];
	public virtual ICollection<GradeCurricular> GradesCurriculares { get; set; } = [];

	[NotMapped]
	public uint CargaHoraria => (uint)Materias.Sum(m => m.CargaHoraria);

	[NotMapped]
	protected override string[] CamposRelatorio => [
		"Id", "Nome", "CargaHoraria", "CriadoPor", "CriadoEm", "EditadoPor", "EditadoEm"
	];

	public override string ToString()
	{
		return $"| {Id,-16} | {Nome,-16} | {CargaHoraria + "h",-16} | {CriadoPor,-16} | {CriadoEm,-16} | {EditadoPor,-16} | {EditadoEm,-16} |";
	}
}
