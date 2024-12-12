namespace college_management.Views.Interfaces;


public interface IView
{
	public void Exibir();

	public string ConstruirLayout();

	public string GerarDivisoria(char separador);
}
