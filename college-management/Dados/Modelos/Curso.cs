using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	private static ulong _contagemId = 10000000000;

	public Curso(string nome, Materia[] gradeCurricular)
	{
		Nome            = nome;
		GradeCurricular = gradeCurricular;

		Id = _contagemId;
		_contagemId++;
	}

	public string?       Nome            { get; set; }
	public Materia[]     GradeCurricular { get; set; }

	public override string ToString()
	{
		return
			$"| {Nome,-16} "
			+ $"| {GradeCurricular.Length + " Matéria(s)",-16} "
			+ $"| {Id,-16} |";
	}

	public double ObterCargaHoraria()
	{
		return GradeCurricular.Sum(materia => materia.CargaHoraria);
	}
}
