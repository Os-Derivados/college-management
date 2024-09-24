# Gestor Educacional

O Gestor Educacional é um sistema CLI que visa gerenciar os recursos internos de empresas do ramo educacional. Funciona de forma semelhante a sistemas ERP (Enterprise Resource Planning - Planejamento de recursos empresarial) porém seu escopo é delimitado para a área administrativa/acadêmica. 

## Conteúdos

* [Inicializando o Sistema](#inicializando-o-sistema)
* [Requisitos](#requisitos)
* [Estrutura do Sistema](#estrutura-do-sistema)

---

## Inicializando o Sistema

Para inicializar o sistema e carregar seus módulos, o ponto de entrada conta com alguns aspectos importantes para a configuração do mesmo: variáveis de ambiente e argumentos de linha de comando (*command line arguments*), explicados nas sessões abaixo.

Antes da primeira inicialização, abra uma janela de terminal e execute um dos seguintes blocos de comando, a depender do sistema operacional em que estiver executando.

1. No Linux (bash): 

```shell
# Obs.: substitua [username] pelo seu nome de usuário.

cd /home/username/.config/OsDerivados/CollegeManagement/
touch .env
```

2. No Windows (PowerShell):

```powershell
# Obs.: substitua [username] pelo seu nome de usuário.

cd C:\Users\username\AppData\Roaming\OsDerivados\CollegeManagement
New-Item Type -File .env
```

> **_Observação:_** Você só deve realizar esta tarefa uma única vez.

### Variáveis de Ambiente

Variáveis de ambiente são valores nomeados cuja sua presença se faz de forma externa ao programa, diferente de variáveis declaradas dentro do código fonte.

O objetivo é que, valores sujeitos à mudança ou informações críticas (como credenciais do sistema, acessos externos, etc.) não fiquem expostas em código-fonte, permitindo assim que o sistema fique protegido e flexível, contanto que *o ambiente de execução do sistema não esteja exposto*.

Todas as variáveis de ambiente se encontram definidas no módulo `Constantes.VariaveisDeAmbiente`, conforme as definições abaixo:

````csharp
public const string MasterAdminNome  = "ADMIN_USER_NAME";
public const string MasterAdminLogin = "ADMIN_USER_LOGIN";
public const string MasterAdminSenha = "ADMIN_USER_PASSWORD";

public const string UsuarioTesteNome  = "TEST_USER_NAME";
public const string UsuarioTesteLogin = "TEST_USER_LOGIN";
public const string UsuarioTesteSenha = "TEST_USER_PASSWORD";

public const string LoginTeste = "TEST_LOGIN";
````

Módulos como o `UtilitárioAmbiente` servirão como portas de entrada para acessar esses valores específicos.

> **_Importante_:** Para inicializar o sistema *pela primeira vez*, o mesmo necessita de algumas das variáveis de ambiente definidas no módulo `Constantes.VariaveisAmbiente`. Essas variáveis necessitam estar no formato padrão para arquivos `.env`, conforme o exemplo abaixo:

```shell
# Sintaxe: NOME=valor

# Define credenciais a serem utilizadas pelo Usuário Mestre
ADMIN_PASSWORD=admin12345
ADMIN_LOGIN=master.admin
ADMIN_USER=Master

# Define credenciais para acessar o sistema em modo de Desenvolvimento
TEST_USER_NAME=Usuario Teste
TEST_USER_LOGIN=usuario.teste
TEST_USER_PASSWORD=teste12345

TEST_LOGIN=usuario.teste
```

Este arquivo de configuração deve ser criado e armazenado na pasta que o utilitário `SpecialDirectories.MyDocuments` determinar. 

A localização atual na máquina no qual o sistema for instalado [varia conforme o sistema operacional](https://learn.microsoft.com/en-us/dotnet/api/Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments?view=net-8.0). A princípio, será o caminho `C:\users\username\AppData\Roaming\OsDerivados\CollegeManagement` (no Windows) ou `/home/username/.config/OsDerivados/CollegeManagement` (no Linux).

---

### Argumentos de linha de comando

Para que o sistema funcione corretamente, seu ponto de entrada, `Program.cs`, utiliza de dois argumentos repassados através do método `Main(string[] args)`. São estes: `modoDesenvolvimento` e `seed`.

* `ModoDesenvolvimento`: Habilita fluxos específicos do modo de desenvolvimento, como: pular login;
* `Seed`: Habilita a inicialização da base de dados com informações iniciais como: Usuário Mestre, Usuário Teste, cargos, cursos e matérias padrão.


Para inicializar o sistema, utilize o comando `dotnet run [ModoDesenvolvimento] [ModoSeed]`, substituindo `[modoDesenvolvimento]` e `[ModoSeed]` por valores booleanos (`true ou false`).

Ex.: 
```shell
# Funcionamento: 
# [PontoDeEntrada] [ModoDesenvolvimento] [ModoSeed]
dotnet run           true                   true 
# Nesse caso, vai tanto inicializar a base de dados quanto habilitar o modo de desenvolvimento
```

> **_Observação:_** Para a primeira vez que o sistema for iniciado, deve ser obrigatória a inicialização da base de dados. Portanto, neste caso, inicie o sistema utilizando `dotnet run false true` ou  `dotnet run true true` (caso queira depurar o sistema). 

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
        * Gerenciar Matrículas - Gestor
        * Gerenciar Cursos - Gestor
        * Gerar relatório de Grade horária - Aluno
        * Gerar relatório de Grade curricular - Aluno
        * Gerar relatório de Notas - Aluno
        * Gerar relatório de Matrícula - Aluno
        * Gerar relatório Financeiro - Aluno
    - Todas as funcionalidades são progressivas:
        * Se uma ação pode ser feita por um Aluno, ela também pode ser feita pelo Gestor ou Administrador.
        * Gestores e Adminsitradores podem acessar recursos de *todos os Alunos*, porém um Aluno deve visualizar somente informações referentes a seu próprio cadastro;
* Persistência de dados:
    - Todas as operações que alteram informações devem ser salvas separadamente, de forma que o desligamento dos sitema não acarrete na perda das informações cadastradas.
* Auditoria:
    - Todas as ações realizadas no sistema devem gerar um registro inalterável, para fins de auditoria.

### Modelagem

As entidades e relacionamentos presentes no sistema podem ser descritas através do Diagrama Entidade-Relacionamento abaixo, bem como nos esquemas descritos logo após a diagramação.

Dentro do código-fonte, toda a modelagem de dados se encontra no módulo `Dados.Modelos`

* [Modelo](#modelo)
* [Cargo](#cargo)
* [Curso](#curso)
* [Matéria](#matéria)
* [Usuário](#usuário)
* [Funcionário](#funcionário)
* [Aluno](#aluno)
* [Matrícula](#matrícula)
  * [Notas](#notas)

![](/college-management/Public/College_Management.jpg)

#### Modelo

Entidade base da qual todas as outras herdam as seguintes propriedades:

* Id

```c#
public abstract class Modelo
{
    public string? Id { get; set; }
}
```

#### Cargo

A entidade Cargo representa a função atribuída ao usuário durante o uso do sistema. Através dele, são liberados os acessos a funcionalidades individuais, com base na seguinte propriedade:

* Permissões

Um cargo pode estar associado a nenhum ou N:

* Usuário

A definição da entidade `Cargo` se encontra conforme o exemplo abaixo:

```c#
public sealed class Cargo : Modelo
{
    public string?       Nome        { get; set; }
    public List<string>  Permissoes  { get; set; }
    public List<string>? UsuariosIds { get; set; } = [];
}
```

---

#### Curso

A entidade `Curso` representa um conjunto específico e particular de Matérias que podem estar associadas a um ou mais `Aluno`s. 

Todo Curso deve estar associado a:

* Matéria

Todo Curso pode estar associado a um ou N:

* Matrícula

A definição da estrutura de um `Curso` se encontra conforme o exemplo abaixo:

```c#
public class Curso : Modelo
{
	public string?       Nome            { get; set; }
	public Materia[]     GradeCurricular { get; set; }
	public List<string>? MatriculasIds   { get; set; }
}
```

---

#### Matéria

A entidade Matéria representa uma disciplina em específico que visa estudar um determinado assunto.

Uma Matéria possui as seguintes propriedades:

* Turno
* Carga Horária

Uma matéria pode estar associada a nenhuma ou N:

* Curso
* Notas

A definição da estrutura de uma `Materia` se encontra conforme o exemplo abaixo:

```csharp
public sealed class Materia : Modelo
{
    public string? Nome         { get; set; }
    public Turno   Turno        { get; set; }
    public int     CargaHoraria { get; set; }
}
```

---

#### Usuário

A entidade base Usuário representa o usuário final que realiza acessos ao sistema. Todo Usuário é dotado das seguintes propriedades:

* Login
* Senha

Todo Usuário precisa, necessariamente, ser especializado em uma das seguintes entidades:

* Funcionário
* Aluno

Todo Usuário precisa, obrigatoriamente, possuir um:

* Cargo

A definição da estrutura de um `Usuario` se encontra abaixo:

```csharp
public class Usuario : Modelo
{
    public string? Login   { get; set; }
    public string? Nome    { get; set; }
    public string? Senha   { get; set; }
    public string  CargoId { get; set; }
}
```

---

#### Funcionário

A entidade Funcionário representa o colaborador da instituição responsável por realizar tarefas pertinentes ao escopo do sistema.

A estrutura de um `Funcionario` se encontra conforme o exemplo abaixo:

```csharp
public sealed class Funcionario : Usuario {}
```

---

#### Aluno

A entidade Aluno representa os alunos que possuem vínculo (ativo ou não) com a instituição de ensino.

Todo Aluno possui, necessariamente, uma associação para:

* Matrícula

A definição da estrutura de um `Aluno` se encontra conforme o exemplo abaixo:

```csharp
public sealed class Aluno : Usuario
{
    public string MatriculaId { get; set; }
}
```

---

#### Matrícula

A entidade Matrícula representa o vínculo entre um Aluno e um Curso. Ela associa ambas as entidades através das seguintes propriedades:

* Número
* Período
* Modalidade

Toda Matrícula possui, necessariamente, uma associação para:

* Curso

Toda Matrícula pode estar associada a nenhuma ou N:

* Notas

A estrutura de uma `Matricula` se encontra conforme o exemplo abaixo:

```csharp
public sealed class Matricula : Modelo
{
	public string?    CursoId    { get; set; }
	public string?    AlunoId    { get; set; }
	public int        Periodo    { get; set; }
	public Modalidade Modalidade { get; set; }
	public List<Nota> Notas      { get; set; } = [];
}
```

---

#### Notas

A entidade Notas representa o conjunto das notas avaliativas de uma Matéria atribuídas a um determinado Aluno.

Todo registro de Notas possui as seguintes propriedades:

* P1
* P2
* P3
* Menção

Todo registro de Notas pode estar associado a nenhuma ou N entidades:

* Matéria

Todo registro particular de Notas está asssociado unicamente a uma entidade:

* Matrícula

A estrutura de uma `Nota` se encontra conforme o exemplo abaixo:

```csharp
public sealed class Nota
{
    public string          NomeMateria     { get; set; }
    public string          MateriaId       { get; set; }
    public float?          P1              { get; set; }
    public float?          P2              { get; set; }
    public float?          P3              { get; set; }
    public double?         MediaFinal      { get; private set; }
    public SituacaoMateria SituacaoMateria { get; set; }
}
```

---

## Estrutura do Sistema

Esta seção define o funcionamento e regras de cada módulo (*namespace*) do sistema.

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
