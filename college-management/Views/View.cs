using System.Text;
using college_management.Views.Interfaces;
using college_management.Utilitarios; // Import Logger

namespace college_management.Views;

public abstract class View : IView
{
	public readonly StringBuilder Layout = new();
	public readonly string Titulo;
	private static readonly Logger logger = new Logger(); // Initialize Logger

	protected View(string titulo) { Titulo = titulo; }

	public virtual void Exibir()
	{
		Console.Clear();
		Console.Write(Layout.ToString());
	}

	public virtual void ConstruirLayout() { Layout.AppendLine(Titulo); }

	public static void Aviso(string mensagem)
	{
		// Log the warning message
		logger.Log($"Aviso: {mensagem}", Logger.LogLevel.Warn);

		// Display the message to the user
		InputView inputAviso = new("Aviso");
		inputAviso.ConstruirLayout();
		inputAviso.LerEntrada("Aviso", $"""
		                                {mensagem}

		                                Pressione [Enter] para continuar.
		                                """);
	}
}
