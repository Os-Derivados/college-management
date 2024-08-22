namespace college_management.Constantes;

public static class OperacoesDeContexto
{
    public const string AcessarCursos = "Acessar Cursos";
    public const string AcessarCargos = "Acessar Cargos";
    public const string AcessarMaterias = "Acessar Matérias";
    public const string AcessarUsuarios = "Acessar Contas";

    public static readonly string[] AcessoAlunos =
    [
        AcessarCursos,
        AcessarMaterias,
        AcessarUsuarios
    ];

    public static readonly string[] AcessoGestoresAdministradores =
    [
        ..AcessoAlunos,
        AcessarCargos
    ];
}
