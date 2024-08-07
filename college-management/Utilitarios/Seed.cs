using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Utilitarios;

public static class Seed
{
    public static async Task IniciarBaseDeDados(BaseDeDados baseDeDados)
    {
        var usuarioDefault =
            baseDeDados.usuarios.ObterPorLogin(VariaveisDeAmbiente
                                                   .MasterAdminLogin);

        if (usuarioDefault is not null)
            return;

        var (nomeMestre, loginMestre, senhaMestre)
            = ObterCredenciais(VariaveisDeAmbiente.MasterAdminNome,
                               VariaveisDeAmbiente.MasterAdminLogin,
                               VariaveisDeAmbiente.MasterAdminSenha);

        await baseDeDados.cargos.Adicionar(
            new Cargo(CargosDeAcesso.CargoAdministradores));
        var cargoDefault =
            baseDeDados.cargos.ObterPorId("10000000000");

        await baseDeDados.usuarios.Adicionar(
            new Funcionario(loginMestre,
                            nomeMestre,
                            cargoDefault,
                            senhaMestre));
    }

    private static (string, string, string) ObterCredenciais(
        string nome,
        string login,
        string senha)
    {
        Ambiente.Variaveis
                .TryGetValue(nome, out var nomeDefault);
        Ambiente.Variaveis
                .TryGetValue(login, out var loginDefault);
        Ambiente.Variaveis
                .TryGetValue(senha, out var senhaDefault);

        return (nomeDefault, loginDefault, senhaDefault);
    }
}
