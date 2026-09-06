# 🏋️ Sistema de Gerenciamento de Academia

## 👥 Integrantes

- **Vitor Dias dos Santos** — RM: 565422
- **Felipe Modesto** — RM: 561810

---

## 📌 Domínio do Projeto

O domínio escolhido para o projeto foi **Academia**.

O sistema foi modelado para representar a estrutura de uma rede de academias, permitindo o gerenciamento de alunos, planos, fichas de treino, funcionários, unidades e demais elementos necessários para o funcionamento de uma academia moderna.

---

## 🗃️ SGBD Utilizado

**Oracle Database** — via provider `Oracle.EntityFrameworkCore`.

A connection string é configurada no `appsettings.json` sob a chave `AcademiaOracle`. Credenciais reais **não são commitadas** no repositório; utilize User Secrets ou variáveis de ambiente para fornecer a string de conexão em desenvolvimento (veja a seção [Como Executar](#️-como-executar)).

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em quatro camadas:

| Camada | Projeto | Responsabilidade |
|---|---|---|
| Domain | `CP1-Academia.Domain` | Entidades, regras de negócio e exceções de domínio |
| Application | `CP1-Academia.Application` | DTOs, interfaces de repositório (específicas e genérica) |
| Infrastructure | `CP1-Academia.Infrastructure` | DbContext, mapeamentos, migrations e implementações de repositório |
| API | `CP1-Academia.API` | Controllers, Program.cs, Swagger, health checks e tratamento global de exceções |

Camadas de teste (xUnit), adicionadas no CP4:

| Projeto | Referencia | Escopo |
|---|---|---|
| `CP1-Academia.Domain.Tests` | Somente `Domain` | Regras de negócio das entidades, sem mock |
| `CP1-Academia.Application.Tests` | `Application` + `Infrastructure` | Repositórios que validam dependências (FK), com mock |

---

## 🧩 Entidades Modeladas

O modelo contém as seguintes entidades:

- Plano
- Aluno
- Ficha de Treino
- Aula Extra
- Funcionário
- Instrutor *(especialização de Funcionário)*
- Gerente *(especialização de Funcionário)*
- Unidade de Academia
- Rede de Academia
- Localização

---

## 📊 Modelo Entidade-Relacionamento (MER)

O MER apresenta:

- Entidades do sistema
- Atributos principais
- Chaves primárias (PK)
- Relacionamentos
- Cardinalidades
- Opcionalidades

---

## 📚 Descrição das Entidades

### Plano

A entidade **Plano** armazena as informações dos planos oferecidos pela academia.

Contém dados como: preço, tipo de plano, data de assinatura, data de renovação, fidelidade e status ativo. Regra de negócio: preço deve ser maior que zero, tipo de plano é obrigatório, e a data de renovação não pode ser anterior à data de assinatura.

Esses planos podem ser associados aos alunos cadastrados.

### Aluno

A entidade **Aluno** representa os clientes da academia.

São armazenadas informações como: nome, CPF, e-mail, telefone, data de matrícula e status de atividade. Regra de negócio: nome e CPF são obrigatórios, e a data de matrícula não pode ser no futuro.

Cada aluno está vinculado a um plano (validado na criação) e pode possuir uma ficha de treino específica.

### Ficha de Treino

A **Ficha de Treino** contém as informações relacionadas aos exercícios realizados pelos alunos.

Inclui dados como: exercícios, número de repetições, séries, tipo de exercício, músculo alvo e observações do instrutor. Regra de negócio: nome do exercício é obrigatório, repetições e séries devem ser maiores que zero. Vinculada a um aluno existente (validado na criação).

### Aula Extra

A entidade **Aula Extra** registra aulas adicionais oferecidas pela academia (ex.: Yoga, Funcional, Spinning).

Possui informações como: tipo de aula, horário e capacidade máxima de participantes. Regra de negócio: tipo de aula é obrigatório e capacidade deve ser maior que zero. Vinculada a uma ficha de treino existente (validado na criação).

### Funcionário

A entidade **Funcionário** representa os colaboradores da academia.

São armazenados dados como: nome, CPF, e-mail, cargo, salário, data de contratação e status de atividade. Regra de negócio: nome e CPF são obrigatórios, salário deve ser maior que zero. Vinculado a um gerente e a uma unidade existentes (validado na criação).

### Instrutor

O **Instrutor** é uma especialização da entidade Funcionário, representando os profissionais responsáveis por orientar os alunos nos treinos. Possui a informação adicional de registro profissional **CREF**. Herda as validações de `Funcionario`.

### Gerente

A entidade **Gerente** também é uma especialização de Funcionário, representando os responsáveis pela gestão da academia. Possui informações adicionais como: comissão, período de liderança, área de responsabilidade e nível de gerência. Herda as validações de `Funcionario`.

### Unidade da Academia

A entidade **Unidade da Academia** representa cada unidade física pertencente à rede. Ela possui: telefone, horário de funcionamento, status da unidade, vínculo com gerente, funcionários e rede de academias. Regra de negócio: telefone é obrigatório. Vinculada a uma rede, um gerente e uma localização existentes (validado na criação).

### Rede de Academia

A entidade **Rede de Academia** armazena informações sobre a organização principal que administra as unidades: nome da rede, quantidade de unidades, CNPJ e data de fundação. Regra de negócio: nome e CNPJ são obrigatórios, quantidade de unidades não pode ser negativa.

### Localização

A entidade **Localização** registra o endereço das unidades: estado, cidade, bairro, CEP, rua e número. Regra de negócio: estado e CEP são obrigatórios.

---

## 🔗 Relacionamentos do Sistema

| Entidades | Cardinalidade |
|---|---|
| Plano → Aluno | (1) : (N) |
| Aluno → Ficha de Treino | (1) : (1) |
| Funcionário → Instrutor | herança/especialização |
| Funcionário → Gerente | herança/especialização |
| Rede de Academia → Unidade | (1) : (N) |
| Unidade → Localização | (1) : (1) |
| Unidade → Funcionário | (1) : (N) |
| Ficha de Treino → Aula Extra | (1) : (N) |

---

## 🗄️ Persistência com EF Core (CP2)

### DbContext

O `AcademiaContext` está localizado em `CP1-Academia.Infrastructure/Persistence/` e expõe os seguintes `DbSet`s:

- `Alunos`
- `AulaExtras`
- `FichaTreinos`
- `Funcionarios`
- `Gerentes`
- `Instrutors`
- `Localizacoes`
- `Planos`
- `RedeAcademias`
- `UnidadeAcademias`

### Mapeamento — Fluent API

Cada entidade possui sua própria classe de configuração (`IEntityTypeConfiguration<T>`) em `CP1-Academia.Infrastructure/Persistence/Configurations/`:

| Arquivo | Entidade |
|---|---|
| `AlunoConfiguration.cs` | Aluno |
| `AulaExtraConfiguration.cs` | AulaExtra |
| `FichaTreinoConfiguration.cs` | FichaTreino |
| `FuncionarioConfiguration.cs` | Funcionario |
| `GerenteConfiguration.cs` | Gerente |
| `InstrutorConfiguration.cs` | Instrutor |
| `LocalizacaoConfiguration.cs` | Localizacao |
| `PlanoConfiguration.cs` | Plano |
| `RedeAcademiaConfiguration.cs` | RedeAcademia |
| `UnidadeAcademiaConfiguration.cs` | UnidadeAcademia |

As configurações definem explicitamente: nomes de tabelas, PKs, tipos de coluna, `maxLength`, `IsRequired`, relacionamentos com `HasOne`/`WithMany`/`HasForeignKey` e comportamento de deleção (`OnDelete`).

### Migration

Uma migration inicial foi gerada e cobre o esquema completo:

```
20260418164044_Initial
```

Para aplicar ao banco:

```bash
dotnet ef database update --project CP1-Academia.Infrastructure --startup-project CP1-Academia.API
```

### Repositórios

**Interfaces específicas** (camada Application — `CP1-Academia.Application/Services/`):

- `IAlunoRepository`
- `IAulaExtraRepository`
- `IFichaTreinoRepository`
- `IFuncionarioRepository`
- `IGerenteRepository`
- `IInstrutorRepository`
- `ILocalizacaoRespository`
- `IPlanoRepository`
- `IRedeAcademiaRepository`
- `IUnidadeAcademiaRepository`

**Implementações** (camada Infrastructure — `CP1-Academia.Infrastructure/`):

- `AlunoRepository`
- `AulaExtraRepository`
- `FichaTreinoRepository`
- `FuncionarioRepository`
- `GerenteRepository`
- `InstrutorRepository`
- `LocalizacaoRepository`
- `PlanoRepository`
- `RedeAcademiaRepository`
- `UnidadeAcademiaRepository`

### Repositório Genérico (CP3)

Além dos repositórios específicos acima, a solução implementa um contrato genérico:

- `IRepository<T>` — interface (Application), restrita a `T : BaseEntity`, com `GetAll`, `GetById`, `Add`, `Update`, `Delete`, `ExistsById`.
- `Repository<T>` — implementação (Infrastructure) usando EF Core (`DbSet<T>`, `AsNoTracking` em leituras).

Registro na DI:
```csharp
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

**Uso demonstrado:**
- `AlunoController.DeleteGenerico` — remoção de aluno via `IRepository<Aluno>`.
- Validação de dependências (FK) nos repositórios específicos antes de persistir, injetando `IRepository<T>` da entidade referenciada:
  - `AlunoRepository` → `IRepository<Plano>` (valida `PlanoId`)
  - `AulaExtraRepository` → `IRepository<FichaTreino>` (valida `FichaTreinoId`)
  - `FichaTreinoRepository` → `IRepository<Aluno>` (valida `AlunoId`)
  - `FuncionarioRepository` → `IRepository<Gerente>` + `IRepository<UnidadeAcademia>`
  - `UnidadeAcademiaRepository` → `IRepository<RedeAcademia>` + `IRepository<Gerente>` + `IRepository<Localizacao>`

### Injeção de Dependência

Registros realizados em `Program.cs`:

```csharp
builder.Services.AddDbContext<AcademiaContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("AcademiaOracle")));

