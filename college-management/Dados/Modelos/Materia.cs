namespace college_management.Dados.Modelos;


public sealed class Materia : Modelo
{
	public Materia(string nome) : base(nome) { }

	public uint CargaHoraria { get; set; }

	public ICollection<Turma>           Turmas             { get; set; } = [];
	public ICollection<Aluno>           Alunos             { get; set; } = [];
	public ICollection<Curso>           Cursos             { get; set; } = [];
	public ICollection<Docente>         Docentes           { get; set; } = [];
	public ICollection<Avaliacao>       Avaliacoes         { get; set; } = [];
	public ICollection<GradeCurricular> GradesCurriculares { get; set; } = [];


	public override string ToString()
	{
		return
			$"| {Nome,-16} | {CargaHoraria.ToString() + 'h',-16} | {Id,-16} |";
	}
}

public enum Turno
{
	Matutino,
	Vespertino,
	Noturno
}
