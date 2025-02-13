using System.Globalization;


namespace college_management.Dados.Modelos;


public sealed class Materia : Modelo
{
	private static double _contagemId = 10000000000;

	public Materia(string nome, Turno turno, int cargaHoraria)
	{
		Nome         = nome;
		Id           = _contagemId.ToString(CultureInfo.InvariantCulture);
		Turno        = turno;
		CargaHoraria = cargaHoraria;

		_contagemId++;
	}

	public string? Nome         { get; set; }
	public Turno   Turno        { get; set; }
	public int     CargaHoraria { get; set; }

	public override string ToString()
	{
		return
			$"| {Nome,-16} | {Turno,-16} | {CargaHoraria.ToString() + 'h',-16} | {Id,-16} |";
	}
}

public enum Turno
{
	Matutino,
	Vespertino,
	Noturno,
	Integral
}
