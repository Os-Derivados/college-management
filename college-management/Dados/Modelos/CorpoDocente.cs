namespace college_management.Dados.Modelos;


public class CorpoDocente : Modelo
{
	public CorpoDocente(string nome) : base(nome) { }

	public uint MateriaId { get; set; }
	public uint DocenteId { get; set; }
}
