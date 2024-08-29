using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;

BaseDeDados baseDeDados = new();

await UtilitarioSeed.IniciarBaseDeDados(baseDeDados);

_ = bool.TryParse(args[0], out var modoDesenvolvimento);

var usuarioLogado =
    MiddlewareAutenticacao.Autenticar(modoDesenvolvimento,
                                      baseDeDados.usuarios);

MiddlewareContexto.Inicializar(baseDeDados, usuarioLogado);

public enum EstadoDoApp
{
    Sair,
    Login,
    Contexto,
    Recurso
}
