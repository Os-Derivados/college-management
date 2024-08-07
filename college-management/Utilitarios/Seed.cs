using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Utilitarios;

public static class Seed
{
    public static async Task IniciarBaseDeDados(
        BaseDeDados baseDeDados,
        Dictionary<string, string> variaveisDeAmbiente)
    {
        variaveisDeAmbiente.TryGetValue(
            VariaveisDeAmbiente.MasterAdminNome,
            out var nomeDefault);

        variaveisDeAmbiente.TryGetValue(
            VariaveisDeAmbiente.MasterAdminLogin,
            out var loginDefault);

        variaveisDeAmbiente.TryGetValue(
            VariaveisDeAmbiente.MasterAdminSenha,
            out var senhaDefault);

        var usuarioDefault =
            baseDeDados.usuarios.ObterPorId(VariaveisDeAmbiente
                                                .MasterAdminId);

        if (usuarioDefault is not null)
            return;

        await baseDeDados.cargos.Adicionar(
            new Cargo(CargosDeAcesso.CargoAdministradores));
        var cargoDefault =
            baseDeDados.cargos.ObterPorId("10000000000");

        await baseDeDados.usuarios.Adicionar(
            new Funcionario(loginDefault, nomeDefault, cargoDefault,
                            senhaDefault));
    }
}
