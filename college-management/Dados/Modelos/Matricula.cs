namespace college_management.Dados.Modelos;


public sealed class Matricula : Modelo
{
	public Matricula(int periodo,
	                 Modalidade modalidade,
	                 Guid? cursoId = null,
	                 Guid? alunoId = null)
	{
		Periodo    = periodo;
		Modalidade = modalidade;
		CursoId    = cursoId;
		AlunoId    = alunoId;
	}

	public Guid?      CursoId    { get; set; }
	public Guid?      AlunoId    { get; set; }
	public int        Periodo    { get; set; }
	public Modalidade Modalidade { get; set; }
	public List<Nota> Notas      { get; set; } = [];

	public static Matricula? CriarMatricula(
		Dictionary<string, string> cadastroUsuario)
	{
		var conversaoValida = int.TryParse(cadastroUsuario["Periodo"],
		                                   out var periodoCurso);

		if (!conversaoValida) return null;

		var modalidadeCurso =
			cadastroUsuario["Modalidade"] switch
			{
				"Ead"        => Modalidade.Ead,
				"Presencial" => Modalidade.Presencial,
				"Hibrido"    => Modalidade.Hibrido,
				_            => Modalidade.Invalido
			};

		if (modalidadeCurso is Modalidade.Invalido) return null;

		Matricula novaMatricula = new(periodoCurso, modalidadeCurso);

		return novaMatricula;
	}
}

public enum Modalidade
{
	Presencial,
	Ead,
	Hibrido,
	Invalido
}
