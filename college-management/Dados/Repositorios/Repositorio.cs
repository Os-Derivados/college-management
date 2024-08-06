using System.Text.Json;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios.Interfaces;
using college_management.Servicos;

namespace college_management.Dados.Repositorios;

public abstract class Repositorio<T> : IRepositorio<T> where T : Modelo
{
    protected List<T>? BaseDeDados;
    private readonly ServicoDeArquivos<T> _servicoDeArquivos = new();

    protected Repositorio()
    {
        if (BaseDeDados is not null)
            return;

        Task.Run(async () =>
            {
                try
                {
                    using var dadosSalvos =
                        _servicoDeArquivos.CarregarAssincrono();
                    BaseDeDados = await dadosSalvos;
                }
                catch (Exception e) when (e is JsonException
                                              or AggregateException
                                              or IOException)
                {
                    BaseDeDados = [];
                }
            })
            .Wait();
    }

    public async void Dispose()
    {
        await _servicoDeArquivos.SalvarAssicrono(BaseDeDados);
    }

    public virtual async Task Adicionar(T modelo)
    {
        var modeloExistente = ObterPorId(modelo.Id);

        if (modeloExistente is not null)
            return;

        BaseDeDados.Add(modelo);

        await Task.Run(Dispose);
    }

    public List<T> ObterTodos() { return BaseDeDados; }

    public T ObterPorId(string? id)
    {
        return BaseDeDados.FirstOrDefault(t => t.Id == id);
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

        BaseDeDados.Remove(modelo);
        await Task.Run(Dispose);
    }
}
