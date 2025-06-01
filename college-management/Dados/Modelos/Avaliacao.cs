using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace college_management.Dados.Modelos;


public class Avaliacao : IRastreavel
{
	[MinLength(0)]
	[MaxLength(10)]
	public float? P1 { get; set; }

	[MinLength(0)]
	[MaxLength(10)]
	public float? P2 { get; set; }

	[MinLength(0)]
	[MaxLength(10)]
	public float? P3 { get; set; }

	[DefaultValue(StatusAvaliacao.EmAndamento)]
	public StatusAvaliacao Status { get; set; }

	public uint   AlunoId { get; set; }
	public virtual Aluno? Aluno   { get; set; }

	public uint     MateriaId { get; set; }
	public virtual Materia? Materia   { get; set; }


	public string? CriadoPor  { get; set; }
	public string? EditadoPor { get; set; }
	public DateTime? CriadoEm { get; set; }
	public DateTime? EditadoEm { get; set; }
}

public enum StatusAvaliacao
{
	EmAndamento,
	Aprovado,
	Reprovado
}
