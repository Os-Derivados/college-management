namespace college_management.Servicos.Interfaces;

public interface IServicoRelatorios<T>
{
    public string GerarRelatorio(T modelo);

    public Task ExportarRelatorio(string relatorio);
}
