namespace college_management.Dados.Modelos;


public sealed class Matricula : Modelo
{
	private static ulong _contagemId = 10000000000;

	public Matricula(int periodo,
	                 Modalidade modalidade,
	                 ulong? cursoId = null,
	                 ulong? alunoId = null)
	{
		Periodo    = periodo;
		Modalidade = modalidade;
		CursoId    = cursoId;
		AlunoId    = alunoId;
		Id         = _contagemId++;
	}

	public ulong?     CursoId    { get; set; }
	public ulong?     AlunoId    { get; set; }
	public int        Periodo    { get; set; }
	public Modalidade Modalidade { get; set; }
	public List<Nota> Notas      { get; set; } = [];

	public static Matricula CriarMatricula(
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
