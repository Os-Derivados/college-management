namespace college_management.Utilitarios.Atributos;

public enum TipoPropriedade
{
    Valor,
    /// <summary>
    /// Deve ser utilizada apenas em propriedades que tem como base <see cref="IEnumerable{T}"/>.
    /// </summary>
    Colecao,
    Privada,
    /// <summary>
    /// Deve ser utilizada apenas em propriedades que tem como base <see cref="IEnumerable{T}"/>.
    /// </summary>
    Quantidade
}

public class PropriedadeModeloAttribute(TipoPropriedade tipo, string? identificador = null) : Attribute
{
    public readonly TipoPropriedade Tipo = tipo;
    public readonly string Identificador = identificador ?? string.Empty;
}