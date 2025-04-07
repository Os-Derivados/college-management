using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	public Curso(string nome) : base(nome) { }

	public ICollection<Aluno>   Alunos   { get; }      = [];
	public ICollection<Materia> Materias { get; set; } = [];

	public override string ToString()
	{
		return $"| {Nome,-16} " + $"| {Id,-16} |";
	}
}
