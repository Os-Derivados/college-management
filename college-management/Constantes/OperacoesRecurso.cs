namespace college_management.Constantes;

public static class OperacoesRecurso
{
    private static readonly string[] OperacoesComuns =
    [
        "Cadastrar",
        "Visualizar",
        "Editar",
        "Excluir"
    ];

    public static readonly string[] OperacoesUsuarios =
    [
        ..OperacoesComuns,
        "Editar Matricula",
        "Ver Matricula",
        "Ver Boletim",
        "Ver Financeiro"
    ];

    public static readonly string[] OperacoesCursos =
    [
        ..OperacoesComuns,
        "Ver Grade Horaria",
        "Ver Grade Curricular"
    ];

    public static readonly string[] OperacoesMaterias =
    [
        ..OperacoesComuns
    ];

    public static readonly string[] OperacoesCargos =
    [
        ..OperacoesComuns
    ];
}
