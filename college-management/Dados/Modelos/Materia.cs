namespace college_management.Dados.Modelos;


public sealed class Materia : Modelo
{
	public Materia(string nome) : base(nome) { }

	public uint CargaHoraria { get; set; }

	public ICollection<Turma> Turmas { get; set; } = [];
	public ICollection<Aluno> Alunos { get; set; } = [];
	public ICollection<Curso> Cursos { get; set; } = [];
	public ICollection<Docente> Docentes { get; set; } = [];
	public ICollection<Avaliacao> Avaliacoes { get; set; } = [];
	public ICollection<GradeCurricular> GradesCurriculares { get; set; } = [];
	public ICollection<CorpoDocente> CorpoDocente { get; set; } = [];

	protected override string[] CamposRelatorio => [
		"Id", "Nome", "CargaHoraria", "CriadoPor", "CriadoEm", "EditadoPor", "EditadoEm"
	];

	public override string ToString()
	{
		return
			$"| {Id,-16} | {Nome,-16} | {CargaHoraria.ToString() + 'h',-16} | {CriadoPor,-16} | {CriadoEm,-16} | {EditadoPor,-16} | {EditadoEm,-16} |";
	}
}

public enum Turno
{
	Matutino,
	Vespertino,
	Noturno
}
