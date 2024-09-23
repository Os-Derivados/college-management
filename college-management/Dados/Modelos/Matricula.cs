namespace college_management.Dados.Modelos;


public sealed class Matricula : Modelo
{
	public Matricula(int        periodo,
	                 Modalidade modalidade)
	{
		Periodo    = periodo;
		Modalidade = modalidade;
		Id         = _contagemId.ToString();

		_contagemId++;
	}

	private static long _contagemId = 10000000000;

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
}

public enum Modalidade
{
	Presencial,
	Ead,
	Hibrido,
	Invalido
}
