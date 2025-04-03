namespace college_management.Views.Interfaces;

public interface IPaginaView
{
    public ConsoleKeyInfo LerEntrada(bool ignorarEntrada);
    public void AdicionarPagina(string conteudo);
    public void AdicionarPaginas(string[] paginas);
    
    /// <param name="indice">Índice da página. (>0)</param>
    public string ObterPagina(int indice);
}