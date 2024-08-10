namespace college_management.Constantes;

public static class OperacoesDeContexto
{
    public const string AcessarGradeHoraria = "Acessar Grade Horária";
    public const string AcessarGradeCurricular = "Acessar Grade Curricular";
    public const string AcessarNotas = "Acessar Notas";
    public const string AcessarFinanceiro = "Acessar Financeiro";
    public const string AcessarMatricula = "Acessar Matrícula";
    public const string AcessarCursos = "Acessar Cursos";
    public const string AcessarCargos = "Acessar Cargos";
    public const string AcessarMaterias = "Acessar Matérias";
    public const string AcessarUsuarios = "Acessar Usuários";

    public static readonly string[] AcessoAlunos =
    [
        AcessarGradeHoraria,
        AcessarGradeCurricular,
        AcessarNotas,
        AcessarFinanceiro,
        AcessarMatricula
    ];

    public static readonly string[] AcessoGestoresAdministradores =
    [
        ..AcessoAlunos,
        AcessarCursos,
        AcessarCargos,
        AcessarMaterias,
        AcessarUsuarios
    ];
}
