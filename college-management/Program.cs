using college_management.Dados;
using college_management.Middlewares;
using college_management.Utilitarios;


UtilitarioArquivos.Inicializar();

BaseDeDados baseDeDados = new();

bool seed = false;

try
{
	if (!bool.TryParse(args[1], out seed))
	{
		throw new Exception();
	}
}
catch (Exception)
{
	Console.WriteLine("Aviso : Argumento seed não informado ou incorreto, utilizando o valor padrão : false");
}

if (seed)
	await UtilitarioSeed.IniciarBaseDeDados(baseDeDados);

bool modoDesenvolvimento = false;

try
{
	if (!bool.TryParse(args[0], out modoDesenvolvimento))
	{
		throw new Exception();
	}
}
catch (Exception)
{
	Console.WriteLine("Aviso : Argumento modoDesenvolvimento não informado ou incorreto, utilizando o valor padrão : false");
}

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
