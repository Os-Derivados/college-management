using college_management.Views.Interfaces;


namespace college_management.Views;


public class ConfirmaView : View, IConfirmaView
{
	public ConfirmaView(string titulo) : base(titulo) { }

	public char Confirmar(string mensagem)
	{
		Console.Clear();
		Console.Write($"""
		               {Titulo}

		               {mensagem}

		               Deseja confirmar? [S] [N]:  
		               """);

		return Console.ReadKey().KeyChar;
	}
}
