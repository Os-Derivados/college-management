using college_management.Modelos;

namespace college_management.Dados.Repositorios.Interfaces;

public interface IRepositorio<T> : IDisposable where T : Modelo
{
    void Adicionar(T modelo);
    Task<List<T>> ObterTodos();
    T ObterPorId(string? id);
    void Atualizar(T modelo);
    void Remover(string? id);
}