using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	public Curso(string nome) : base(nome) { }

	public ICollection<Aluno>           Alunos             { get; set; } = [];
	public ICollection<Materia>         Materias           { get; set; } = [];
	public ICollection<Matricula>       Matriculas         { get; set; } = [];
	public ICollection<GradeCurricular> GradesCurriculares { get; set; } = [];

	[NotMapped]
	public uint CargaHoraria => (uint)Materias.Sum(m => m.CargaHoraria);

	public override string ToString()
	{
		return $"| {Nome,-1} | {Id,-16} | {CargaHoraria,-16}h | {CriadoPor,-16} | {CriadoEm,-16} | {EditadoPor,-16} | {EditadoEm,-16} |";
	}
}
