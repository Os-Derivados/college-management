using System.Text;
using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public abstract class Contexto<T> : IContexto<T> where T : Modelo
{
    public void ListarOpcoes()
    {
        StringBuilder mensagem = new();

        var opcoes = typeof(T).Name switch
        {
            nameof(Usuario) => OperacoesRecurso.OperacoesUsuarios,
            nameof(Curso)   => OperacoesRecurso.OperacoesCursos,
            nameof(Materia) => OperacoesRecurso.OperacoesMaterias,
            nameof(Cargo)   => OperacoesRecurso.OperacoesCargos,
            _ => throw new InvalidOperationException(
                     "Não há contexto definido para este tipo")
        };

        mensagem.AppendLine(
            $"Bem vindo ao recuso de {typeof(T).Name}.\n"
            + $"Selecione uma das opções abaixo.\n");

        for (var i = 0; i < opcoes.Length; i++)
            mensagem.AppendLine($"[{i + 1}] {opcoes[i]}");

        mensagem.Append("\nSua opção (somente números): ");

        Console.Write(mensagem.ToString());
    }

    public void AcessarRecurso(string nomeRecurso,
                               BaseDeDados baseDeDados,
                               Usuario usuario)
    {
        var interfacesContexto = GetType().GetInterfaces();

        var recurso =
            interfacesContexto.Select(t => t.GetMethod(nomeRecurso))
                              .FirstOrDefault(t => t is not null);

        if (recurso is null)
            throw new InvalidOperationException("Recurso inexistente");

        dynamic repositorio = typeof(T).Name switch
        {
            nameof(Usuario) => baseDeDados.usuarios,
            nameof(Cargo)   => baseDeDados.cargos,
            nameof(Materia) => baseDeDados.materias,
            nameof(Curso)   => baseDeDados.cursos,
            _ => throw new InvalidOperationException(
                     "Repositorio inexistente")
        };

        recurso.Invoke(this, [repositorio, usuario]);
    }

    public async Task Cadastrar(Repositorio<T> repositorio,
                                Usuario usuario)
    {
        var usuarioTemPermissao =
            usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita);

        if (!usuarioTemPermissao)
        {
            Console.WriteLine("Você não tem permissão para realizar esta ação");

            return;
        }
        
        // TODO: Desenvolver um algoritmo para criar novos registros no sistema
        // [REQUISITO]: O usuário deve inserir os valores pertinentes
        // para cada campo do registro em questão
        //
        // Ex.: Cadastrar Matéria
        //
        // Para cadastrar uma nova Matéria, digite os campos conforme
        // o exemplo abaixo.
        //
        // NOME,TURNO,CARGA_HORARIA: Sistemas Digitais,Noturno,60
        //
        // [REQUISITO]: O usuário deve receber uma confirmação
        // antes do sistema registrar as informações
        // 
        // Ex.: 
        //
        // Deseja mesmo realizar o novo cadastro de Matéria?
        //
        // Nome: Sistemas Digitais
        // Turno: Noturno
        // Carga Horária: 60h
        //
        // [S] Sim      [N] Não:
        
        throw new NotImplementedException();
    }

    public async Task Editar(Repositorio<T> repositorio,
                             Usuario usuario)
    {
        var usuarioTemPermissao =
            usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita);

        if (!usuarioTemPermissao)
        {
            Console.WriteLine("Você não tem permissão para realizar esta ação");

            return;
        }
        
        // TODO: Desenvolver um algoritmo para atualizar registros no sistema
        // REQUISITO: O usuário deve escolher qual campo deve ser editado
        // 
        // Ex.: Editar Usuario com Login == thiago.santos
        //
        // Qual campo deseja editar? Selecione uma das opções abaixo:
        // [1] Nome
        // [2] Login
        // ...
        // Sua opção: 
        // 
        // REQUISITO: Deve ser exibida uma confirmação antes de
        // atualizar o registro, exibindo (lado a lado) o registro original
        // e o registro atualizado.
        // 
        // Ex.:
        //
        // Deseja atualizar o seguinte Usuario?
        // [Antigo]
        // Nome: Thiago
        // Login: thiago.santos
        // ...
        // [Novo]
        // Nome: Thiago Rodrigues
        // Login: thiago.santos03
        //
        // [S] Sim      [N] Não: 
        throw new NotImplementedException();
    }

    public async Task Excluir(Repositorio<T> repositorio,
                              Usuario usuario)
    {
        var temPermissao =
            usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita);

        if (!temPermissao)
        {
            Console.WriteLine("Você não tem permissão para realizar esta ação");

            return;
        }
        
        // TODO: Desenvolver um algoritmo para excluir registros do sistema
        // REQUISITO: Uma confirmação deve ser exibida antes de excluir o registro,
        // exibindo informações (em formato descritivo)
        // sobre o registro a ser excluído
        //
        // Ex.: Excluir Curso com ID == 10000000
        //
        // Deseja excluir o seguinte Curso?
        // Id: 10000000
        // Curso: Ciência da Computação
        // Turno: Noturno
        // ...
        // [S] Sim      [N] Não: 
        throw new NotImplementedException();
    }

    public void Visualizar(Repositorio<T> repositorio, Usuario usuario)
    {

        if (usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita))
        {
            // TODO: Desenvolver um algoritmo de busca e visualização para gestores
            // REQUISITO: A visualização do administrador é global e deve ser
            // em formato de relatorio
            //
            // Ex.: Visualizar Usuarios -> Exibe todos os usuários cadastrados
            // | ID | NOME    | LOGIN         | CARGO  | 
            // | 1  | Thiago  | thiago.santos | Alunos |

            throw new NotImplementedException();
        }
        
        // TODO: Desenvolver um algoritmo de busca e visualização para alunos
        // REQUISITO: A visualização do aluno deve ser em um formato descritivo
        //
        // Ex.: Visualizar Usuarios -> Exibe somente o próprio usuário
        // Nome: Thiago
        // Login: thiago.santos
        
        throw new NotImplementedException();
    }
}
