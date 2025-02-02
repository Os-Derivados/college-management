namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login,
	             string nome,
	             CredenciaisUsuario credenciais,
	             ulong cargoId,
	             ulong matriculaId)
		: base(login, nome, credenciais, cargoId)
	{
		MatriculaId = matriculaId;
	}

	public ulong MatriculaId { get; set; }
}
