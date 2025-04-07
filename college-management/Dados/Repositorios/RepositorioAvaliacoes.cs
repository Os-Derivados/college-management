using college_management.Dados.Contexto;
using college_management.Dados.Modelos;


namespace college_management.Dados.Repositorios;


public class RepositorioAvaliacoes : Repositorio<Avaliacao>
{
	public RepositorioAvaliacoes(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Avaliacao modelo)
	{
		var avaliacao = Dados.FirstOrDefault(
			a => a.AlunoId == modelo.AlunoId
			     && a.MateriaId == modelo.MateriaId);

		return avaliacao is not null;
	}
}
