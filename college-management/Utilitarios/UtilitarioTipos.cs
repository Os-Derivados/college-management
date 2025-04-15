using System.Collections;
using System.Reflection;
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

		return atributo switch
		{
			null or { Tipo: TipoPropriedade.Valor } =>
				propriedade.GetValue(modelo)?.ToString() ?? string.Empty,
			{ Tipo: TipoPropriedade.Colecao } =>
				string.Join(", ", propriedade.GetValue(modelo) as IEnumerable),
			{ Tipo: TipoPropriedade.Quantidade } =>
				$"{(propriedade.GetValue(modelo) as ICollection)?.Count ?? 0} {atributo.Identificador}",
			_ => string.Empty // Deve ser impossível
		};
	}
}
