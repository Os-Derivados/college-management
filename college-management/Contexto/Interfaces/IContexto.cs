using college_management.Dados.Modelos;

namespace college_management.Contexto.Interfaces;

public interface IContexto<T> where T : Usuario
{
    public void AcessarNotas(T usuario);
    public void AcessarGradeCurricular(T usuario);
    public void AcessarGradeHoraria(T usuario);
    public void AcessarFinanceiro(T usuario);
    public void AcessarMatricula(T usuario);
}
