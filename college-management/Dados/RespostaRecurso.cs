namespace college_management.Dados;


public record RespostaRecurso<T>(T? modelo, StatusResposta status);

public enum StatusResposta
{
	Sucesso,
	NaoEncontrado,
	ErroDuplicata,
	ErroInvalido,
	ErroInterno,
	ErroNaoAutorizado
}
