using college_management.Dados;


namespace college_management.Servicos.Interfaces;


public interface IServicoModelos<T>
{
	public RespostaRecurso<T> Buscar(CriterioBusca modoBusca, string chaveBusca);

	public bool Validar(RespostaRecurso<T> resposta);
}
