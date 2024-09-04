namespace college_management.Constantes;

public static class AcessosContexto
{
    public const string AcessoContextoCursos = "Cursos";
    public const string AcessoContextoCargos = "Cargos";
    public const string AcessoContextoMaterias = "Matérias";
    public const string AcessoContextoUsuarios = "Contas";

    public static readonly string[] AcessoAlunos =
    [
        AcessoContextoCursos,
        AcessoContextoMaterias,
        AcessoContextoUsuarios
    ];

    public static readonly string[] AcessoGestoresAdministradores =
    [
        ..AcessoAlunos,
        AcessoContextoCargos
    ];
}
