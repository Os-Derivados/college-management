namespace college_management.Dados.Modelos;


public class CorpoDocente : IRastreavel
{
	public uint MateriaId { get; set; }
	public virtual Materia? Materia { get; set; }
	public uint DocenteId { get; set; }
	public virtual Docente? Docente { get; set; }
	public string? CriadoPor { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }
}
