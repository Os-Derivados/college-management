using System.Collections;
using System.Reflection;
using System.Text;
using college_management.Utilitarios.Atributos;


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
			if (propriedade is null || propriedade.GetCustomAttribute<PropriedadeModeloAttribute>()
			    is { Tipo: TipoPropriedade.Privada })
				continue;
			var valor = ObterValor(modelo, propriedade);

			resultado.Add(nome, valor);
		}

		return resultado;
	}

	private static string ObterValor<T>(T modelo, PropertyInfo propriedade)
	{
		var atributo = propriedade.GetCustomAttribute<PropriedadeModeloAttribute>();
		if (atributo is null || atributo.Tipo is TipoPropriedade.Valor)
			return propriedade.GetValue(modelo)?.ToString() 
			       ?? string.Empty;

		return $"{(propriedade.GetValue(modelo) as ICollection)?.Count ?? 0} {atributo.Identificador}";
	}
}
