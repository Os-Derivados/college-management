namespace college_management.Dados.Modelos;


public class TurmaAluno : IRastreavel
{
	public uint    TurmaId  { get; set; }
	public Turma?  Turma    { get; set; }
	public uint    AlunoId  { get; set; }
	public Aluno?  Aluno    { get; set; }
	public uint?   GestorId { get; set; }
	public Gestor? Gestor   { get; set; }
}
