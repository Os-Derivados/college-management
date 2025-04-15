using System.Globalization;
using college_management.Utilitarios.Atributos;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	private static double _contagemId = 10000000000;

	public Curso(string nome, Materia[] gradeCurricular)
	{
		Nome            = nome;
		GradeCurricular = gradeCurricular;

		Id = _contagemId.ToString(CultureInfo.InvariantCulture);
		_contagemId++;
	}

	public string?       Nome            { get; set; }
	[PropriedadeModelo(TipoPropriedade.Quantidade, "Matéria(s)")]
	public Materia[]     GradeCurricular { get; set; }
	[PropriedadeModelo(TipoPropriedade.Quantidade, "Matrícula(s)")]
	public List<string>? MatriculasIds   { get; set; }

	public double ObterCargaHoraria()
	{
		return GradeCurricular.Sum(materia
			                           => materia.CargaHoraria);
	}
}
