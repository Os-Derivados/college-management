namespace college_management.Servicos.Interfaces;


public interface IServicoDados<T>
{
	public Task SalvarAssicrono(List<T>? items);

	public Task<List<T>?> CarregarAssincrono();
}
