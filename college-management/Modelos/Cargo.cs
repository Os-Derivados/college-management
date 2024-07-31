using college_management.Constantes;

namespace college_management.Modelos;

public sealed class Cargo
{
    public string? Nome;
    private string[]? _permissoes;

    public Cargo(string nome, string cargo)
    {
        Nome = nome;

        InicializarPermissoes(cargo);
    }

    private void InicializarPermissoes(string cargo)
    {
        _permissoes = cargo switch
        {
            CargosDeAcesso.CargoAlunos => [PermissoesDeAcesso.PermissaoAcessarCursos],
            CargosDeAcesso.CargoGestores =>
            [
                PermissoesDeAcesso.PermissaoGerenciarMatriculas,
                PermissoesDeAcesso.PermissaoGerenciarCursos,
                PermissoesDeAcesso.PermissaoCadastrarAlunos
            ],
            CargosDeAcesso.CargoAdministradores =>
            [
                PermissoesDeAcesso.PermissaoGerenciarMatriculas,
                PermissoesDeAcesso.PermissaoGerenciarCursos,
                PermissoesDeAcesso.PermissaoCadastrarAlunos,
                PermissoesDeAcesso.PermissaoCadastrarGestores,
                PermissoesDeAcesso.PermissaoCadastrarAdministradores
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