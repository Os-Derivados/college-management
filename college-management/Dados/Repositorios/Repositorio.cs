using System.Text.Json;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Modelos;
using college_management.Servicos;

namespace college_management.Dados.Repositorios;

public abstract class Repositorio<T> : IRepositorio<T> where T : Modelo
{
    protected List<T>? _baseDeDados;
    private readonly ServicoDeArquivos<T> _servicoDeArquivos = new();

    protected Repositorio()
    {
        if (_baseDeDados.Count is not 0)
            return;

        Task.Run(async () =>
        {
            try
            {
                using var dadosSalvos = _servicoDeArquivos.CarregarAssincrono();
                _baseDeDados = await dadosSalvos;
            }
            catch (Exception e) when (e is JsonException or AggregateException or IOException)
            {
                _baseDeDados = [];
            }
        }).Wait();
    }
    
    public async void Dispose()
    {
        await _servicoDeArquivos.SalvarAssicrono(_baseDeDados);
    }

    public async Task Adicionar(T modelo)
    {
        _baseDeDados.Add(modelo);

        await Task.Run(Dispose);
    }

    public List<T> ObterTodos()
    {
        return _baseDeDados;
    }

    public T ObterPorId(string? id)
    {
        return _baseDeDados.FirstOrDefault(t => t.Id == id);
    }

    public async Task Atualizar(T modelo)
    {
        var modeloAntigo = ObterPorId(modelo.Id);

        if (modeloAntigo is null)
        {
            await Adicionar(modelo);
            
            return;
        }
        
        await Remover(modelo.Id);
        await Adicionar(modelo);
        await Task.Run(Dispose);
    }

    public async Task Remover(string? id)
    {
        var modelo = ObterPorId(id);

        if (modelo is null)
            return;
        
        _baseDeDados.Remove(modelo);
        await Task.Run(Dispose);
    }
}