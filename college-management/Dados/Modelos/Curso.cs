using System.Globalization;
using System.Text;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	public Curso(string nome, Materia[] gradeCurricular)
	{
		Nome = nome;
		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		GradeCurricular = gradeCurricular;

		_contagemId++;
	}

	private static double _contagemId = 10000000000;

	public string?   Nome            { get; set; }
	public Materia[] GradeCurricular { get; set; }

	public override string ToString()
	{
		return
			$"| {Nome,-16} | {GradeCurricular.Length + " Matéria(s)",-16} | {Id,-16} |";
	}

	public double CalcularCargaHoraria()
	{
		return GradeCurricular.Sum(materia
			                           => materia.CargaHoraria);
	}
}
