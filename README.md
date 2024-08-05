# Gestor Educacional

O Gestor Educacional é um sistema CLI que visa gerenciar os recursos internos de empresas do ramo educacional. Funciona de forma semelhante a sistemas ERP (Enterprise Resource Planning - Planejamento de recursos empresarial) porém seu escopo é delimitado para a área administrativa/acadêmica. 

## Conteúdos

* [Requisitos](#requisitos)
* [Estrutura do Sistema](#estrutura-do-sistema)

---

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

---

## Estrutura do Sistema

### Sumário

* [Constantes](#constantes)
* [Dados](#dados)
    - [Repositórios](#repositórios)
* [Middlewares](#middlewares)
* [Modelos](#modelos)
* [Serviços](#serviços)
* [Utilitários](#utilitários)

### Constantes

Constantes são valores absolutos e não modificáveis (isto é, não podem ser alterados após sua inicialização), representando informações pertinentes ao sistema como um todo. São informações que podem ser utilizadas em diversas camadas do sistema, visando centralizar o acesso a recursos do mesmo.

#### Acessos De Contexto

#### Cargos de Acesso

#### Permissões de Acesso

### Dados

Dados representam informações pertinentes ao sistema, sejam elas relacionadas ao domínio do negócio, sejam estruturas que auxiliam no entendimento ou utilização do sistema em seu aspecto técnico.

#### Modelos

Modelos representam informações pertinentes especificamente ao modelo de negócio. São **moldados** conforme os aspectos lógicos e regras internas de um determinado segmento comercial, o que implica que estas informações variam conforme o contexto.

Para o contexto deste projeto, o sistema conta com os seguintes modelos de dados: 

* [Cargo](#cargo)
* [Curso](#curso)
* [Matéria](#matéria)
* [Usuário](#usuário)

#### Repositórios

Um repositório (do inglês *Repository*) é um *Design Pattern* que representa uma coleção de objetos pertinentes ao negócio. 

Isto quer dizer que tal estrutura está diretamente relacionada aos modelos de dados, provendo uma interface para acessarmos um determinado recurso da base de dados, tanto para leitura quanto para escrita de informações.

Sua única finalidade é prover um conjunto de funcionalidades lógicas para realizar operações de banco de dados.

### Middlewares

Um *middleware* ("software intermediador", em tradução livre), é um software que costuma interceptar ações pertinentes a diferentes camadas do sistema.

### Serviços

### Utilitários