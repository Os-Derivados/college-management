using college_management.Constantes;

namespace college_management.Modelos;

public sealed class Cargo : Modelo
{
    public Cargo(string nome)
    {
        Nome = nome;
        Id = _contagemId.ToString();
        
        InicializarPermissoes(nome);

        _contagemId++;
    }
    
    private static double _contagemId = 10000000000;
    
    public readonly string? Nome;
    public override string? Id { get; set; }
    private string[]? Permissoes { get; set; }


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