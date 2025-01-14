namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login,
	             string nome,
	             CredenciaisUsuario credenciais,
	             string cargoId,
	             string matriculaId)
		: base(login,
		       nome,
		       credenciais,
		       cargoId)
	{
		MatriculaId = matriculaId;
	}

	public string MatriculaId { get; set; }
}
