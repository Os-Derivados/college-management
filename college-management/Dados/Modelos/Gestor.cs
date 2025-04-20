namespace college_management.Dados.Modelos;


public class Gestor : Usuario
{
	public Gestor(string login, string nome, CredenciaisUsuario credenciais) :
			base(login, nome) { }

	public Gestor(string login, string nome) :
			base(login, nome) { }

	public Cargo               Cargo   { get; set; }
	public ICollection<Modelo> Modelos { get; } = [];
}

public enum Cargo { Operador, Administrador }
