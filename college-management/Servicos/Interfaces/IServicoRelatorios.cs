using college_management.Dados.Modelos;


namespace college_management.Servicos.Interfaces;


public interface IServicoRelatorios<T>
{
	public string GerarRelatorio(Cargo cargoUsuario);

	public string       GerarEntradasRelatorio();
	public Task<string> ExportarRelatorio(string relatorio);
}
