using System.Text.Json;
using college_management.Dados.Modelos;

namespace college_management.Servicos;

public sealed class ServicoDados<T> where T : Modelo
{
    private readonly string _caminhoDoArquivo =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
            "OsDerivados.CollegeManagement",
            $"{typeof(T)}_db.json");

    public ServicoDados()
    {
        if (!Directory.Exists(
                Path.GetDirectoryName(_caminhoDoArquivo)!))
            Directory.CreateDirectory(
                Path.GetDirectoryName(_caminhoDoArquivo)!);

        if (!File.Exists(_caminhoDoArquivo))
            File.Create(_caminhoDoArquivo).Dispose();
    }

    public async Task SalvarAssicrono(List<T>? items)
    {
        await using var fs = File.OpenWrite(_caminhoDoArquivo);
        await JsonSerializer.SerializeAsync(fs, items);
    }

    public async Task<List<T>?> CarregarAssincrono()
    {
        try
        {
            await using var fs = File.OpenRead(_caminhoDoArquivo);
            return await JsonSerializer.DeserializeAsync<List<T>>(fs);
        }
        catch (Exception e) when (e is FileNotFoundException
                                      or DirectoryNotFoundException)
        {
            throw;
        }
    }
}
