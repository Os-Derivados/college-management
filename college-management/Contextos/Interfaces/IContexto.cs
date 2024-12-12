using college_management.Dados.Modelos;


namespace college_management.Contextos.Interfaces;


public interface IContexto<T> where T : Modelo
{
	public Task Cadastrar();
	public Task Editar();
	public Task Excluir();
	public void Visualizar();
	public void VerDetalhes();

	public void AcessarRecurso(string nomeRecurso);
}
