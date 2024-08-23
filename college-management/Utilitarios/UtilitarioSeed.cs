using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Utilitarios;

public static class UtilitarioSeed
{
    public static async Task IniciarBaseDeDados(BaseDeDados baseDeDados)
    {
        await CadastrarCargoPadrao(
            new Cargo(CargosAcesso.CargoAdministradores),
            baseDeDados);
        await CadastrarCargoPadrao(
            new Cargo(CargosAcesso.CargoGestores),
            baseDeDados);
        await CadastrarCargoPadrao(
            new Cargo(CargosAcesso.CargoAlunos),
            baseDeDados);

        var (loginMestre, nomeMestre, senhaMestre)
            = ObterCredenciais(VariaveisAmbiente.MasterAdminLogin,
                               VariaveisAmbiente.MasterAdminNome,
                               VariaveisAmbiente.MasterAdminSenha);

        await CadastrarUsuarioPadrao(new Funcionario(loginMestre,
                                                     nomeMestre,
                                                     new Cargo(
                                                         CargosAcesso
                                                             .CargoAdministradores),
                                                     senhaMestre),
                                     baseDeDados);

        var (loginTeste, nomeTeste, senhaTeste)
            = ObterCredenciais(VariaveisAmbiente.UsuarioTesteLogin,
                               VariaveisAmbiente.UsuarioTesteNome,
                               VariaveisAmbiente.UsuarioTesteSenha);

        Materia materiaTeste = new("Matéria Teste",
                                   Turno.Integral,
                                   60);
        await CadastrarMateriaPadrao(materiaTeste, baseDeDados);

        Curso cursoTeste = new("Curso Teste",
                               [materiaTeste]);
        await CadastrarCursoPadrao(cursoTeste, baseDeDados);

        Matricula matriculaTeste = new(1,
                                       1,
                                       cursoTeste,
                                       Modalidade.Presencial);

        await CadastrarUsuarioPadrao(new Aluno(loginTeste,
                                               nomeTeste,
                                               new Cargo(CargosAcesso
                                                             .CargoAlunos),
                                               senhaTeste,
                                               matriculaTeste),
                                     baseDeDados);
    }

    private static async Task CadastrarCargoPadrao(Cargo cargo,
                                                   BaseDeDados
                                                       baseDeDados)
    {
        var cargos = baseDeDados.cargos.ObterTodos();

        if (cargos.Count is 0)
            await baseDeDados.cargos.Adicionar(cargo);
    }

    private static async Task CadastrarMateriaPadrao(Materia materia,
                                                     BaseDeDados
                                                         baseDeDados)
    {
        var materias = baseDeDados.materias.ObterTodos();

        if (materias.Count is 0)
            await baseDeDados.materias.Adicionar(materia);
    }

    private static async Task CadastrarCursoPadrao(Curso curso,
                                                   BaseDeDados
                                                       baseDeDados)
    {
        var cursos = baseDeDados.cursos.ObterTodos();

        if (cursos.Count is 0)
            await baseDeDados.cursos.Adicionar(curso);
    }

    private static async Task CadastrarUsuarioPadrao(Usuario usuario,
                                                     BaseDeDados
                                                         baseDeDados)
    {
        var usuarioPadrao =
            baseDeDados.usuarios.ObterPorLogin(usuario.Login);

        if (usuarioPadrao is not null)
            return;

        await baseDeDados.usuarios.Adicionar(usuario);
    }

    private static (string, string, string) ObterCredenciais(
        string login,
        string nome,
        string senha)
    {
        UtilitarioAmbiente.Variaveis
                          .TryGetValue(login, out var loginDefault);
        UtilitarioAmbiente.Variaveis
                          .TryGetValue(nome, out var nomeDefault);
        UtilitarioAmbiente.Variaveis
                          .TryGetValue(senha, out var senhaDefault);

        return (loginDefault, nomeDefault, senhaDefault);
    }
}
