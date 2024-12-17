using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;


UtilitarioArquivos.Inicializar();

BaseDeDados baseDeDados = new();

_ = bool.TryParse(args[1], out var seed);

if (seed)
	await UtilitarioSeed.IniciarBaseDeDados(baseDeDados);

_ = bool.TryParse(args[0], out var modoDesenvolvimento);

var usuarioLogado =
	MiddlewareAutenticacao.Autenticar(modoDesenvolvimento,
	                                  baseDeDados.Usuarios);

MiddlewareContexto.Inicializar(baseDeDados, usuarioLogado);

Console.Clear();
Console.WriteLine("Saindo...");

public enum EstadoDoApp
{
	Sair,
	Login,
	Contexto,
	Recurso
}
