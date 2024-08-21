namespace college_management.Dados.Modelos.Interfaces;

public abstract class Modelo : IModelo
{
    public string? Id { get; set; }

    public abstract string ObterNomesPropriedades();
}