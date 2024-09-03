# Gestor Educacional

O Gestor Educacional é um sistema CLI que visa gerenciar os recursos internos de empresas do ramo educacional. Funciona de forma semelhante a sistemas ERP (Enterprise Resource Planning - Planejamento de recursos empresarial) porém seu escopo é delimitado para a área administrativa/acadêmica. 

## Conteúdos

* [Requisitos](#requisitos)
* [Estrutura do Sistema](#estrutura-do-sistema)

---

## Inicializando o Sistema

Para inicializar o sistema e carregar seus módulos, o ponto de entrada conta com alguns aspectos importantes para a configuração do mesmo: variáveis de ambiente e argumentos de linha de comando (*command line arguments*).

### Variáveis de Ambiente

Variáveis de ambiente são valores nomeados cuja sua presença se faz de forma externa ao programa, diferente de variáveis declaradas dentro do código fonte.

O objetivo é que, valores sujeitos à mudança ou informações críticas (como credenciais do sistema, acessos externos, etc.) não fiquem expostas em código-fonte, permitindo assim que o sistema fique protegido e flexível, contanto que *o ambiente de execução do sistema não esteja exposto*.

Módulos como o `UtilitárioAmbiente` servirão como portas de entrada para acessar esses valores específicos.

Para poder inicializar o sistema pela primeira vez, o mesmo necessita de algumas variáveis de ambiente definidas nas constantes `VariaveisAmbiente`. Essas variáveis necessitam estar no formato padrão para arquivos `.env`, conforme o exemplo abaixo:

```shell
# Sintaxe: NOME=valor

# Define credenciais a serem utilizadas pelo Usuário Mestre
ADMIN_PASSWORD=admin12345
ADMIN_LOGIN=master.admin
ADMIN_USER=Master

# Define credenciais para acessar o sistema em modo de Desenvolvimento
TEST_USER='Usuario Teste'
TEST_LOGIN=usuario.teste
TEST_PASSWORD=teste12345
```

Este arquivo de configuração deve ser criado e armazenado na pasta que o utilitário `SpecialDirectories.MyDocuments` determinar. 

A localização atual na máquina no qual o sistema for instalado [varia conforme o sistema operacional](https://learn.microsoft.com/en-us/dotnet/api/Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments?view=net-8.0). A princípio, será o caminho `C:\users\username\mydocuments\CollegeManagement.OsDerivados` (no Windows) ou `/home/username/.config/CollegeManagement.OsDerivados` (no Linux).

### Argumentos de linha de comando

O ponto de entrada do sistema, `Program.cs`, utiliza de dois argumentos repassados através do método `Main(string[] args)`. São estes: `modoDesenvolvimento` e `seed`.

* `modoDesenvolvimento`: Habilita fluxos específicos do modo de desenvolvimento, como: pular login;
* `seed`: Habilita a inicialização da base de dados com informações iniciais como: Usuário Mestre, Usuário Teste, cargos, cursos e matérias padrão.


Para inicializar o sistema, utilize o comando `dotnet run [modoDesenvolvimento] [seed]`, substituindo `[modoDesenvolvimento]` e `[seed]` por valores booleanos (`true ou false`).

Ex.: 
```shell
dotnet run true true # vai tanto inicializar a base de dados quanto habilitar o modo de desenvolvimento
```

---

> **_Observação:_** Para a primeira vez que o sistema for iniciado, deve ser obrigatória a inicialização da base de dados. Portanto, neste caso, inicie o sistema utilizando `dotnet run false true` ou  `dotnet run true true` (caso queira depurar o sistema).






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
    - [Autenticação](#autenticacao)
    - [Contexto](#contexto)
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

Um *middleware* ("software intermediador", em tradução livre), é um software que costuma interceptar ações pertinentes a diferentes camadas do sistema. O College Management conta com dois principais *middlewares* para intermediar as interações do usuário com o sistema, sendo estes: 

1. Autenticação
2. Contexto

#### Middleware de Autenticação

#### Middleware de Contexto

### Serviços

Serviços são responsáveis por prover um conjunto de funcionalidades pertinentes ao modelo de negócios de um sistema.
Geralmente, englobam recursos que acessam a lógica e os dados internos do sistema, provendo asbtrações para necessidades comuns desse sistema. 

O College Management conta com os seguintes serviços:

* Serviço de Arquivos
* Serviço de Cursos
* Serviço de Usuários
* Serviço de Matérias

#### Serviço de Arquivos

#### Serviço de Cursos

#### Serviço de Usuários

#### Serviço de Matérias

### Utilitários

Utilitários englobam funcionalidades de uso geral, que não envolvem, diretamente, o acesso a lógica e dados do sistema; ou seja, envolvem funcionalidades que não são necessariamente específicas ao sistema em questão.

O College Management conta com os seguintes utilitários:

* Ambiente
* Seed

#### Utilitário de Ambiente

#### Utilitário de Seed

### Funcionalidades

No College Management, uma funcionalidade é uma estrutura responsável por interligar diferentes camadas em um determinado estado da aplicação (seja Autenticação, Contexto etc.), representando os comportamentos únicos que o sistema possui.

O College Managament conta com as seguintes funcionalidades:

1. Autenticação
2. Contexto

#### Autenticação

#### Contexto
