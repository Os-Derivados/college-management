namespace college_management.Dados.Modelos;


public sealed class Matricula : Modelo
{
	private static long _contagemId = 10000000000;

	public Matricula(int periodo,
	                 Modalidade modalidade)
	{
		Periodo    = periodo;
		Modalidade = modalidade;
		Id         = _contagemId.ToString();

		_contagemId++;
	}

	public string?    CursoId    { get; set; }
	public string?    AlunoId    { get; set; }
	public int        Periodo    { get; set; }
	public Modalidade Modalidade { get; set; }
	public List<Nota> Notas      { get; set; } = [];

	public void InicializarNotas(Curso curso)
	{
		if (curso.Id != CursoId) return;

		foreach (var materia in curso.GradeCurricular)
			Notas.Add(new Nota(materia.Nome, materia.Id));
	}
	
	public static Matricula CriarMatricula(Dictionary<string, string> cadastroUsuario)
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
