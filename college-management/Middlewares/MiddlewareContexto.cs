using college_management.Constantes;
using college_management.Contextos;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Servicos;
using college_management.Views;


namespace college_management.Middlewares;


public static class MiddlewareContexto
{
	public static void Inicializar(BaseDeDados baseDeDados,
	                               Usuario usuario)
	{
		var obterCargo = baseDeDados.Cargos.ObterPorId(usuario.CargoId);

		if (obterCargo.Status is StatusResposta.ErroNaoEncontrado) return;

		var opcaoContexto = EscolherContexto(obterCargo.Modelo!);

		if (opcaoContexto is "") return;

		ServicoCargos   servicoCargos   = new(baseDeDados.Cargos);
		ServicoUsuarios servicoUsuarios = new(baseDeDados.Usuarios);
		ServicoCursos   servicoCursos   = new(baseDeDados.Cursos);

		ContextoUsuarios contextoUsuarios = new(baseDeDados, usuario, servicoCargos, servicoUsuarios);
		ContextoCargos contextoCargos = new(baseDeDados, usuario, servicoCargos);
		ContextoMaterias contextoMaterias = new(baseDeDados, usuario);
		ContextoCursos contextoCursos = new(baseDeDados, usuario, servicoCursos);

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
					ConverterParaMetodo(contexto,
					                    opcaoEscolhida);

				Console.Clear();

				contexto.AcessarRecurso(recursoEscolhido);
			}
			else
			{
				estadoAtual = EstadoDoApp.Sair;
			}
		} while (estadoAtual is EstadoDoApp.Recurso);
	}

	private static string ConverterParaMetodo<T>(Contexto<T> contexto,
	                                             ConsoleKeyInfo indice)
		where T : Modelo
	{
		var recursosDisponiveis = contexto.ObterOpcoes();

		_ = int.TryParse(indice.KeyChar.ToString(), out var i);

		var recursoEscolhido = recursosDisponiveis
		                       .Select(r => r
		                                    .Trim()
		                                    .Replace(" ", ""))
		                       .ElementAt(i - 1);

		return recursoEscolhido;
	}

	private static string EscolherContexto(Cargo cargoUsuario)
	{
		var estadoAtual       = EstadoDoApp.Contexto;
		var contextoEscolhido = "";

		do
		{
			var opcoesContextos = ObterOpcoesContextos(cargoUsuario);

			MenuView menuContextos = new("Menu Contextos",
			                             "Bem-vindo(a).",
			                             opcoesContextos);

			menuContextos.ConstruirLayout();
			menuContextos.Exibir();

			var opcaoEscolhida = Console.ReadKey();
			var opcaoValida = int.TryParse(opcaoEscolhida
			                               .KeyChar
			                               .ToString(),
			                               out var opcaoUsuario);

			if (!opcaoValida) continue;

			if (opcaoUsuario is 0) break;

			contextoEscolhido = opcoesContextos[opcaoUsuario - 1];
			estadoAtual       = EstadoDoApp.Recurso;
		} while (estadoAtual is EstadoDoApp.Contexto);

		return contextoEscolhido;
	}

	private static string[] ObterOpcoesContextos(Cargo cargoUsuario)
	{
		var temPermissoesAdmin = cargoUsuario
			                         .TemPermissao(
				                         PermissoesAcesso.AcessoEscrita)
		                         || cargoUsuario
			                         .TemPermissao(
				                         PermissoesAcesso
					                         .AcessoAdministradores);

		return temPermissoesAdmin
			? AcessosContexto.ContextoEscrita
			: AcessosContexto.ContextoLeitura;
	}
}
