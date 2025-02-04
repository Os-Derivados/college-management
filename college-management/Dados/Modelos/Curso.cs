using System.Globalization;


namespace college_management.Dados.Modelos;


public class Curso : Modelo
{
	private static ulong _contagemId = 10000000000;

	public Curso(string nome)
	{
		Nome = nome;

		Id = _contagemId++;
	}

	public string? Nome { get; set; }

	public override string ToString() { return $"| {Nome,-16} | {Id,-16} |"; }
}
