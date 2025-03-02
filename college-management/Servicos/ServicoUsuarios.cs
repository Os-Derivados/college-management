using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoUsuarios : ServicoModelos<Usuario>
{
	public ServicoUsuarios(IRepositorioUsuarios repositorioUsuarios)
		: base(repositorioUsuarios)
	{
	}

	public override Usuario? Pesquisar()
	{
		BuscaUsuarioView buscaUsuario = new();

		var resultadoBusca = buscaUsuario.Buscar();
		var chaveBusca     = resultadoBusca.Value;

		_ = Enum.TryParse<CriterioBusca>(resultadoBusca.Key,
		                                 out var criterioBusca);

		var obterUsuario = Buscar(criterioBusca, chaveBusca);

		return ValidarResposta(obterUsuario, ModoOperacao.Leitura)
			? null
			: obterUsuario.Modelo;
	}
}
