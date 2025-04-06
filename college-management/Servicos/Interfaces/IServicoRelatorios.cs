using college_management.Dados.Modelos;


namespace college_management.Servicos.Interfaces;


public interface IServicoRelatorios<T>
{

	public string       GerarEntradasRelatorio();
	public Task<string> ExportarRelatorio(string relatorio);
}
