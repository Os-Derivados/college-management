using college_management.Constantes;
using college_management.Dados.Repositorios;
using college_management.Modelos;
using college_management.Servicos;
using college_management.Utilitarios;

var estadoAtual = EstadoDoApp.Contexto;

var variaveisDeAmbiente = Ambiente.CarregarVariaveis();

variaveisDeAmbiente.TryGetValue("ADMIN_USER", out var nomeDefault);
variaveisDeAmbiente.TryGetValue("ADMIN_LOGIN", out var loginDefault);
variaveisDeAmbiente.TryGetValue("ADMIN_PASSWORD", out var senhaDefault);

Repositorio<Usuario> baseDeUsuarios = new();
Repositorio<Cargo> baseDeCargos = new();

await baseDeCargos.Adicionar(new Cargo(CargosDeAcesso.CargoAdministradores));
var cargoDefault = await baseDeCargos.ObterPorId("10000000000");

await baseDeUsuarios.Adicionar(new Usuario(loginDefault, nomeDefault, cargoDefault, senhaDefault));
var usuarioTeste = await baseDeUsuarios.ObterPorId("10000000000");

/*
do
{
    // Implemente a lógica de realizar login aqui
} while (estadoAtual is EstadoDoApp.Login);
*/

do
{
    Console.WriteLine("Bem-vindo(a). Selecione um dos contextos abaixo.");
    ServicoDePermissoes.ListarContextos(usuarioTeste);

    var opcaoEscolhida = Console.ReadLine();

    var opcaoValida = int.TryParse(opcaoEscolhida, out var resultado);

    if (!opcaoValida) continue;

    if (resultado is 0)
        estadoAtual = EstadoDoApp.Sair;
    
    Console.Clear();
} while (estadoAtual is EstadoDoApp.Contexto);

Console.WriteLine("Saindo...");

public enum EstadoDoApp
{
    Sair, Login, Contexto
}
