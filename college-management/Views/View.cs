using System.Text;
using college_management.Servicos;
using college_management.Views.Interfaces;
using college_management.Utilitarios;

namespace college_management.Views;

public abstract class View : IView
{
	public readonly StringBuilder Layout = new();
	public readonly string Titulo;

	protected View(string titulo) { Titulo = titulo; }

	public virtual void Exibir()
	{
		Console.Clear();
		Console.Write(Layout.ToString());
	}

	public virtual void ConstruirLayout() { Layout.AppendLine(Titulo); }

	public static void Aviso(string mensagem)
	{
		new ServicoLog().Log($"{mensagem}", ServicoLog.Severidade.Aviso);

		InputView inputAviso = new("Aviso");
		inputAviso.ConstruirLayout();
		inputAviso.LerEntrada("Aviso", $"""
		                                {mensagem}

		                                Pressione [Enter] para continuar.
		                                """);
	}
}
