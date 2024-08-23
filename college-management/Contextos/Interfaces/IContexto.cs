using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos.Interfaces;

public interface IContexto<T> where T : Modelo
{
    public Task Cadastrar(Repositorio<T> repositorio, Usuario usuario);
    public Task Editar(Repositorio<T> repositorio, Usuario usuario);
    public Task Excluir(Repositorio<T> repositorio, Usuario usuario);
    public void Visualizar(Repositorio<T> repositorio, Usuario usuario);
}
