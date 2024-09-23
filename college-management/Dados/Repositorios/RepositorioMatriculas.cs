using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioMatriculas : Repositorio<Matricula>
{
	public override bool Existe(Matricula modelo)
	{
		return ObterPorId(modelo.Id) is not null;
	}
}
