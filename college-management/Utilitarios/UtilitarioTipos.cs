using System.Reflection;
using System.Text;


namespace college_management.Utilitarios;


public static class UtilitarioTipos
{
	public static Dictionary<string, string> ObterPropriedades<T>(T modelo,
		string[] nomesPropriedades)
	{
		Dictionary<string, string> resultado  = new();
		var                        tipoModelo = typeof(T);

		foreach (var nome in nomesPropriedades)
		{
			var propriedade = tipoModelo.GetProperty(nome);
			var valor = propriedade?.GetValue(modelo)?.ToString() ??
			            string.Empty;

			resultado.Add(nome, valor);
		}

		return resultado;
	}
}
