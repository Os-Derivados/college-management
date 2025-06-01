namespace college_management.Dados.Modelos;


public class Docente : Usuario
{
	public Docente(string login, string nome) : base(login, nome) { }

	public virtual ICollection<Turma> Turmas { get; set; } = [];
	public virtual ICollection<Materia> Materias { get; set; } = [];
	public virtual ICollection<CorpoDocente> CorpoDocente { get; set; } = [];
}
