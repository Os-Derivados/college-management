using college_management.Dados;
using college_management.Dados.Contexto;
using college_management.Dados.Repositorios;
using college_management.Middlewares;
using college_management.Utilitarios;
using college_management.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

UtilitarioArquivos.Inicializar();

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<BancoDeDados>(options =>
{
	options.UseLazyLoadingProxies()
		.UseSqlite($"Data Source={Path.Combine(UtilitarioArquivos.DiretorioDados, "college_management.db")}")
		.EnableSensitiveDataLogging();

#if DEBUG
	options.EnableSensitiveDataLogging()
		.LogTo(Console.WriteLine, LogLevel.Information);
#endif
});

serviceCollection.AddScoped<RepositorioCursos>();
serviceCollection.AddScoped<RepositorioMaterias>();
serviceCollection.AddScoped<RepositorioUsuarios>();
serviceCollection.AddScoped<BaseDeDados>();

var serviceProvider = serviceCollection.BuildServiceProvider();
await using var bancoDeDados
	= serviceProvider.GetRequiredService<BancoDeDados>();

var baseDeDados = serviceProvider.GetRequiredService<BaseDeDados>();

if (!bool.TryParse(args[1], out var seed))
	View.Aviso(
		"Aviso : Argumento \"seed\" não informado ou incorreto, utilizando o valor padrão : false");

if (seed)
{
	await UtilitarioSeed.IniciarBaseDeDados(bancoDeDados);
}
else if (!await UtilitarioSeed.ValidarDadosIniciais(bancoDeDados))
{
	View.Aviso(
		"Base de Dados não inicializada com valores padrão. Execute o programa novamente com o argumento seed definido como true");
	return;
}

if (!bool.TryParse(args[0], out var modoDesenvolvimento))
	View.Aviso(
		"Aviso : Argumento modoDesenvolvimento não informado ou incorreto, utilizando o valor padrão : false");

var usuarioLogado
	= MiddlewareAutenticacao.Autenticar(modoDesenvolvimento,
										baseDeDados.Usuarios);

MiddlewareContexto.Inicializar(baseDeDados, usuarioLogado);

Console.Clear();
Console.WriteLine("Saindo...");
