namespace college_management.Dados.Modelos;


public class Avaliacao
{
	public float?          P1        { get; set; }
	public float?          P2        { get; set; }
	public float?          P3        { get; set; }
	public StatusAvaliacao Status    { get; set; }
	public uint            AlunoId   { get; set; }
	public uint            MateriaId { get; set; }
}

public enum StatusAvaliacao
{
	EmAndamento,
	Aprovado,
	Reprovado
}