builder.Services.AddAcademiaHealthChecks();

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAulaExtraRepository, AulaExtraRepository>();
// ... demais repositórios específicos

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
```

---

## 🌐 Endpoints da API

Todos os controllers estão em `CP1-Academia.API/Controllers/` e seguem o padrão `api/[controller]`. Cada entidade expõe:

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `api/{entidade}` | Lista todos os registros (sempre 200, lista pode ser vazia) |
| `GET` | `api/{entidade}/{id}` | Busca por ID (404 se não encontrado) |
| `POST` | `api/{entidade}` | Cria novo registro (400 se dados inválidos; 404 se dependência referenciada não existir) |
| `DELETE` | `api/{entidade}/{id}` | Remove registro (404 se não encontrado) |

O `AlunoController` expõe adicionalmente:

| Método | Rota | Descrição |
|---|---|---|
| `DELETE` | `api/aluno/generico/{id}` | Remove um aluno usando o repositório genérico `IRepository<Aluno>` |

Todos os endpoints estão documentados no Swagger com comentários XML (`<summary>`, `<param>`, `<response>`) e `[ProducesResponseType]` para cada código de retorno possível.

A documentação interativa está disponível via **Swagger UI** em `/swagger` quando rodando em ambiente de desenvolvimento.

---

## ⚠️ Tratamento Global de Exceções (CP3)

Todas as exceções não tratadas pelos controllers são interceptadas pelo
`GlobalExceptionHandler` (`CP1-Academia.API/Exceptions/GlobalExceptionHandler.cs`),
que implementa `IExceptionHandler` e converte a exceção em uma resposta padrão
**RFC 7807** (`ProblemDetails`, `Content-Type: application/problem+json`), sempre
incluindo o `traceId` da requisição.

Em produção, o campo `detail` de erros 500 não expõe mensagem interna nem stack
trace; em Development, o stack trace é incluído em `Extensions["stackTrace"]`
para facilitar a depuração.

### Mapeamento de Exceções → Status HTTP

| Exceção | Status HTTP | Quando ocorre |
|---|---|---|
| `ArgumentException` | 400 | Requisição malformada |
| `DomainException` | 400 | Regra de negócio violada (ex.: campo obrigatório vazio, valor fora do intervalo válido) |
| `ResourceNotFoundException` / `KeyNotFoundException` | 404 | Recurso solicitado ou dependência referenciada (FK) não existe |
| `ConflictException` | 409 | Conflito de dados (ex.: duplicidade) |
| Qualquer outra exceção | 500 | Erro não mapeado — mensagem genérica fora de Development |

As exceções de domínio (`DomainException`, `ResourceNotFoundException`,
`ConflictException`) estão em `CP1-Academia.Domain/Exceptions/`.

---

## 🩺 Health Checks (CP4)

A API expõe um único endpoint de health check:

```
GET /health
```

Retorna um JSON detalhado (via `HealthCheckWriter`, em
`CP1-Academia.API/HealthChecks/`) com o status geral, duração total e o
detalhamento de cada check (nome, status, duração e, apenas em Development,
mensagem de erro).

| Check | O que verifica |
|---|---|
| `self` | Se o processo da API está no ar (`HealthCheckResult.Healthy`) |
| `oracle-db` | Conectividade com o banco Oracle, via `AddDbContextCheck<AcademiaContext>` |

**Status HTTP retornados:**
- `Healthy` → **200**
- `Degraded` → **200** (ainda serve tráfego, com aviso)
- `Unhealthy` → **503**

Exemplo de formato da resposta:
```json
{
  "status": "Healthy",
  "totalDurationMs": 12.4,
  "checks": [
    { "name": "self", "status": "Healthy", "durationMs": 0.1, "description": "API no ar", "error": null },
    { "name": "oracle-db", "status": "Healthy", "durationMs": 12.1, "description": null, "error": null }
  ]
}
```

Configuração via extensão (`HealthCheckServiceExtensions.AddAcademiaHealthChecks`)
para não inchar o `Program.cs`. Evidências (Healthy/Unhealthy) em
[`/docs/health-checks`](./docs/health-checks) — *a preencher com prints/JSON reais*.

---

## 📋 Observabilidade — Logs (CP4)

A API usa `ILogger<T>` nativo do ASP.NET Core, com **logs estruturados**
(propriedades nomeadas, sem concatenação de string) e correlação via
`HttpContext.TraceIdentifier`.

- **Fluxo de escrita instrumentado:** `POST /api/aluno` — loga início e sucesso
  da criação, incluindo `Nome`/`AlunoId` e `TraceId`.
- **GlobalExceptionHandler:** loga toda exceção não tratada em nível `Error`,
  incluindo o mesmo `traceId` retornado na resposta `ProblemDetails`.

Evidência de log de uma requisição bem-sucedida e de uma exceção tratada em
[`/docs/logs`](./docs/logs) — *a preencher com saída real do console*.

---

## 🧪 Testes Automatizados — xUnit (CP4)

A solução contém dois projetos de teste, ambos incluídos na `.sln`:

| Projeto | Referencia | Estratégia |
|---|---|---|
| `CP1-Academia.Domain.Tests` | Somente `CP1-Academia.Domain` | Sem mock — `[Fact]` (caminho feliz) + `[Theory]`/`[InlineData]` (caminho de erro), padrão AAA explícito |
| `CP1-Academia.Application.Tests` | `Application` + `Infrastructure` | Com Moq — mocka `IRepository<T>` das dependências (FK) e usa EF Core InMemory para isolar o `DbContext` |

### Cobertura — Domain.Tests (regras de negócio, sem mock)

| Classe de teste | Entidade | Cenários |
|---|---|---|
| `AlunoTests` | `Aluno` | Nome/CPF obrigatórios, data de matrícula não pode ser futura |
| `AulaExtraTests` | `AulaExtra` | Tipo de aula obrigatório, capacidade > 0 |
| `FichaTreinoTests` | `FichaTreino` | Exercício obrigatório, repetições/séries > 0 |
| `FuncionarioTests` | `Funcionario` | Nome/CPF obrigatórios, salário > 0 |
| `GerenteTests` | `Gerente` | Herda validação de `Funcionario` |
| `InstrutorTests` | `Instrutor` | Herda validação de `Funcionario` |
| `LocalizacaoTests` | `Localizacao` | Estado/CEP obrigatórios |
| `PlanoTests` | `Plano` | Preço > 0, tipo obrigatório, datas coerentes |
| `RedeAcademiaTests` | `RedeAcademia` | Nome/CNPJ obrigatórios, quantidade ≥ 0 |
| `UnidadeAcademiaTests` | `UnidadeAcademia` | Telefone obrigatório |

### Cobertura — Application.Tests (validação de dependências, com mock)

| Classe de teste | Repositório | Dependência mockada | Cenários |
|---|---|---|---|
| `AlunoRepositoryTests` | `AlunoRepository` | `IRepository<Plano>` | Plano inexistente → `ResourceNotFoundException`, `Times.Never` de persistência; plano existente → persiste, `Times.Once` |
| `AulaExtraRepositoryTests` | `AulaExtraRepository` | `IRepository<FichaTreino>` | Idem, para ficha de treino |
| `FichaTreinoRepositoryTests` | `FichaTreinoRepository` | `IRepository<Aluno>` | Idem, para aluno |
| `FuncionarioRepositoryTests` | `FuncionarioRepository` | `IRepository<Gerente>` + `IRepository<UnidadeAcademia>` | Testa cada dependência ausente isoladamente, e o caminho feliz com ambas presentes |
| `UnidadeAcademiaRepositoryTests` | `UnidadeAcademiaRepository` | `IRepository<RedeAcademia>` + `IRepository<Gerente>` + `IRepository<Localizacao>` | Idem, para as três dependências |

### Executar os testes

Na raiz da solução:
```bash
dotnet test
```

Evidência da execução em [`/docs/tests`](./docs/tests) — *a preencher com a saída real*.

---

## ⚙️ Como Executar

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd CP1-Academia
```

