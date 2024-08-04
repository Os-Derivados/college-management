namespace college_management.Utilitarios;

public static class VariaveisDeAmbiente
{
    public static void Carregar(string caminhoDoArquivo)
    {
        if (!File.Exists(caminhoDoArquivo)) return;

        foreach (var linha in File.ReadAllLines(caminhoDoArquivo))
        {
            var partes = linha.Split('=', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length is not 2)
                continue;
            
            Environment.SetEnvironmentVariable(partes[0], partes[1]);
        }
    }
}