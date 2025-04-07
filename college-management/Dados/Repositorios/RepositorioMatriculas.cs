using college_management.Dados.Contexto;
using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioMatriculas : Repositorio<Matricula>
{
	public RepositorioMatriculas(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Matricula modelo)
	{
		var matricula = Dados.FirstOrDefault(
			m => m.AlunoId == modelo.AlunoId && m.CursoId == modelo.CursoId);
		
		return matricula is not null;
	}
}
