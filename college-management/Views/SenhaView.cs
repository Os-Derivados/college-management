using college_management.Views.Interfaces;


namespace college_management.Views;


public sealed class SenhaView : View, IInputView
{
	public readonly Dictionary<string, string> EntradasUsuario
		= new();

	private string? _mensagem;

	public SenhaView(string titulo) :
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

		ConsoleKeyInfo entrada = new();
		string buffer = string.Empty;

		while (entrada.Key is not ConsoleKey.Enter)
		{
			Console.Clear();
			Exibir();
			
			Console.Write(new string('*', buffer.Length));
			
			entrada = Console.ReadKey();
			if (entrada.Key is ConsoleKey.Backspace)
			{
				buffer = buffer.Length - 1 <= 0 ? string.Empty : buffer[..^1];
				continue;
			}

			buffer += entrada.KeyChar;
		}

		EntradasUsuario.Add(chave, buffer.Trim());
	}

	public string ObterEntrada(string chave)
	{
		_ = EntradasUsuario.TryGetValue(chave, out var entrada);

		return entrada ?? "";
	}

	public override void ConstruirLayout()
	{
		Layout.AppendLine(Titulo);
		Layout.AppendLine();
		Layout.Append(_mensagem);
	}
}
