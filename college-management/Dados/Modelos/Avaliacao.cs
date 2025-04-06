namespace college_management.Dados.Modelos;


public class Avaliacao : Modelo
{
	public Avaliacao(string nome) : base(nome) { }

	public float           P1        { get; set; }
	public float           P2        { get; set; }
	public float           P3        { get; set; }
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
