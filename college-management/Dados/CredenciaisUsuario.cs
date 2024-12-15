using college_management.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace college_management.Dados;

public class CredenciaisUsuario
{
    public string Senha { get; set; }
    public string Sal { get; set; }

    public CredenciaisUsuario(string senha, string? sal = null)
    {
        Senha = senha;
        Sal = sal ?? Utilitarios.UtilitarioCriptografia.GerarSal();
    }

    public static CredenciaisUsuario? TryParse(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;

        var split = input.Split('+', 2);
        if (split.Length <= 1) return null;

        (string senha, string sal) = (split[0], split[1]);
        return new CredenciaisUsuario(senha, sal);
    }

    public bool Validar(string senha)
    {
        return Utilitarios.UtilitarioCriptografia.CriptografarSha256(senha, Sal) == Senha;
    }

    public override string ToString()
    {
        return $"{Senha}+{Sal}";
    }
}