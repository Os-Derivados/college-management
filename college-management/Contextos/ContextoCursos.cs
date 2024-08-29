using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoCursos : Contexto<Curso>,
                                IContextoCursos
{
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
