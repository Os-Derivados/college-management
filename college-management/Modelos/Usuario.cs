using System.Globalization;

namespace college_management.Modelos;

public sealed class Usuario
{
    private static double _contagemId = 10000000000;
    public readonly string? Id;
    private readonly Cargo? _cargo;
    public string? Nome;
    private string? _senha;

    public Usuario(string nome, string senha, Cargo cargo)
    {
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Nome = nome;
        _senha = senha;
        _cargo = cargo;
        
        _contagemId++;
    }

    public bool Login(string senha)
    {
        return _senha.Equals(senha);
    }
}