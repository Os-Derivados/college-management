using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Views;


namespace college_management.Servicos;


public class ServicoMaterias : ServicoModelos<Materia>
{
	public ServicoMaterias(IRepositorio<Materia> repositorio) : base(repositorio)
	{
	}

	public override Materia? Pesquisar()
	{
		BuscaMateriaView buscaMateria   = new("Buscar Matéria");
		var              resultadoBusca = buscaMateria.Buscar();

		_ = Enum.TryParse<CriterioBusca>(resultadoBusca.Key,
		                                 out var criterioBusca);

		var obterMateria
			= Buscar(criterioBusca, resultadoBusca.Value);

		return ValidarResposta(obterMateria, ModoOperacao.Leitura) ? null : obterMateria.Modelo;
	}
}
