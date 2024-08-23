using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos.Interfaces;

public interface IContextoCursos
{
    public void VerGradeHoraria(Repositorio<Curso> repositorio,
                                Usuario usuario);

    public void VerGradeCurricular(Repositorio<Curso> repositorio,
                                   Usuario usuario);
}
