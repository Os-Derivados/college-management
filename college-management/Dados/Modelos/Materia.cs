namespace college_management.Dados.Modelos;


public sealed class Materia : Modelo
{
	public Materia(string nome) : base(nome) { }

	public uint               CargaHoraria { get; set; }
	public IEnumerable<Aluno> Alunos       { get; } = [];
	public IEnumerable<Curso> Cursos       { get; } = [];
	public IEnumerable<Docente> Docentes       { get; } = [];

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
