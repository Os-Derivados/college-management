namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login, string nome) : base(login, nome) { }

	public ICollection<Turma>     Turmas     { get; }      = [];
	public ICollection<Materia>   Materias   { get; }      = [];
	public ICollection<Curso>     Cursos     { get; }      = [];
	public ICollection<Matricula> Matriculas { get; set; } = [];
}
