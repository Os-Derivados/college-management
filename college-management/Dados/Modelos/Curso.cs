using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	public Curso(string nome) : base(nome) { }

	public IEnumerable<Aluno>   Alunos   { get; } = [];
	public IEnumerable<Materia> Materias { get; } = [];

	public override string ToString()
	{
		return $"| {Nome,-16} " + $"| {Id,-16} |";
	}
}
