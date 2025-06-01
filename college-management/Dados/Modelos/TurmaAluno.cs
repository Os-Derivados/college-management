using System.ComponentModel.DataAnnotations.Schema;

namespace college_management.Dados.Modelos;


[Table("TurmasAlunos")]
public class TurmaAluno : IRastreavel
{
	public uint TurmaId { get; set; }
	public virtual Turma? Turma { get; set; }
	public uint AlunoId { get; set; }
	public virtual Aluno? Aluno { get; set; }
	public string? CriadoPor { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }
}
