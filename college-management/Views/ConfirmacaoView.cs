using college_management.Views.Interfaces;


namespace college_management.Views;


public class ConfirmacaoView : View, IConfirmacaoView
{
	public ConfirmacaoView(string titulo) : base(titulo) { }

	public  string? Confirmacao { get; set; }
	private string? Mensagem    { get; set; }

	public override string ConstruirLayout()
	{
		Layout.AppendLine(Mensagem);
		Layout.AppendLine(GerarDivisoria('-'));
		Layout.AppendLine("Deseja confirmar? [S/N]");
		
		return Layout.ToString();
	}

	public void Confirmar(string mensagem)
	{
		Mensagem = mensagem;
		
		ConstruirLayout();
		Exibir();
		
		Confirmacao = Console.ReadKey().KeyChar.ToString().ToLower();
	}
}
