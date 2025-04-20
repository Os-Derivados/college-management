namespace college_management.Dados.Modelos;


public sealed class Matricula
{
	public Matricula(uint periodo, Modalidade modalidade)
	{
		Periodo    = periodo;
		Modalidade = modalidade;
	}

	public uint       CursoId    { get; set; }
	public uint       AlunoId    { get; set; }
	public string     Codigo     => $"{CursoId}{AlunoId}";
	public uint       Periodo    { get; set; }
	public Modalidade Modalidade { get; set; }

	public static Matricula CriarMatricula(Dictionary<string, string> cadastro)
	{
		var periodoValido = uint.TryParse(cadastro["Periodo"],
		                                  out var periodoCurso);

		if (!periodoValido) return null;

		var modalidadeValida = Enum.TryParse<Modalidade>(
			cadastro["Modalidade"],
			out var modalidadeCurso);

		if (!modalidadeValida) return null;

		Matricula novaMatricula = new(periodoCurso, modalidadeCurso);

		return novaMatricula;
	}
}

public enum Modalidade
{
	Presencial,
	Ead,
	Hibrido
}
