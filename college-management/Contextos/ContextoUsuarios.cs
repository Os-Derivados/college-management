using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoUsuarios : Contexto<Usuario>,
                                IContextoUsuarios
{
    public void VerMatricula(Repositorio<Usuario> repositorio,
                             Usuario usuario)
    {
        // TODO: Desenvolver um algoritmo para visualizar Matricula de um Aluno
        // [REQUISITO]: A visualização deve ser no formato descritivo
        // 
        // Ex.: Ver Matricula 2401123415
        //
        // Nome: Thiago
        // Matricula: 2401123415
        // Curso: Ciência da Computação
        // Período: 2

        if (usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita))
            // [REQUISITO]: A visualização do Gestor deve permitir a busca
            // de um Aluno em específico na base de dados
            //
            // Ex.: Ver Matricula do Aluno com Login == "thiago.santos" 
            //
            // [Ver Grade Horária]
            // Selecione um campo abaixo campo para realizar a busca
            //
            // [1] Login
            // [2] Id
            // [3] Matricula
            // 
            // Sua opção: 1 <- Opção que o usuário escolheu 
            // ...
            //
            // Digite o Login do Aluno: thiago.santos <- Nome
            // digitado pelo Gestor
            // ...
            throw new NotImplementedException();

        // [REQUISITO]: A visualização do Aluno deve ser somente
        // da Matricula vinculada a ele

        throw new NotImplementedException();
    }

    public void VerBoletim(Repositorio<Usuario> repositorio,
                           Usuario usuario)
    {
        // TODO: Desenvolver um algoritmo para visualizar as Notas de um Aluno
        // [REQUISITO]: A visualização deve ser no formato relatório
        // 
        // Ex.: Ver Boletim do Aluno com Matricula 2401123415
        //
        // | MATERIA        | NOTA FINAL | STATUS   |
        // |----------------|------------|----------|
        // | Calculo 1      |    9.0     | Aprovado |
        // | Algebra Linear |    N/A     |   N/A    |

        if (usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita))
            // [REQUISITO]: A visualização do Gestor deve permitir a busca
            // de uma Aluno em específico na base de dados
            //
            // Ex.: Ver Boletim do Aluno com Login == "thiago.santos" 
            //
            // Selecione um campo abaixo campo para realizar a busca
            //
            // [1] Login
            // [2] Id
            // [3] Matricula
            // 
            // Sua opção: 1 <- Opção que o usuário escolheu 
            // ...
            //
            // Digite o Login do Aluno: thiago.santos <- Nome
            // digitado pelo Gestor
            // ...
            throw new NotImplementedException();

        // [REQUISITO]: A visualização do Aluno deve ser somente
        // da Matricula vinculada a ele

        throw new NotImplementedException();
    }

    public void VerFinanceiro(Repositorio<Usuario> repositorio,
                              Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Cadastrar(
        Repositorio<Usuario> repositorio,
        Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Editar(Repositorio<Usuario> repositorio,
                                      Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Excluir(Repositorio<Usuario> repositorio,
                                       Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override void Visualizar(Repositorio<Usuario> repositorio,
                                    Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
