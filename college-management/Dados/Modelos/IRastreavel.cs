namespace college_management.Dados.Modelos;


public interface IRastreavel
{
	public string? CriadoPor  { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }
}
