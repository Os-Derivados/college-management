using System.Globalization;
using System.Text.Json.Serialization;

namespace college_management.Dados.Modelos;

[JsonDerivedType(typeof(Usuario), typeDiscriminator: "base")]
[JsonDerivedType(typeof(Aluno), typeDiscriminator: "aluno")]
[JsonDerivedType(typeof(Funcionario), typeDiscriminator: "funcionario")]
public class Usuario : Modelo
{
    public Usuario(string login,
                   string nome,
                   Cargo cargo,
                   string senha)
    {
        Login = login;
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);
        Cargo = cargo;
        Senha = senha;

        _contagemId++;
    }

    private static long _contagemId = 10000000000;

    public string? Login { get; set; }
    public string? Nome { get; set; }
    public Cargo? Cargo { get; set; }
    public string? Senha { get; set; }

    public static bool Autenticar(Usuario usuario,
                                  string nomeUsuario,
                                  string senha)
    {
        return usuario.Login == nomeUsuario && usuario.Senha == senha;
    }

    public override string ToString()
    {
        return $"{Id} - {Login} - {Nome} - {Cargo.Nome}";
    }
}
