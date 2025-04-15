using System.Collections;
using System.Reflection;
using System.Text;
using college_management.Utilitarios.Atributos;


namespace college_management.Utilitarios;


public static class UtilitarioTipos
{
	public static PropertyInfo[] ObterPropriedades<T>() =>
		typeof(T).GetProperties()
			.Where(i => i.GetCustomAttribute<PropriedadeModeloAttribute>()
				is not { Tipo: TipoPropriedade.Privada }).ToArray();
	
	public static Dictionary<string, string> ObterPropriedades<T>(T modelo)
	{
		Dictionary<string, string> resultado  = new();
		var                        tipoModelo = typeof(T);

		foreach (var propriedade in tipoModelo.GetProperties())
		{
			if (propriedade.GetCustomAttribute<PropriedadeModeloAttribute>()
			    is { Tipo: TipoPropriedade.Privada })
				continue;
			var valor = ObterValor(modelo, propriedade);

			resultado.Add(propriedade.Name, valor);
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
