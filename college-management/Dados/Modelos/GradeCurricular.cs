using college_management.Dados.Repositorios;


namespace college_management.Dados.Modelos;


public class GradeCurricular : Modelo
{
	public GradeCurricular(string nome) : base(nome) { }

	public uint CursoId   { get; set; }
	public uint MateriaId { get; set; }

	public uint CargaHoraria(RepositorioMaterias materias)
	{
		var modelo = materias.ObterTodos().Modelo;

		return (uint)(modelo?.Sum(m => m.CargaHoraria) ?? 0);
	}
}
