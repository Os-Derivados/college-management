namespace college_management.Utilitarios.Atributos;

public enum TipoPropriedade
{
    Valor,
    Colecao,
    Privada,
    Quantidade
}

public class PropriedadeModeloAttribute(TipoPropriedade tipo, string? identificador = null) : Attribute
{
    public readonly TipoPropriedade Tipo = tipo;
    public readonly string Identificador = identificador ?? string.Empty;
}