namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login, string nome, CredenciaisUsuario credenciais) :
		base(login, nome, credenciais)
	{
	}

	public ICollection<Materia> Materias { get; } = [];
	public ICollection<Curso>   Cursos   { get; } = [];
}
