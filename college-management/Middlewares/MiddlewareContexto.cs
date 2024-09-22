using System.Text;
using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Contextos;
using college_management.Views;


namespace college_management.Middlewares;


public static class MiddlewareContexto
{
	public static void Inicializar(BaseDeDados baseDeDados,
	                               Usuario     usuario)
	{
		var opcaoContexto = EscolherContexto(usuario);

		if (opcaoContexto is "") return;

		ContextoUsuarios contextoUsuarios
			= new(baseDeDados, usuario);

		ContextoCargos contextoCargos
			= new(baseDeDados, usuario);

		ContextoMaterias contextoMaterias
			= new(baseDeDados, usuario);

		ContextoCursos contextoCursos
			= new(baseDeDados, usuario);

		switch (opcaoContexto)
		{
			case AcessosContexto.ContextoUsuarios:
				AcessarContexto(contextoUsuarios);

				break;

			case AcessosContexto.ContextoCargos:
				AcessarContexto(contextoCargos);

				break;

			case AcessosContexto.ContextoCursos:
				AcessarContexto(contextoCursos);

				break;

			case AcessosContexto.ContextoMaterias:
				AcessarContexto(contextoMaterias);

				break;
		}
	}

	private static void AcessarContexto<T>(Contexto<T> contexto)
	where T : Modelo
	{
		var estadoAtual = EstadoDoApp.Recurso;

		do
		{
			Console.Clear();

			contexto.ListarOpcoes();

			var opcaoEscolhida = Console.ReadKey();

			if (opcaoEscolhida.Key is not ConsoleKey.D0)
			{
				var recursoEscolhido =
					ConverterParaMetodo(contexto, opcaoEscolhida);

				Console.Clear();

				contexto.AcessarRecurso(recursoEscolhido);
			}
			else
			{
				estadoAtual = EstadoDoApp.Sair;
			}
		}
		while (estadoAtual is EstadoDoApp.Recurso);
	}

	private static string ConverterParaMetodo<T>(Contexto<T> contexto,
												 ConsoleKeyInfo indice)
	where T : Modelo
	{
		var recursosDisponiveis = contexto.ObterOpcoes();

		_ = int.TryParse(indice.KeyChar.ToString(), out var i);

		var recursoEscolhido = recursosDisponiveis
		                       .Select(r => r.Trim()
		                                     .Replace(" ", ""))
		                       .ElementAt(i - 1);

		return recursoEscolhido;
	}

	private static string EscolherContexto(Usuario usuario)
	{
		var estadoAtual       = EstadoDoApp.Contexto;
		var contextoEscolhido = "";

		do
		{
			var opcoesContextos = ObterOpcoesContextos(usuario);

			MenuView menuContextos = new("Menu Contextos",
			                             "Bem-vindo(a).",
			                             opcoesContextos);

			menuContextos.ConstruirLayout();
			menuContextos.Exibir();

			var opcaoEscolhida = Console.ReadKey();
			var opcaoValida = int.TryParse(opcaoEscolhida.KeyChar
			                                             .ToString(),
			                               out var opcaoUsuario);

			if (!opcaoValida) continue;

			if (opcaoUsuario is 0) break;

			contextoEscolhido
				= opcoesContextos[opcaoUsuario - 1];

			estadoAtual = EstadoDoApp.Recurso;
		}
		while (estadoAtual is EstadoDoApp.Contexto);

		return contextoEscolhido;
	}

	private static string[] ObterOpcoesContextos(Usuario usuario)
	{
		return usuario.Cargo.Nome switch
		{
			CargosPadrao.CargoAlunos => AcessosContexto
				.AcessoAlunos,
			CargosPadrao.CargoGestores
				or CargosPadrao.CargoAdministradores =>
				AcessosContexto.AcessoGestoresAdministradores,
			_ => throw new
				     InvalidOperationException("O usuário não possui um cargo validado")
		};
	}
}
