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
		return $"| {Nome,-16} " + $"| {Id,-16} |";
	}
}
