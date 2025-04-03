using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class EditarUsuarioView : View, IEditarModeloView<Usuario>
{
	public EditarUsuarioView(Usuario usuario,
	                         IRepositorio<Cargo> repositorioCargos)
		: base("Editar Usuario")
	{
		Usuario           = usuario;
		RepositorioCargos = repositorioCargos;
	}

	private Usuario             Usuario           { get; }
	private IRepositorio<Cargo> RepositorioCargos { get; }

	public Usuario Editar()
	{
		MenuView camposEditaveis = new("Editar Usuário",
		                               "Selecione um dos campos para editar.",
		                               ["Nome", "Senha", "Cargo"]);

		camposEditaveis.ConstruirLayout();
		camposEditaveis.LerEntrada();

		while (camposEditaveis.OpcaoEscolhida is not 0)
		{
			Console.Clear();

			var indiceOpcao    = camposEditaveis.OpcaoEscolhida;
			var opcaoEscolhida = camposEditaveis.Opcoes[indiceOpcao - 1];
			var mensagemCampo
				= $"Insira um novo valor para \"{opcaoEscolhida}\": ";

			InputView inputEdicao = new("Editar Usuário");
			inputEdicao.LerEntrada(opcaoEscolhida, mensagemCampo);

			switch (camposEditaveis.OpcaoEscolhida)
			{
				case 1:
				{
					Usuario.Nome = inputEdicao.ObterEntrada("Nome");

					break;
				}
				case 2:
				{
					Usuario.Credenciais
						= new CredenciaisUsuario(
							inputEdicao.ObterEntrada("Senha"));

					break;
				}
				case 3:
				{
					var cargoInserido = RepositorioCargos.ObterPorNome(
						inputEdicao.ObterEntrada("Cargo"));

					if (cargoInserido.Status is StatusResposta.ErroNaoEncontrado)
					{
						inputEdicao.LerEntrada(
							"Erro",
							"Cargo Inválido. Pressione [Enter] para continuar.");

						break;
					}

					Usuario.CargoId = cargoInserido.Modelo!.Id!;

					break;
				}
			}

			DetalhesView detalhesUsuario = new("Editar Usuário",
			                                   UtilitarioTipos
				                                   .ObterPropriedades(Usuario,
				                                   [
					                                   "Nome", "Senha",
					                                   "CargoId"
				                                   ]));

			detalhesUsuario.ConstruirLayout();

			camposEditaveis = new MenuView("Editar Usuário",
			                               $"""
			                                {detalhesUsuario.Layout}

			                                Os campos editáveis estão abaixo.
			                                """,
			                               ["Nome", "Senha", "Cargo"]);

			camposEditaveis.ConstruirLayout();
			camposEditaveis.LerEntrada();
		}

		return Usuario;
	}
}
