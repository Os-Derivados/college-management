using college_management.Dados.Modelos;

namespace college_management.Views;

public class DetalhesView : View
{
    public DetalhesView(string titulo,
                        Dictionary<string, string> detalhes) :
        base(titulo)
    {
        _detalhes = detalhes;
    }

    private readonly Dictionary<string, string> _detalhes;

    public override void ConstruirLayout()
    {
        foreach (var detalhe in _detalhes)
            Layout.AppendLine($"{detalhe.Key}: {detalhe.Value}");
    }
}
