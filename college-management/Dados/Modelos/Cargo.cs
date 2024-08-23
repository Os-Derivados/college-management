using System.Globalization;
using college_management.Constantes;

namespace college_management.Dados.Modelos;

public sealed class Cargo : Modelo
{
    public Cargo(string nome)
    {
        Nome = nome;
        Id = _contagemId.ToString(CultureInfo.InvariantCulture);

        InicializarPermissoes(nome);

        _contagemId++;
    }

    private static double _contagemId = 10000000000;

    public string? Nome { get; set; }
    public string[]? Permissoes { get; set; }


    private void InicializarPermissoes(string cargo)
    {
        Permissoes = cargo switch
        {
            CargosAcesso.CargoAlunos =>
            [
                PermissoesAcesso.PermissaoAcessarCursos
            ],
            CargosAcesso.CargoGestores =>
            [
                PermissoesAcesso.PermissaoGerenciarMatriculas,
                PermissoesAcesso.PermissaoGerenciarCursos,
                PermissoesAcesso.PermissaoGerenciarAlunos
            ],
            CargosAcesso.CargoAdministradores =>
            [
                PermissoesAcesso.PermissaoGerenciarMatriculas,
                PermissoesAcesso.PermissaoGerenciarCursos,
                PermissoesAcesso.PermissaoGerenciarAlunos,
                PermissoesAcesso.PermissaoGerenciarGestores,
                PermissoesAcesso.PermissaoGerenciarAdministradores
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(cargo),
                                                       "Não é possível atribuir permissões sem especificar um cargo válido")
        };
    }

    public static bool VerificarPermissao(Cargo cargo, string permissao)
    {
        return cargo.Permissoes.Any(p => p == permissao);
    }
}
