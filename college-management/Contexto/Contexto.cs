using college_management.Contexto.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Contexto;

public abstract class Contexto<T> : IContexto<T> where T : Usuario
{
    protected readonly BaseDeDados BaseDeDados;

    protected Contexto(BaseDeDados baseDeDados)
    {
        BaseDeDados = baseDeDados;
    }

    public abstract void AcessarNotas(T usuario);

    public abstract void AcessarGradeCurricular(T usuario);

    public abstract void AcessarGradeHoraria(T usuario);

    public abstract void AcessarFinanceiro(T usuario);

    public abstract void AcessarMatricula(T usuario);
}
