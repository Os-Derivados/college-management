namespace college_management.Constantes;

public static class OperacoesRecursos
{
    private static readonly string[] OperacoesComuns =
    [
        "Cadastrar",
        "Visualizar",
        "Editar",
        "Excluir"
    ];

    public static readonly string[] RecursoUsuarios =
    [
        ..OperacoesComuns,
        "Editar Matricula",
        "Ver Matricula",
        "Ver Boletim",
        "Ver Financeiro"
    ];

    public static readonly string[] RecursoCursos =
    [
        ..OperacoesComuns,
        "Ver Grade Horaria",
        "Ver Grade Curricular"
    ];

    public static readonly string[] RecursoMaterias =
    [
        ..OperacoesComuns
    ];

    public static readonly string[] RecursoCargos =
    [
        ..OperacoesComuns
    ];
}
