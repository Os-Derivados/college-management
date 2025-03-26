namespace college_management.Views.Interfaces;

/// <summary>
/// Usado em Views que possuem a possibilidade de serem paginadas por <see cref="PaginaView"/>.
/// </summary>
public interface IPaginavel
{
    /// <summary>
    /// Constrói uma série de páginas que serão utilizadas por <see cref="PaginaView"/>.
    /// </summary>
    /// <param name="linhasMaximas">
    ///     Parâmetro arbitrário utilizado para determinar o tamanho máximo de cada página,
    ///     especificado em linhas no console. Geralmente corresponde à metade da altura do console. 
    /// </param>
    public string[] ConstruirPaginas(int linhasMaximas);
}