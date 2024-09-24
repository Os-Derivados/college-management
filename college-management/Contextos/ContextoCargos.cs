using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Contextos;


public class ContextoCargos : Contexto<Cargo>
{
	public ContextoCargos(BaseDeDados baseDeDados,
	                      Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

	public override async Task Cadastrar() { throw new NotImplementedException(); }

	public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir() { throw new NotImplementedException(); }

	public override void Visualizar()  { throw new NotImplementedException(); }
	public override void VerDetalhes() { throw new NotImplementedException(); }
}
