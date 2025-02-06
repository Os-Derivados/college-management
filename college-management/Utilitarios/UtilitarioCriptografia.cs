using System.Security.Cryptography;
using System.Text;


namespace college_management.Utilitarios;


/// <summary>
///     Utilitários para encriptação e cifras.
/// </summary>
public static class UtilitarioCriptografia
{
    /// <summary>
    ///     Criptografa uma string usando SHA256 e opcionalmente, um salt.
    /// </summary>
    /// <param name="valor">O valor a ser criptografado.</param>
    /// <param name="sal">Um salt para ser adicionado ao resultado final.</param>
    /// <returns>O valor agora criptografado.</returns>
    public static string CriptografarSha256(string valor, string? sal = null)
	{
		return
			Convert.ToHexString(
				       SHA256.HashData(
					       Encoding.UTF8.GetBytes(
						       valor + (sal ?? string.Empty)
					       )
				       )
			       )
			       .ToLower();
	}

    /// <summary>
    ///     Gera uma string aleatória usando um CSPRNG.
    /// </summary>
    /// <param name="tamanho">Tamanho do buffer gerado.</param>
    /// <returns>Uma string aleatória.</returns>
    public static string
		GerarSal(int tamanho
			         = 12) // 12 é um valor arbitrário escolhido aleatoriamente. Deve ser suficiente para nossos usos.
	{
		var buffer = new byte[tamanho];
		RandomNumberGenerator.Fill(buffer);
		return Convert.ToHexString(buffer);
	}
}
