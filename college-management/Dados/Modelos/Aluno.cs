namespace college_management.Dados.Modelos;


public sealed class Aluno : Usuario
{
	public Aluno(string    login,
	             string    nome,
	             string    cargoId,
	             string    senha,
	             string matriculaId) 
		: base(login, nome, cargoId, senha)
	{
		MatriculaId = matriculaId;
	}

	public string MatriculaId { get; set; }
}
