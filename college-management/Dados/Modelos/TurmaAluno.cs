namespace college_management.Dados.Modelos;


public class TurmaAluno : IRastreavel
{
	public uint TurmaId { get; set; }
	public Turma? Turma { get; set; }
	public uint AlunoId { get; set; }
	public Aluno? Aluno { get; set; }
	public string? CriadoPor { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }
}
