using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;


namespace college_management.Contextos;

public class ContextoTurmas : Contexto<Turma>, IContextoTurmas
{
	public ContextoTurmas(BaseDeDados baseDeDados, Usuario usuarioContexto) : base(baseDeDados, usuarioContexto)
	{
	}

	public override Task Cadastrar()
	{
		throw new NotImplementedException();
	}

	public override Task Editar()
	{
		throw new NotImplementedException();
	}

	public override Task Excluir()
	{
		throw new NotImplementedException();
	}

	public override void VerDetalhes()
	{
		throw new NotImplementedException();
	}

	public override void Visualizar()
	{
		throw new NotImplementedException();
	}
}
