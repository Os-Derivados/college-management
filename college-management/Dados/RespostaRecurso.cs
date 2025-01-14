namespace college_management.Dados;


public record RespostaRecurso<T>(T? Modelo, StatusResposta Status);

public enum StatusResposta
{
	Sucesso,
	ErroNaoEncontrado,
	ErroDuplicata,
	ErroInvalido,
	ErroInterno,
	ErroNaoAutorizado
}
