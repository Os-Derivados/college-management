namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string login,
	             string nome,
	             string senha,
	             string cargoId,
	             string matriculaId)
		: base(login,
		       nome,
		       senha,
		       cargoId)
	{
		MatriculaId = matriculaId;
	}

	public string MatriculaId { get; set; }
}
