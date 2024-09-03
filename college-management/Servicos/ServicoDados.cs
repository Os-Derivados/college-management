using System.Text.Json;
using college_management.Servicos.Interfaces;

namespace college_management.Servicos;

public sealed class ServicoDados<T> : IServicoDados<T>
{
    public ServicoDados()
    {
        if (!Directory.Exists(
                Path.GetDirectoryName(_caminhoDoArquivo)!))
            Directory.CreateDirectory(
                Path.GetDirectoryName(_caminhoDoArquivo)!);

        if (!File.Exists(_caminhoDoArquivo))
            File.Create(_caminhoDoArquivo).Dispose();
    }

    private readonly string _caminhoDoArquivo =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
            "OsDerivados.CollegeManagement",
            $"{typeof(T).Name}s.json");

    public async Task SalvarAssicrono(List<T>? items)
    {
        await using var streamArquivo =
            File.OpenWrite(_caminhoDoArquivo);
        await JsonSerializer.SerializeAsync(streamArquivo, items);
    }

    public async Task<List<T>?> CarregarAssincrono()
    {
        await using var streamArquivo =
            File.OpenRead(_caminhoDoArquivo);
        return await JsonSerializer.DeserializeAsync<List<T>>(
                   streamArquivo);
    }
}
