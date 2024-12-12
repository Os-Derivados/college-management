using System.Text;
using college_management.Views.Interfaces;


namespace college_management.Views;


public abstract class View : IView
{
	protected readonly StringBuilder Layout = new();
	public readonly    string        Titulo;

	protected View(string titulo) { Titulo = titulo; }

	public virtual void Exibir()
	{
		Console.Clear();
		Console.Write(Layout.ToString());
	}

	public virtual string ConstruirLayout()
	{
		Layout.AppendLine(Titulo);

		return Layout.ToString();
	}

	public string GerarDivisoria(char separador)
	{
		StringBuilder divisoria = new();

		for (var i = 0; i < Console.WindowWidth; i++)
			divisoria.Append(separador);

		return divisoria.ToString();
	}
}
