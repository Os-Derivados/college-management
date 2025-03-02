using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoCursos : ServicoModelos<Curso>
{
	public ServicoCursos(IRepositorio<Curso> repositorio) : base(repositorio)
	{
	}

	public override Curso? Pesquisar()
	{
		MenuView menuPesquisa = new("Pesquisar Curso",
		                            "Escolha o método de pesquisa.",
		                            ["Nome", "Id"]);

		InputView inputPesquisa = new("Ver Grade Curricular: Pesquisar Curso");

		menuPesquisa.ConstruirLayout();
		menuPesquisa.LerEntrada();

		(string Campo, string Mensagem)? campoPesquisa
			= menuPesquisa.OpcaoEscolhida switch
			{
				1 => ("Nome", "Insira o Nome do curso: "),
				2 => ("Id", "Insira o Id do curso: "),
				_ => null
			};

		if (campoPesquisa is null) return null;

		inputPesquisa.LerEntrada(campoPesquisa?.Campo!,
		                         campoPesquisa?.Mensagem);

		_ = Enum.TryParse<CriterioBusca>(campoPesquisa?.Campo!,
		                                 out var criterioBusca);

		var obterCurso = Buscar(criterioBusca,
		                                       inputPesquisa.ObterEntrada(
			                                       campoPesquisa?.Campo!));

		return ValidarResposta(obterCurso, ModoOperacao.Leitura)
			? null
			: obterCurso.Modelo;
	}
}
