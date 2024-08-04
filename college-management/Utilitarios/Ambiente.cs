namespace college_management.Utilitarios;

public sealed class Ambiente
{
    public readonly Dictionary<string, string> variaveis = CarregarVariaveis();
    
    private static Dictionary<string, string> CarregarVariaveis()
    {
        Dictionary<string, string> variaveisDeAmbiente = new();
        
        var caminhoDoArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "OsDerivados.CollegeManagement", ".env");
        
        foreach (var linha in File.ReadAllLines(caminhoDoArquivo))
        {
            var partes = linha.Split('=', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length is not 2)
                continue;
            
            variaveisDeAmbiente.Add(partes[0], partes[1]);
        }

        return variaveisDeAmbiente;
    }
}