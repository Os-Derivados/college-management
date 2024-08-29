using System.Globalization;

namespace college_management.Dados.Modelos;

public sealed class Cargo : Modelo
{
    public Cargo(string nome, string[] permissoes)
    {
        Nome = nome;
        Permissoes = permissoes;

        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        _contagemId++;
    }

    private static double _contagemId = 10000000000;

    public string? Nome { get; set; }
    public string[]? Permissoes { get; set; }

    public bool TemPermissao(string permissao)
    {
        return Permissoes.Any(p => p == permissao);
    }
}
