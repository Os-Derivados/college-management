namespace college_management.Views.Interfaces;


public interface IInputView
{
	public void LerEntrada(string chave, string? mensagem);

	public string ObterEntrada(string chave);
}
