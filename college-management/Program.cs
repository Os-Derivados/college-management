using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;

BaseDeDados baseDeDados = new();
Ambiente ambiente = new();

await Seed.IniciarBaseDeDados(baseDeDados, ambiente.variaveis);

var parametroValido = bool.TryParse(args[0], out var modoDesenvolvimento);

var usuarioLogado = Autenticacao.Login(modoDesenvolvimento, baseDeDados.usuarios);

Contexto.Inicializar(usuarioLogado);

public enum EstadoDoApp
{
    Sair, Login, Contexto
}
