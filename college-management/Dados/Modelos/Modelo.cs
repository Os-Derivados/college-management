using college_management.Dados.Modelos.Interfaces;

namespace college_management.Dados.Modelos;

public abstract class Modelo : IModelo
{
    public string? Id { get; set; }

    public abstract string ObterNomesPropriedades();
}