### 2. Configure a connection string via User Secrets

O projeto utiliza **User Secrets** para manter credenciais fora do repositório. O `UserSecretsId` já está configurado no `.csproj` da API (`9c7a04f0-96a0-46b9-97a3-7e388b8953db`).

Navegue até o projeto da API e inicialize os secrets:

```bash
cd CP1-Academia.API
dotnet user-secrets init
```

Em seguida, defina a connection string com seus dados do Oracle (formato FIAP):

```bash
dotnet user-secrets set "ConnectionStrings:AcademiaOracle" "User Id=<RM>;Password=<senha>;Data Source=oracle.fiap.com.br:1521/orcl"
```

Para verificar se o secret foi salvo corretamente:

```bash
dotnet user-secrets list
```

> ⚠️ Os User Secrets ficam armazenados localmente na máquina do desenvolvedor e **nunca são commitados** no repositório. O arquivo `appsettings.json` contém apenas um placeholder e pode ser commitado normalmente. Cada integrante do grupo precisa rodar esse comando na própria máquina ao clonar o projeto.

### 3. Aplique as migrations

De volta à raiz da solução:

```bash
dotnet ef database update --project CP1-Academia.Infrastructure --startup-project CP1-Academia.API
```

### 4. Execute a API

```bash
dotnet run --project CP1-Academia.API
```

