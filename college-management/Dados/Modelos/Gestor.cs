using System.ComponentModel;


namespace college_management.Dados.Modelos;


public class Gestor : Usuario
{
	public Gestor(string login, string nome) : base(login, nome) { }

	[DefaultValue(Cargo.Operador)]
	public Cargo Cargo { get; set; }

	public ICollection<Modelo> Modelos { get; } = [];
	public ICollection<CorpoDocente> CorposDocentes { get; } = [];
	public ICollection<Matricula> Matriculas { get; } = [];
	public ICollection<Avaliacao> Avaliacoes { get; } = [];
	public ICollection<GradeCurricular> Grades { get; } = [];
}

public enum Cargo
{
	Operador,
	Administrador
}
