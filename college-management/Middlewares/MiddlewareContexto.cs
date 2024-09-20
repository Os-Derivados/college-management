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
				AcessarContexto(contextoUsuarios, opcaoContexto);

				break;

			case AcessosContexto.ContextoCargos:
				AcessarContexto(contextoCargos, opcaoContexto);

				break;

			case AcessosContexto.ContextoCursos:
				AcessarContexto(contextoCursos, opcaoContexto);

				break;

			case AcessosContexto.ContextoMaterias:
				AcessarContexto(contextoMaterias, opcaoContexto);

				break;
		}
	}

	private static void AcessarContexto<T>(Contexto<T> contexto,
	                                       string opcaoContexto)
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
					ConverterParaMetodo(opcaoContexto,
					                    opcaoEscolhida);

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

	private static string ConverterParaMetodo(string opcao,
	                                          ConsoleKeyInfo
		                                          indice)
	{
		var recursosDisponiveis = opcao switch
		{
			AcessosContexto.ContextoUsuarios =>
				OperacoesRecursos.RecursoUsuarios,
			AcessosContexto.ContextoCursos =>
				OperacoesRecursos.RecursoCursos,
			AcessosContexto.ContextoMaterias =>
				OperacoesRecursos.RecursoMaterias,
			AcessosContexto.ContextoCargos =>
				OperacoesRecursos.RecursoCargos,
			_ => throw new
				     InvalidOperationException("Não há contexto definido para este tipo")
		};

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
