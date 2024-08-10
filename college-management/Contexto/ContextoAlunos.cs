using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Contexto;

public class ContextoAlunos : Contexto<Aluno>
{
    public ContextoAlunos(BaseDeDados baseDeDados) : base(baseDeDados)
    {
    }

    public override void AcessarNotas(Aluno usuario)
    {
        throw new NotImplementedException();
    }

    public override void AcessarGradeCurricular(Aluno usuario)
    {
        throw new NotImplementedException();
    }

    public override void AcessarGradeHoraria(Aluno usuario)
    {
        throw new NotImplementedException();
    }

    public override void AcessarFinanceiro(Aluno usuario)
    {
        throw new NotImplementedException();
    }

    public override void AcessarMatricula(Aluno usuario)
    {
        throw new NotImplementedException();
    }
}
