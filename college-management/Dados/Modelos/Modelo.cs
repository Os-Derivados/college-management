namespace college_management.Dados.Modelos;


public abstract class Modelo
{
	public Guid Id { get; set; } = Guid.NewGuid();
}
