using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoMaterias() : Contexto(typeof(Materia)), 
                                  IContexto<Materia>
{
    public async Task Cadastrar(Repositorio<Materia> repositorio,
                                Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Editar(Repositorio<Materia> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Excluir(Repositorio<Materia> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Visualizar(Repositorio<Materia> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