### 5. Acesse o Swagger e o Health Check

- Swagger: `https://localhost:{porta}/swagger`
- Health Check: `https://localhost:{porta}/health`

### 6. Rode os testes automatizados

Na raiz da solução:
```bash
dotnet test
```

---

## 🗂️ Estrutura de Pastas (resumo)

```
CP1-Academia/
├── CP1-Academia.Domain/
│   ├── Common/BaseEntity.cs
│   ├── Entities/            # Aluno, AulaExtra, FichaTreino, Funcionario, Gerente,
│   │                        #   Instrutor, Localizacao, Plano, RedeAcademia, UnidadeAcademia
│   └── Exceptions/          # DomainException, ResourceNotFoundException, ConflictException
├── CP1-Academia.Application/
│   ├── DTOs/                # Request e Response por entidade
│   └── Services/            # Interfaces de repositório específicas + IRepository<T> genérica
├── CP1-Academia.Infrastructure/
│   ├── Persistence/
│   │   ├── AcademiaContext.cs
│   │   └── Configurations/  # IEntityTypeConfiguration<T> por entidade
│   ├── Migrations/          # 20260418164044_Initial
│   ├── Repository.cs        # Implementação genérica IRepository<T>
│   └── *Repository.cs       # Implementações específicas por entidade
├── CP1-Academia.API/
│   ├── Controllers/         # Um controller por entidade, com XML comments
│   ├── Exceptions/          # GlobalExceptionHandler
│   ├── HealthChecks/        # HealthCheckServiceExtensions, HealthCheckWriter
│   ├── Program.cs           # DI, Swagger, health checks e exception handler
│   └── appsettings.json
├── CP1-Academia.Domain.Tests/       # Testes de regra de negócio (xUnit, sem mock)
├── CP1-Academia.Application.Tests/  # Testes de repositório (xUnit + Moq)
└── docs/
    ├── health-checks/       # Evidências de /health (Healthy/Unhealthy)
    ├── logs/                # Evidências de logs estruturados
    └── tests/                # Evidências de dotnet test
```
