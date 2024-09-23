namespace college_management.Dados.Modelos;


public sealed class Nota
{
	public Nota(string nomeMateria, string materiaId)
	{
		NomeMateria     = nomeMateria;
		MateriaId       = materiaId;
	}

	public string   NomeMateria { get; set; }
	public string MateriaId   { get; set; }
	public float?          P1              { get; set; }
	public float?          P2              { get; set; }
	public float?          P3              { get; set; }
	public double?         MediaFinal      { get; private set; }
	public SituacaoMateria SituacaoMateria { get; set; }

	public void AlterarNota(string tipoNota, float valor)
	{
		if (tipoNota != "P1"
		    && tipoNota != "P2"
		    && tipoNota != "P3")
		{
			Console.WriteLine("Nota inválida");

			return;
		}

		var campoNota =
			GetType()
				.GetProperties()
				.FirstOrDefault(m => m.Name == tipoNota);

		campoNota.SetValue(this, valor);

		CalcularMediaFinal();
	}

	private void CalcularMediaFinal()
	{
		if (P1 is null || P2 is null) return;

		double?[] mediaSubstitutiva =
		[
			P1 * 0.4 + P2 * 0.6,
			P3 * 0.4 + P2 * 0.6,
			P1 * 0.4 + P3 * 0.6
		];

		Array.Sort(mediaSubstitutiva,
		           (a, b) => b.GetValueOrDefault()
		                      .CompareTo(a.GetValueOrDefault()));

		MediaFinal = mediaSubstitutiva[0];

		SituacaoMateria = MediaFinal switch
		{
			>= 5                => SituacaoMateria.Aprovado,
			< 5 when P3 is null => SituacaoMateria.Substituicao,
			_                   => SituacaoMateria.Reprovado
		};
	}
}

public enum SituacaoMateria
{
	Aprovado,
	Reprovado,
	Substituicao
}
