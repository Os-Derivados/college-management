using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


public class GradeCurricular : Rastreavel
{
	public uint CursoId   { get; set; }
	public uint MateriaId { get; set; }
}
