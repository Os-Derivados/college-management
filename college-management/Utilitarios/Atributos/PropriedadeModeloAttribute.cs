namespace college_management.Utilitarios.Atributos;

public enum TipoPropriedade
{
    Valor,
    Privada,
    Quantidade
}

public class PropriedadeModeloAttribute(TipoPropriedade tipo, string? identificador = null) : Attribute
{
    public TipoPropriedade Tipo = tipo;
    public string Identificador = identificador ?? string.Empty;
}