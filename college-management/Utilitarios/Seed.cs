using college_management.Constantes;
using college_management.Dados;
using college_management.Modelos;

namespace college_management.Utilitarios;

public static class Seed
{
    public static async Task IniciarBaseDeDados(BaseDeDados baseDeDados, Dictionary<string, string> variaveisDeAmbiente)
    {
        variaveisDeAmbiente.TryGetValue("ADMIN_USER", out var nomeDefault);
        variaveisDeAmbiente.TryGetValue("ADMIN_LOGIN", out var loginDefault);
        variaveisDeAmbiente.TryGetValue("ADMIN_PASSWORD", out var senhaDefault);
        
        var usuarioDefault = baseDeDados.usuarios.ObterPorId("10000000000");

        if (usuarioDefault is not null) 
            return;
        
        await baseDeDados.cargos.Adicionar(new Cargo(CargosDeAcesso.CargoAdministradores));
        var cargoDefault = baseDeDados.cargos.ObterPorId("10000000000");
        
        await baseDeDados.usuarios.Adicionar(new Usuario(loginDefault, nomeDefault, cargoDefault, senhaDefault));
    }
}