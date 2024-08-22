namespace college_management.Dados.Modelos;

public sealed class Aluno : Usuario
{
    public Aluno(string login,
                 string nome,
                 Cargo cargo,
                 string senha,
                 Matricula matricula) : base(login,
                                             nome,
                                             cargo,
                                             senha)
    {
        Matricula = matricula;
    }

    public Matricula Matricula { get; set; }
}
