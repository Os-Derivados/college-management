using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


public class GradeCurricular : IRastreavel
{
	public uint     CursoId   { get; set; }
	public Curso?   Curso     { get; set; }
	public uint     MateriaId { get; set; }
	public Materia? Materia   { get; set; }

	public string? CriadoPor  { get; set; }
	public string? EditadoPor { get; set; }
}
