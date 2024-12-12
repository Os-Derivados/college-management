using college_management.Dados.Modelos;
using college_management.Utilitarios;


namespace college_management.Views;


public class RelatorioView<T> : View where T : Modelo
{
	private readonly List<T> _modelos;

	public RelatorioView(string titulo, List<T> modelos) :
		base(titulo)
	{
		_modelos = modelos;
	}

	public override void ConstruirLayout()
	{
		var tipo         = typeof(T);
		var propriedades = tipo.GetProperties();
		var nomesPropriedades =
			UtilitarioTipos.ObterNomesPropriedades(propriedades);

		Layout.AppendLine(nomesPropriedades);

		foreach (var p in propriedades)
			Layout.Append($"| {new string('-', 16)} ");

		Layout.AppendLine("|");

		foreach (var modelo in _modelos)
			Layout.AppendLine(modelo.ToString());
	}
}
