using System.Globalization;

namespace college_management.Modelos;

public class Usuario
{
    public Usuario(string nome, Cargo cargo, string senha)
    {
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Cargo = cargo;
        _senha = senha;

        _contagemId++;
    }

    private static double _contagemId = 10000000000;
    
    public readonly string? Nome;
    public readonly string? Id;
    public readonly Cargo? Cargo;
    private string? _senha { get; set; }

    public static bool Autenticar(Usuario usuario, string nomeUsuario, string senha)
    {
        return (usuario.Nome == nomeUsuario) && (usuario._senha == senha);
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} - {Cargo.Nome}";
    }
}