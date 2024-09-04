using System.Text;
using college_management.Views.Interfaces;

namespace college_management.Views;

public abstract class View : IView
{
    protected readonly StringBuilder Layout = new();
    public readonly string Titulo;

    protected View(string titulo) { Titulo = titulo; }

    public void Exibir() { Console.Write(Layout.ToString()); }

    public virtual void ConstruirLayout() { Layout.AppendLine(Titulo); }
}
