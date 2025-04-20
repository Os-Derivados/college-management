namespace college_management.Dados.Modelos;


public class Docente : Usuario
{
	public Docente(string login, string nome) :
			base(login, nome) { }

	public ICollection<Turma>   Turmas   { get; } = [];
	public ICollection<Materia> Materias { get; } = [];
}
