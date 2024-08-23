using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoCargos() : Contexto(typeof(Cargo)),
                               IContexto<Cargo>
{
    public async Task Cadastrar(Repositorio<Cargo> repositorio,
                                Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Editar(Repositorio<Cargo> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Excluir(Repositorio<Cargo> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Visualizar(Repositorio<Cargo> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
