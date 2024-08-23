using college_management.Constantes;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Funcionalidades;

public sealed class Autenticacao
{
    private readonly RepositorioUsuarios _repositorioUsuarios;

    public Autenticacao(RepositorioUsuarios repositorioUsuarios)
    {
        _repositorioUsuarios = repositorioUsuarios;
    }

    public Usuario Login()
    {
        // 1. Pedir o login
        // 2 Pedir a senha
        // 3. Verificar se o login existe
        // 3.1. Se não existir -> Login ou a senha estão inválidos
        // 3.2. Se o login está válido -> Validar a senha
        // 3.3. Se a senha estiver errada -> Login ou senha estão inválidos
        // 3.4. Se a senha estiver válida -> Retornar o usuário encontrado na base de dados

        // TODO:
        // * Substituir o usuário abaixo por um usuário existente
        // na base de dados, com base nas informações
        // recebidas durante a interação
        return new Aluno("thiago.santos",
                         "Thiago Santos",
                         new Cargo(CargosAcesso.CargoAlunos),
                         "senha12345",
                         new Matricula(2412130152,
                                       2,
                                       new Curso(
                                           "Ciência da Computação",
                                           [
                                               new Materia("Cálculo 1",
                                                           Turno
                                                               .Noturno,
                                                           60)
                                           ]),
                                       Modalidade.Presencial));
    }
}
