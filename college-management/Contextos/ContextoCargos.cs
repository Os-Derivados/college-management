using college_management.Dados;
using college_management.Dados.Modelos;


namespace college_management.Contextos;


public class ContextoCargos : Contexto<Cargo>
{
	public ContextoCargos(BaseDeDados baseDeDados,
	                      Usuario usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto)
	{
	}

	public override async Task Cadastrar()
	{
		throw new NotImplementedException();
	}

	public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir()
	{
		throw new NotImplementedException();
	}

	public override void Visualizar()  { throw new NotImplementedException(); }
	public override void VerDetalhes() { throw new NotImplementedException(); }
}
