namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login, string nome) : base(login, nome) { }

	public ICollection<Turma>      Turmas      { get; set; } = [];
	public ICollection<Materia>    Materias    { get; set; } = [];
	public ICollection<Curso>      Cursos      { get; set; } = [];
	public ICollection<Matricula>  Matriculas  { get; set; } = [];
	public ICollection<Avaliacao>  Avaliacoes  { get; set; } = [];
	public ICollection<TurmaAluno> TurmaAlunos { get; set; } = [];
}
