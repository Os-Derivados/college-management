using college_management.Constantes;

namespace college_management.Modelos;

public sealed class Cargo
{
    public readonly string? Nome;
    private string[]? _permissoes;

    public Cargo(string nome)
    {
        Nome = nome;

        InicializarPermissoes(nome);
    }

    private void InicializarPermissoes(string cargo)
    {
        _permissoes = cargo switch
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

    public bool VerificarPermissao(string permissao)
    {
        return _permissoes.Any(p => p == permissao);
    }
}