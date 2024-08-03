using System.Text.Json;
using college_management.Modelos;

namespace college_management.Dados.Servicos;

public sealed class ServicoDeArquivos<T> where T : Modelo
{
    private readonly string _caminhoDoArquivo = 
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "OsDerivados.CollegeManagement", $"{typeof(T)}_db.json"
            );

    public async Task SalvarAssicrono(List<T>? items)
    {
        if (!Directory.Exists(Path.GetDirectoryName(_caminhoDoArquivo)!))
            Directory.CreateDirectory(Path.GetDirectoryName(_caminhoDoArquivo)!);

        await using var fs = File.Create(_caminhoDoArquivo);
        await JsonSerializer.SerializeAsync(fs, items);
    }

    public async Task<List<T>?> CarregarAssincrono()
    {
        try
        {
            await using var fs = File.OpenRead(_caminhoDoArquivo);
            return await JsonSerializer.DeserializeAsync<List<T>>(fs);
        }
        catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
        {
            return null;
        }
    }
}