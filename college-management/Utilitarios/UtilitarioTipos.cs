using System.Reflection;
using System.Text;

namespace college_management.Utilitarios;

public static class UtilitarioTipos
{
    public static string ObterNomesPropriedades(PropertyInfo[] infos)
    {
        StringBuilder propriedades = new();

        foreach (var p in infos) propriedades.Append($"{p.Name}\t");

        return propriedades.ToString();
    }
}
