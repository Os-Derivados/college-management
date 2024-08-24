using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoCursos() : Contexto(typeof(Curso)), 
                               IContextoCursos
{
    public async Task Cadastrar(Repositorio<Curso> repositorio,
                                Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Editar(Repositorio<Curso> repositorio,
                             Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public async Task Excluir(Repositorio<Curso> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Visualizar(Repositorio<Curso> repositorio,
                           Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerGradeHoraria(Repositorio<Curso> repositorio,
                                Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void VerGradeCurricular(Repositorio<Curso> repositorio,
                                   Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
