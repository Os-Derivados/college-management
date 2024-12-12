using System.Reflection;
using System.Text;


namespace college_management.Utilitarios;


public static class UtilitarioTipos
{
	public static string ObterNomesPropriedades(PropertyInfo[] infos)
	{
		StringBuilder propriedades = new();

		foreach (var p in infos)
			propriedades.Append($"| {p.Name.PadRight(16)} ");

		propriedades.Append('|');

		return propriedades.ToString();
	}

	public static Dictionary<string, string> ObterPropriedades<T>(T modelo,
		string[] nomesPropriedades)
	{
		Dictionary<string, string> resultado  = new();
		var                        tipoModelo = typeof(T);

		foreach (var nome in nomesPropriedades)
		{
			var propriedade = tipoModelo.GetProperty(nome);
			var valor       = propriedade.GetValue(modelo);

			resultado.Add(nome, valor.ToString());
		}

		return resultado;
	}
}
