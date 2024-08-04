using college_management.Dados.Repositorios.Interfaces;
using college_management.Modelos;
using college_management.Servicos;

namespace college_management.Dados.Repositorios;

public sealed class Repositorio<T> : IRepositorio<T> where T : Modelo
{
    public Repositorio()
    {
        _servicoDeArquivos = new ServicoDeArquivos<T>();
        _baseDeDados = new List<T>();
    }

    private List<T>? _baseDeDados;
    private readonly ServicoDeArquivos<T> _servicoDeArquivos;
    
    public async void Dispose()
    {
        await _servicoDeArquivos.SalvarAssicrono(_baseDeDados);
    }

    public async Task Adicionar(T modelo)
    {
        _baseDeDados.Add(modelo);

        await Task.Run(Dispose);
    }

    public async Task<List<T>> ObterTodos()
    {
        if (_baseDeDados.Count is 0)
            _baseDeDados = await _servicoDeArquivos.CarregarAssincrono();
        
        return _baseDeDados;
    }

    public async Task<T> ObterPorId(string? id)
    {
        if (_baseDeDados.Count is 0)
            _baseDeDados = await _servicoDeArquivos.CarregarAssincrono();
        
        return _baseDeDados.FirstOrDefault(t => t.Id == id);
    }

    public async Task Atualizar(T modelo)
    {
        var modeloAntigo = await ObterPorId(modelo.Id);

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
        var modelo = await ObterPorId(id);

        if (modelo is null)
            return;
        
        _baseDeDados.Remove(modelo);
        await Task.Run(Dispose);
    }
}