using college_management.Constantes;
using college_management.Contextos;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Views;


namespace college_management.Middlewares;


public static class MiddlewareContexto
{
	private static EstadoDoApp _estadoAtual = EstadoDoApp.Login;

	public static void Inicializar(BaseDeDados baseDeDados, Usuario usuario)
	{
		ContextoUsuarios contextoUsuarios = new(baseDeDados, usuario);
		ContextoMaterias contextoMaterias = new(baseDeDados, usuario);
		ContextoCursos   contextoCursos   = new(baseDeDados, usuario);

		_estadoAtual = EstadoDoApp.Contexto;

		while (_estadoAtual is EstadoDoApp.Contexto)
		{
			var opcaoContexto = EscolherContexto(usuario);

			switch (opcaoContexto)
			{
				case AcessosContexto.ContextoUsuarios:
					AcessarContexto(contextoUsuarios);

					break;

				case AcessosContexto.ContextoCursos:
					AcessarContexto(contextoCursos);

					break;

				case AcessosContexto.ContextoMaterias:
					AcessarContexto(contextoMaterias);

					break;
			}

			if (string.IsNullOrEmpty(opcaoContexto) && ConfirmarSaida())
				break;
		}
	}

	private static void AcessarContexto<T>(Contexto<T> contexto)
		where T : Modelo
	{
		_estadoAtual = EstadoDoApp.Recurso;

		do
		{
			Console.Clear();

			var menuView = contexto.ObterMenuView();
			menuView.LerEntrada();

			var opcaoEscolhida = menuView.OpcaoEscolhida;

			if (opcaoEscolhida is not 0)
			{
				var recursoEscolhido
					= ConverterParaMetodo(contexto, opcaoEscolhida);

				Console.Clear();

				contexto.AcessarRecurso(recursoEscolhido);
			}
			else
			{
				_estadoAtual = EstadoDoApp.Contexto;
			}
		} while (_estadoAtual is EstadoDoApp.Recurso);
	}

	private static string ConverterParaMetodo<T>(Contexto<T> contexto,
	                                             int indice) where T : Modelo
	{
		var recursosDisponiveis = contexto.ObterOpcoes();

		_ = int.TryParse(indice.ToString(), out var i);

		var recursoEscolhido = recursosDisponiveis
		                       .Select(r => r.Trim().Replace(" ", ""))
		                       .ElementAt(i - 1);

		return recursoEscolhido;
	}

	private static string EscolherContexto(Usuario usuario)
	{
		var contextoEscolhido = "";

		do
		{
			MenuView menuContextos = new("Menu Contextos",
			                             "Bem-vindo(a).",
			                             AcessosContexto.Contextos);

			menuContextos.ConstruirLayout();
			menuContextos.LerEntrada();

			var opcaoEscolhida = menuContextos.OpcaoEscolhida;
			var opcaoValida = int.TryParse(opcaoEscolhida.ToString(),
			                               out var opcaoUsuario);

			if (!opcaoValida) continue;

			if (opcaoUsuario is 0) break;

			contextoEscolhido
				= AcessosContexto.Contextos[menuContextos.OpcaoEscolhida - 1];
			_estadoAtual = EstadoDoApp.Recurso;
		} while (_estadoAtual is EstadoDoApp.Contexto);

		return contextoEscolhido;
	}

	private static bool ConfirmarSaida() =>
		new ConfirmaView("Confirmação de saída")
			.Confirmar("Deseja sair da aplicação?")
			.ToString()
			.ToLower() is "s";
}
