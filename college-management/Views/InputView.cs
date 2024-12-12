using college_management.Views.Interfaces;


namespace college_management.Views;


public sealed class InputView : View, IInputView
{
	public readonly Dictionary<string, string> EntradasUsuario
		= new();

	private string? _mensagem;

	public InputView(string titulo) :
		base(titulo)
	{
	}

	public void LerEntrada(string chave, string? mensagem = null)
	{
		if (mensagem is not null)
		{
			Layout.Clear();
			_mensagem = mensagem;
			ConstruirLayout();
		}

		Console.Clear();
		Exibir();

		var entrada = Console.ReadLine();

		if (entrada is null) return;

		EntradasUsuario.Add(chave, entrada);
	}

	public string ObterEntrada(string chave)
	{
		_ = EntradasUsuario.TryGetValue(chave, out var entrada);

		return entrada ?? "";
	}

	public override void ConstruirLayout()
	{
		Layout.AppendLine(Titulo);
		Layout.AppendLine(GerarDivisoria('-'));
		Layout.AppendLine();
		Layout.Append(_mensagem);
	}
}
