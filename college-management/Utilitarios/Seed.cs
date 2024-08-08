using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;

namespace college_management.Utilitarios;

public static class Seed
{
    public static async Task IniciarBaseDeDados(BaseDeDados baseDeDados)
    {
        var (loginMestre, nomeMestre, senhaMestre)
            = ObterCredenciais(VariaveisDeAmbiente.MasterAdminLogin,
                               VariaveisDeAmbiente.MasterAdminNome,
                               VariaveisDeAmbiente.MasterAdminSenha);
        
        await CadastrarUsuarioPadrao(
            new Funcionario(loginMestre,
                                   nomeMestre,
                                   new Cargo(
                                    CargosDeAcesso.CargoAdministradores),
                                   senhaMestre),
                                   baseDeDados);

        var (loginTeste, nomeTeste, senhaTeste)
            = ObterCredenciais(VariaveisDeAmbiente.UsuarioTesteNome,
                               VariaveisDeAmbiente.UsuarioTesteLogin,
                               VariaveisDeAmbiente.UsuarioTesteSenha);

        await CadastrarUsuarioPadrao(
            new Aluno(loginTeste,
                            nomeTeste,
                            new Cargo(CargosDeAcesso
                                             .CargoAlunos),
                            senhaTeste,
                            new Matricula(2412130152,
                                        2,
                                        new Curso(
                                            "Curso Teste",
                                            [
                                                new Materia(
                                                        "Matéria Teste",
                                                        Turno.Integral,
                                                        60)
                                            ]),
                                        Modalidade.Presencial)),
                            baseDeDados);
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
        Ambiente.Variaveis
                .TryGetValue(login, out var loginDefault);
        Ambiente.Variaveis
                .TryGetValue(nome, out var nomeDefault);
        Ambiente.Variaveis
                .TryGetValue(senha, out var senhaDefault);

        return (loginDefault, nomeDefault, senhaDefault);
    }
}
