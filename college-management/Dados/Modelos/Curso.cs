using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	public Curso(string nome)
	{
		Nome = nome;
	}

	public string? Nome { get; set; }

	public override string ToString() { return $"| {Nome,-16} | {Id,-16} |"; }
}
