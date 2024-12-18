namespace college_management.Views;


public class DetalhesView : View
{
	private readonly Dictionary<string, string> _detalhes;

	public DetalhesView(string titulo,
	                    Dictionary<string, string> detalhes) :
		base(titulo)
	{
		_detalhes = detalhes;
	}

	public override void Exibir()
	{
		InputView detalhes = new(Titulo);
		detalhes.ConstruirLayout();
		
		detalhes.LerEntrada("Detalhes", Layout.ToString());
	}

	public override string ConstruirLayout()
	{
		foreach (var detalhe in
		         _detalhes)
			Layout.AppendLine($"{detalhe.Key}: {detalhe.Value}");
		
		return Layout.ToString();
	}
}
