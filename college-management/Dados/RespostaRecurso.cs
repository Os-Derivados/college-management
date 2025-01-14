namespace college_management.Dados;


public record RespostaRecurso<T>(T? Modelo, StatusResposta Status);

public enum StatusResposta
{
	Sucesso,
	NaoEncontrado,
	ErroDuplicata,
	ErroInvalido,
	ErroInterno,
	ErroNaoAutorizado
}
