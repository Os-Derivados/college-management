using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Utilitarios;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class EditarUsuarioView : View, IEditarModeloView<Usuario>
{
	public EditarUsuarioView(Usuario usuario) : base("Editar Usuario")
	{
		Usuario = usuario;
	}

	private Usuario Usuario { get; set; }

	public Usuario Editar()
	{
		MenuView camposEditaveis = new("Editar Usuário",
		                               "Selecione um dos campos para editar.",
		                               ["Nome", "Senha", "Tipo"]);

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
					Usuario.GerarCredenciais(inputEdicao.ObterEntrada("Senha"));

					break;
				}
				case 3:
				{
					var tipoInserido = inputEdicao.ObterEntrada("Tipo") switch
					{
						"Aluno"   => typeof(Aluno),
						"Docente" => typeof(Docente),
						"Gestor"  => typeof(Gestor),
						_         => null
					};

					if (tipoInserido is null)
					{
						Aviso("[Erro] Tipo inválido. Tente novamente.");

						break;
					}

					Usuario = tipoInserido switch
					{
						_ when tipoInserido == typeof(Aluno) => new Aluno(
							Usuario.Login!,
							Usuario.Nome!,
							Usuario.Credenciais!),
						_ when tipoInserido == typeof(Docente) => new Docente(
							Usuario.Login!,
							Usuario.Nome!,
							Usuario.Credenciais!),
						_ when tipoInserido == typeof(Gestor) => new Gestor(
							Usuario.Login!,
							Usuario.Nome!,
							Usuario.Credenciais!),
						_ => throw new ArgumentOutOfRangeException()
					};

					break;
				}
			}

			DetalhesView detalhesUsuario = new("Editar Usuário",
			                                   UtilitarioTipos
				                                   .ObterPropriedades(Usuario,
				                                   [
					                                   "Nome", "Senha",
					                                   "Tipo"
				                                   ]));

			detalhesUsuario.ConstruirLayout();

			camposEditaveis = new MenuView("Editar Usuário",
			                               $"""
			                                {detalhesUsuario.Layout}

			                                Os campos editáveis estão abaixo.
			                                """,
			                               ["Nome", "Senha", "Tipo"]);

			camposEditaveis.ConstruirLayout();
			camposEditaveis.LerEntrada();
		}

		return Usuario;
	}
}
