namespace college_management.Dados.Modelos;


public sealed class Materia : Modelo
{
	public Materia(string nome) : base(nome) { }

	public uint CargaHoraria { get; set; }

	public ICollection<Turma>     Turmas     { get; }      = [];
	public ICollection<Aluno>     Alunos     { get; }      = [];
	public ICollection<Curso>     Cursos     { get; }      = [];
	public ICollection<Docente>   Docentes   { get; }      = [];
	public ICollection<Avaliacao> Avaliacoes { get; set; } = [];


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
