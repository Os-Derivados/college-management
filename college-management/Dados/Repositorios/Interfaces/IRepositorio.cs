using college_management.Dados.Modelos;

namespace college_management.Dados.Repositorios.Interfaces;

public interface IRepositorio<T> where T : Modelo
{
    Task Adicionar(T modelo);
    List<T> ObterTodos();
    T ObterPorId(string? id);
    Task Atualizar(T modelo);
    Task Remover(string? id);

    bool Existe(T modelo);
}
