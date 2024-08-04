using college_management.Modelos;

namespace college_management.Dados.Repositorios.Interfaces;

public interface IRepositorio<T> : IDisposable where T : Modelo
{
    Task Adicionar(T modelo);
    Task<List<T>> ObterTodos();
    Task<T> ObterPorId(string? id);
    Task Atualizar(T modelo);
    Task Remover(string? id);
}