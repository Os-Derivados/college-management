using System.ComponentModel;


namespace college_management.Dados.Modelos;


public class Gestor : Usuario
{
	public Gestor(string login, string nome) : base(login, nome) { }

	[DefaultValue(Cargo.Operador)]
	public Cargo Cargo { get; set; }
}

public enum Cargo
{
	Operador,
	Administrador
}
