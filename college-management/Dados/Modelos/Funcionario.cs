namespace college_management.Dados.Modelos;

public sealed class Funcionario : Usuario
{
    public Funcionario(string login,
                       string nome,
                       Cargo cargo,
                       string senha) : base(login, nome, cargo, senha)
    {
    }
}
