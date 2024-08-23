using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoUsuarios() : Contexto(typeof(Usuario)), 
                                  IContexto<Usuario>,
                                  IContextoUsuarios
{
    public async Task Cadastrar(Repositorio<Usuario> repositorio,
                                Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Editar(Repositorio<Usuario> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Excluir(Repositorio<Usuario> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Visualizar(Repositorio<Usuario> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task EditarMatricula(Repositorio<Usuario> repositorio,
                                      Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerMatricula(Repositorio<Usuario> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerBoletim(Repositorio<Usuario> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerFinanceiro(Repositorio<Usuario> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
