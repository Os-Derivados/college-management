using System.Text;
using college_management.Views.Interfaces;


namespace college_management.Views;


public abstract class View : IView
{
	public readonly StringBuilder Layout = new();
	public readonly    string        Titulo;

	protected View(string titulo) { Titulo = titulo; }

	public virtual void Exibir()
	{
		Console.Clear();
		Console.Write(Layout.ToString());
	}

	public virtual void ConstruirLayout()
	{
		Layout.AppendLine(Titulo);
	}
}
