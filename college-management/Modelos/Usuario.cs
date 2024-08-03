using System.Globalization;

namespace college_management.Modelos;

public sealed class Usuario : Modelo
{
    public Usuario(string login, string nome, Cargo cargo, string senha)
    {
        Login = login;
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Cargo = cargo;
        _senha = senha;

        _contagemId++;
    }

    private static double _contagemId = 10000000000;
    
    public string? Login;
    public string? Nome { get; set; }
    public override string? Id { get; set; }
    public Cargo? Cargo { get; set; }
    public string? _senha { get; set; }

    public static bool Autenticar(Usuario usuario, string nomeUsuario, string senha)
    {
        return (usuario.Login == nomeUsuario) && (usuario._senha == senha);
    }

    public override string ToString()
    {
        return $"{Id} - {Login} - {Nome} - {Cargo.Nome}";
    }
}