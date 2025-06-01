using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;

namespace college_management.Dados.Repositorios;

public class RepositorioTurmas : Repositorio<Turma>, IRepositorioTurmas
{
	public RepositorioTurmas(BancoDeDados bancoDeDados) : base(bancoDeDados)
	{
	}

	public override bool Existe(Turma modelo)
	{
		throw new NotImplementedException();
	}
}
