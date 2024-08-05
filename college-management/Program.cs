using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;

BaseDeDados baseDeDados = new();
Ambiente ambiente = new();

await Seed.IniciarBaseDeDados(baseDeDados, ambiente.variaveis);

_ = bool.TryParse(args[0], out var modoDesenvolvimento);

var usuarioLogado = MiddlewareAutenticacao.Login(modoDesenvolvimento, baseDeDados.usuarios);

MiddlewareContexto.Inicializar(usuarioLogado);

public enum EstadoDoApp
{
    Sair, Login, Contexto
}
