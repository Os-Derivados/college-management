using college_management.Constantes;

namespace college_management.Modelos;

public sealed class Cargo
{
    public readonly string? Nome;
    private string[]? Permissoes { get; set; }

    public Cargo(string nome)
    {
        Nome = nome;

        InicializarPermissoes(nome);
    }

    private void InicializarPermissoes(string cargo)
    {
        Permissoes = cargo switch
        {
            CargosDeAcesso.CargoAlunos => 
            [
                PermissoesDeAcesso.PermissaoAcessarCursos
            ],
            CargosDeAcesso.CargoGestores =>
            [
                PermissoesDeAcesso.PermissaoGerenciarMatriculas,
                PermissoesDeAcesso.PermissaoGerenciarCursos,
                PermissoesDeAcesso.PermissaoGerenciarAlunos
            ],
            CargosDeAcesso.CargoAdministradores =>
            [
                PermissoesDeAcesso.PermissaoGerenciarMatriculas,
                PermissoesDeAcesso.PermissaoGerenciarCursos,
                PermissoesDeAcesso.PermissaoGerenciarAlunos,
                PermissoesDeAcesso.PermissaoGerenciarGestores,
                PermissoesDeAcesso.PermissaoGerenciarAdministradores
            ],
            _ => throw new ArgumentOutOfRangeException(
                nameof(cargo),
                "Não é possível atribuir permissões sem especificar um cargo válido"
            )
        };
    }

    public static bool VerificarPermissao(Cargo cargo, string permissao)
    {
        return cargo.Permissoes.Any(p => p == permissao);
    }
}