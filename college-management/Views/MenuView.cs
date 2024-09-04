using System.Text;

namespace college_management.Views;

public class MenuView : View
{
    private readonly string _cabecalho;
    private readonly string[] _opcoes;

    public MenuView(string titulo,
                    string cabecalho,
                    string[] opcoes) : base(titulo)
    {
        _cabecalho = cabecalho;
        _opcoes = opcoes;
    }

    public override void ConstruirLayout()
    {
        Layout.AppendLine(_cabecalho);

        for (var i = 0; i < _opcoes.Length; i++)
            Layout.AppendLine($"[{i + 1}] {_opcoes[i]}");

        Layout.AppendLine();
        Layout.Append("Sua opção (somente números): ");
    }
}
