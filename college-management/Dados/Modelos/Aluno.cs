namespace college_management.Dados.Modelos;


public class Aluno : Usuario
{
	public Aluno(string login, string nome) : base(login, nome) { }

	public virtual ICollection<Turma> Turmas { get; set; } = [];
	public virtual ICollection<Materia> Materias { get; set; } = [];
	public virtual ICollection<Curso> Cursos { get; set; } = [];
	public virtual ICollection<Matricula> Matriculas { get; set; } = [];
	public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = [];
	public virtual ICollection<TurmaAluno> TurmaAlunos { get; set; } = [];
}
