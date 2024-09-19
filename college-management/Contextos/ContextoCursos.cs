using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;

namespace college_management.Contextos;

public class ContextoCursos : Contexto<Curso>,
                              IContextoCursos
{
    public void VerGradeHoraria(Repositorio<Curso> repositorio,
                                Usuario usuario)
    {
        // TODO: Desenvolver um algoritmo para visualização de grade horária
        // [REQUISITO]: A visualização deve ser em formato de relatório
        // 
        // Ex.: Ver Grade Horária do Curso "Ciência da Computação", 
        // 2o semestre
        //
        // Curso: Ciência da Computação
        // Semestre Atual: 2
        //
        // Grade Horária:
        //
        // | DIA           | MATÉRIA            | SALA | HORÁRIO |
        // | Segunda-Feira | Álgebra Linear     | 03   | 19:15   | 
        // | Terça-Feira   | Sistemas Digitais  | 03   | 19:15   |
        // ...

        if (usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita))
            // [REQUISITO]: A visualização do gestor deve solicitar a busca
            // de um Curso em específico na base de dados
            //
            // Ex.: Ver Grade Horária do Curso "Ciência da Computação" 
            //
            // [Ver Grade Horária]
            // Selecione um abaixo campo para realizar a busca
            //
            // [1] Nome
            // [2] Id
            // 
            // Sua opção: 1 <- Opção que o usuário escolheu 
            // ...
            //
            // Digite o nome do Curso: "Ciência da Computação"
            // ...
            //
            // [REQUISITO]: O gestor deve selecionar qual semestre do curso
            // este deseja visualizar a grade horária
            //
            // Ex.: O curso "Ciência da Computação" tem 8 semestres
            //
            // Selecione um semestre a ser visualizado (somente números).
            //
            // [1, 2, 3, 4, 5, 6, 7, 8]: 
            throw new NotImplementedException();

        // [REQUISITO]: A visualização do Aluno deve permitir somente
        // a visualização da grade horária do curso no qual ele
        // atualmente esteja vinculado
        throw new NotImplementedException();
    }

    public void VerGradeCurricular(Repositorio<Curso> repositorio,
                                   Usuario usuario)
    {
        // TODO: Desenvolver um algoritmo para visualizar a grade curricular
        // [REQUISITO]: A visualização deve mostrar as Materias de todos
        // os semestres do Curso em questão, segregados por semestre,
        // de descritiva
        //
        // Ex.: Ver Grade Curricular do Curso "Ciência da Computação"
        //
        // Curso: Ciência da Computação
        // Ano: 2024
        // 
        // 1o Semestre:
        //
        // Geometria Analítica
        // Matemática Discreta
        // Algoritmos e Programação de Computadores I
        // Tópicos de Matemática
        // Fundamentos de Lógica
        // ...

        if (usuario.Cargo.TemPermissao(
                PermissoesAcesso.PermissaoAcessoEscrita))
            // [REQUISITO]: A visualização do Gestor deve permitir a busca
            // de um Curso em específico na base de dados
            //
            // Ex.: Ver Grade Horária do Curso "Ciência da Computação" 
            //
            // [Ver Grade Horária]
            // Selecione um campo abaixo campo para realizar a busca
            //
            // [1] Nome
            // [2] Id
            // 
            // Sua opção: 1 <- Opção que o usuário escolheu 
            // ...
            //
            // Digite o nome do Curso: "Ciência da Computação" <- Nome
            // digitado pelo usuário
            // ...
            throw new NotImplementedException();

        // [REQUISITO]: A visualização do Aluno deve permitir somente
        // a visualização da grade curricular do Curso no qual ele
        // atualmente esteja vinculado

        throw new NotImplementedException();
    }

    public override async Task Cadastrar(Repositorio<Curso> repositorio,
                                         Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Editar(Repositorio<Curso> repositorio,
                                      Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override async Task Excluir(Repositorio<Curso> repositorio,
                                       Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public override void Visualizar(Repositorio<Curso> repositorio,
                                    Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
