using college_management.Contexto.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Contexto;

public class ContextoFuncionarios : Contexto<Funcionario>, IContextoFuncionarios
{
    public ContextoFuncionarios(BaseDeDados baseDeDados) : base(baseDeDados)
    {
    }

    public override void AcessarNotas(Funcionario usuario) { throw new NotImplementedException(); }

    public override void AcessarGradeCurricular(Funcionario usuario) { throw new NotImplementedException(); }

    public override void AcessarGradeHoraria(Funcionario usuario) { throw new NotImplementedException(); }

    public override void AcessarFinanceiro(Funcionario usuario) { throw new NotImplementedException(); }

    public override void AcessarMatricula(Funcionario usuario) { throw new NotImplementedException(); }

    public void AcessarCursos(Funcionario funcionario) { throw new NotImplementedException(); }

    public void AcessarCargos(Funcionario funcionario) { throw new NotImplementedException(); }

    public void AcessarMaterias(Funcionario funcionario) { throw new NotImplementedException(); }

    public void AcessarUsuarios(Funcionario funcionario) { throw new NotImplementedException(); }
}
