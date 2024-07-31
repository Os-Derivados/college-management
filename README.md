# Gestor Educacional

O gerenciador de Escola é um sistema que visa gerenciar os recursos internos de empresas do ramo educacional. Funciona de forma semelhante a sistemas ERP (Enterprise Resource Planning - Planejamento de recursos empresarial) porém seu escopo é delimitado para a área administrativa/acadêmica. 

Através deste sitema, é possível realizar uma série de atividades que estão descritas abaixo.

## Requisitos

* Realizar login:
    - O login é capaz de autenticar usuários para liberar um determinado contexto, baseado em um gerenciamento de permissões;
* Acesso baseado em cargos:
    - Com base nos cargos atribuidos ao usuário logado, uma série de funcionalidades estarão liberadas ou ocultas ao mesmo;
    - Os principais cargos serão: Aluno, Gestor, Administrador
    - As seguintes funcionalidades estarão atreladas a determinados cargos:
        * Cadastrar Administradores - Administrador
        * Cadastrar Gestores - Administrador
        * Cadastrar Alunos - Gestor
        * Gerenciar matrículas - Gestor
        * Gerenciar cursos - Gestor
        * Gerar relatório de grade horária - Aluno
        * Gerar relatório de grade curricular - Aluno
        * Gerar relatório de notas - Aluno
        * Gerar relatório de matrícula - Aluno
        * Gerar relatório financeiro - Aluno
    - Todas as funcionalidades são progressivas:
        * Se uma ação pode ser feita por um aluno, ela também pode ser feita pelo gestor ou administrador.
        * Gestores e adminsitradores podem acessar recursos de todos os alunos, porém um aluno deve visualizar somente informações referentes a seu próprio cadastro;
* Persistência de dados:
    - Todas as operações que alteram informações devem ser salvas separadamente, de forma que o desligamento dos sitema não acarrete na perda das informações cadastradas.
* Auditoria:
    - Todas as ações realizadas no sistema devem gerar um registro inalterável, para fins de auditoria.