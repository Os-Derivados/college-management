namespace college_management.Dados.Modelos;


public class Docente : Usuario
{
	public Docente(string login, string nome, CredenciaisUsuario credenciais) :
		base(login, nome, credenciais)
	{
	}

	public ICollection<Turma>   Turmas   { get; } = [];
	public ICollection<Materia> Materias { get; } = [];
}
