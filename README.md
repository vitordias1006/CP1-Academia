# 🏋️ Sistema de Gerenciamento de Academia

> Evolução do checkpoint CP4 com versionamento de API, paginação e rate limit (CP5).

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

Propriedades booleanas (`Ativo`, `Fidelidade`) são mapeadas explicitamente com `HasConversion<int>()`, já que o Oracle não possui tipo `BOOLEAN` nativo em tabelas — o EF Core envia `0`/`1` em vez de um literal booleano, evitando o erro `ORA-06550 / PLS-00382` na geração de comandos SQL.

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

> Nota de implementação: `ExistsById` é implementado com `Count(...) > 0` em vez de `Any(...)`, pois a tradução padrão de `Any()` pelo provider Oracle gerava literais booleanos incompatíveis com o dialeto SQL do Oracle (`ORA-00904`).

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

## 🔀 Versionamento de API (CP5)

Recurso escolhido para versionamento: **Aluno** — é o que já tinha `GET` de listagem desde o CP3 e o que mais cresce no domínio.

| Versão | Status | `GET` de listagem |
|---|---|---|
| **1.0** | ⚠️ Deprecada | Array simples (contrato antigo do CP3), **sem paginação** |
| **2.0** | Atual (padrão quando a versão não é informada) | Envelope paginado (ver seção [Paginação](#-paginação-cp5)) |

As duas versões chamam o **mesmo** `IAlunoRepository` — não há regra de negócio duplicada por versão. Os demais 9 controllers (`AulaExtra`, `FichaTreino`, `Funcionario`, `Gerente`, `Instrutor`, `Localizacao`, `Plano`, `RedeAcademia`, `UnidadeAcademia`) são marcados com `[ApiVersionNeutral]`: continuam respondendo normalmente com qualquer versão informada (ou sem nenhuma), e aparecem nos dois grupos do Swagger. `GetById`, `POST` e `DELETE` do Aluno também não têm versão fixa, então funcionam tanto na 2.0 (padrão) quanto informando `api-version=1.0` explicitamente.

### Como o cliente informa a versão

| Forma | Exemplo |
|---|---|
| Query string | `GET /api/Aluno?api-version=1.0` |
| Header | `GET /api/Aluno` + `X-Api-Version: 1.0` |
| Segmento de URL | `GET /api/v1/Aluno` / `GET /api/v2/Aluno` |
| Omitida | `GET /api/Aluno` → cai na **2.0** |

Todas as respostas do recurso Aluno trazem os headers `api-supported-versions` e `api-deprecated-versions` (`ReportApiVersions = true`), confirmando quais versões existem e quais estão deprecadas.

### URLs do recurso (ambiente local)

| Recurso | URL |
|---|---|
| Swagger | `http://localhost:5012/swagger` |
| Health check | `http://localhost:5012/health` |
| Listagem v1 (lista antiga) | `http://localhost:5012/api/Aluno?api-version=1.0` |
| Listagem v2 (envelope paginado) | `http://localhost:5012/api/v2/Aluno` (ou `http://localhost:5012/api/Aluno`, sem versão) |

Exemplo real de resposta da v1 (array simples):
```json
[
  {"id":"f3ea8df3-0577-4c2a-be63-d14b60c55603","nome":"Artur","cpf":"00011122233","email":"artur@gmail.com","telefone":"11999991111","dataMatricula":"2026-09-07T11:56:13.501","ativo":true,"planoId":"8337ce30-8685-431c-8fd3-6e9e9971b6c3"},
  {"id":"29b849a4-46ad-4570-b8d7-9c3926e26700","nome":"Italo","cpf":"11122233344","email":"italo@gmail.com","telefone":"11999881111","dataMatricula":"2026-09-07T10:22:13.501","ativo":true,"planoId":"8337ce30-8685-431c-8fd3-6e9e9971b6c3"}
]
```

Swagger em Development lista dois grupos no seletor de versão: **v2.0** e **v1.0 (DEPRECADA)**, com a descrição do grupo v1.0 explicitando a depreciação e o endpoint `GET /api/Aluno` daquele grupo marcado como obsoleto.

---

## 📄 Paginação (CP5)

Somente a listagem **v2** do Aluno é paginada — a v1 preserva o contrato antigo (array simples), evitando um breaking change silencioso.

| Parâmetro | Padrão | Regra |
|---|---|---|
| `page` | `1` | inteiro ≥ 1 |
| `pageSize` | `20` | inteiro entre **1 e 100** (teto) |

- `page < 1` ou `pageSize` fora de 1–100 → **400** (`application/problem+json`, reaproveitando o `GlobalExceptionHandler` do CP3 — a validação lança `ArgumentException` em `PageRequest.Create`).
- Página além do total de registros → **200** com `items: []` (não é erro).
- O corte é feito **no banco**: `AlunoRepository.GetPaged` executa `Count` + `OrderBy(Nome).ThenBy(Id)` + `Skip` + `Take` diretamente no `IQueryable`, materializando (`ToList`) só os registros da página.

### Corpo da resposta 200 (v2)

```json
{
  "page": 1,
  "pageSize": 20,
  "totalItems": 4,
  "totalPages": 1,
  "items": [ /* ... */ ],
  "hasPrevious": false,
  "hasNext": false
}
```

### Validações reais executadas

| Caso | Requisição | Resultado |
|---|---|---|
| `page` inválido | `GET /api/v2/Aluno?page=0` | `400` — *"O parâmetro 'page' deve ser um inteiro maior ou igual a 1."* |
| `pageSize` inválido | `GET /api/v2/Aluno?pageSize=9999` | `400` — *"O parâmetro 'pageSize' deve estar entre 1 e 100."* |
| Página além do total | `GET /api/v2/Aluno?page=999&pageSize=20` | `200` com `items: []`, `totalItems: 4`, `totalPages: 1` |
| Paginação normal | `GET /api/v2/Aluno?page=1&pageSize=2` e `page=2&pageSize=2` | Itens distintos nas duas páginas, sem sobreposição |

Evidências completas (headers e corpo) em [`/docs`](./docs).

---

## 🚦 Rate Limit (CP5)

| Item | Valor |
|---|---|
| Endpoint limitado | `POST /api/Aluno` |
| Política | **Fixed window**, nativa do ASP.NET Core (`Microsoft.AspNetCore.RateLimiting`) |
| Limite / janela | **10 requisições por 1 minuto**, particionado por IP do cliente |
| Ao estourar | **429 Too Many Requests** + header **`Retry-After`** (segundos) + corpo `application/problem+json` |
| `GET /health` | **Fora do teto** (`.DisableRateLimiting()`) — continua respondendo normalmente mesmo com o `POST` limitado |

Exemplo real de resposta ao estourar o limite:
```http
HTTP/1.1 429 Too Many Requests
Content-Type: application/problem+json
Retry-After: 60

{
  "type": "https://httpstatuses.com/429",
  "title": "Muitas requisições",
  "status": 429,
  "detail": "Limite de 10 requisições por minuto excedido. Tente novamente em 60 segundo(s).",
  "traceId": "0HNOVLQ5ASFSM:00000001",
  "retryAfterSeconds": 60
}
```

Logo em seguida, `GET /health` confirmado em **200 Healthy** (`self` e `oracle-db` saudáveis), provando que o health check não divide o mesmo teto do endpoint de escrita.

Evidências completas em [`/docs`](./docs).

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

Configuração via extensão (`HealthCheckServiceExtensions.AddAcademiaHealthChecks`)
para não inchar o `Program.cs`.

**Evidências reais (Healthy e Unhealthy)** documentadas em
[`/docs/health-checks`](./docs/health-checks), incluindo:
- `GET /health` retornando **200 OK** com `self` e `oracle-db` como `Healthy`.
- `GET /health` retornando **503 Service Unavailable** com `oracle-db` como
  `Unhealthy` (simulado localmente via connection string inválida) enquanto `self`
  permanece `Healthy`, confirmando que o relatório agregado reflete corretamente
  a indisponibilidade da dependência.

---

## 📋 Observabilidade — Logs (CP4)

A API usa `ILogger<T>` nativo do ASP.NET Core, com **logs estruturados**
(propriedades nomeadas, sem concatenação de string) e correlação via
`HttpContext.TraceIdentifier`.

- **Fluxo de escrita instrumentado:** `POST /api/aluno` — loga início e sucesso
  da criação, incluindo `Nome`/`AlunoId` e `TraceId`.
- **GlobalExceptionHandler:** loga toda exceção não tratada em nível `Error`,
  incluindo o mesmo `traceId` retornado na resposta `ProblemDetails`.

**Evidência real** de um `POST /api/aluno` bem-sucedido, com o mesmo `TraceId`
correlacionando a linha de início e a linha de sucesso, documentada em
[`/docs/logs`](./docs/logs). Evidência do cenário de exceção tratada pelo
`GlobalExceptionHandler` em processo de coleta.

---

## 🧪 Testes Automatizados — xUnit (CP4 + CP5)

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

### Cobertura — Paginação (CP5, em `Application.Tests`)

| Classe de teste | Cobertura |
|---|---|
| `PaginacaoTests` | `[Theory]`/`[InlineData]` para `page`/`pageSize` inválidos (lança `ArgumentException`); `[Fact]` para valores válidos e para os padrões (`page=1`, `pageSize=20`); `[Fact]` para `GetPaged` comprovando que página 1 e página 2 não se sobrepõem e que `totalPages` fecha com `totalItems`; `[Fact]` para página além do total retornando `items` vazio sem erro. Roda com EF Core InMemory, sem subir API nem banco Oracle. |

### Executar os testes

Na raiz da solução:
```bash
dotnet test
```

**Resultado real da execução: 72/72 testes aprovados, 0 falhas.**
Evidência completa da saída em [`/docs/tests`](./docs/tests).

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

- Swagger: `http://localhost:{porta}/swagger`
- Health Check: `http://localhost:{porta}/health`

> No Windows, prefira testar com `curl` usando `http://` em vez de `https://` para
> evitar erros de negociação TLS (`schannel`) com o certificado de desenvolvimento.

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
│   │   ├── PageRequest.cs   # CP5 — valida page/pageSize
│   │   └── PagedResult.cs   # CP5 — envelope paginado
│   └── Services/            # Interfaces de repositório específicas + IRepository<T> genérica
├── CP1-Academia.Infrastructure/
│   ├── Persistence/
│   │   ├── AcademiaContext.cs
│   │   └── Configurations/  # IEntityTypeConfiguration<T> por entidade
│   ├── Migrations/          # 20260418164044_Initial
│   ├── Repository.cs        # Implementação genérica IRepository<T>
│   └── *Repository.cs       # Implementações específicas por entidade (AlunoRepository.GetPaged — CP5)
├── CP1-Academia.API/
│   ├── Controllers/         # Um controller por entidade, com XML comments
│   ├── Exceptions/          # GlobalExceptionHandler
│   ├── HealthChecks/        # HealthCheckServiceExtensions, HealthCheckWriter
│   ├── Swagger/              # CP5 — ConfigureSwaggerOptions (um doc por versão)
│   ├── RateLimiting/          # CP5 — RateLimitingExtensions (fixed window)
│   ├── Program.cs           # DI, versionamento, rate limit, Swagger, health checks e exception handler
│   └── appsettings.json
├── CP1-Academia.Domain.Tests/       # Testes de regra de negócio (xUnit, sem mock)
├── CP1-Academia.Application.Tests/  # Testes de repositório + paginação (xUnit + Moq) — PaginacaoTests.cs (CP5)
└── docs/
    ├── health-checks/       # Evidências de /health (Healthy/Unhealthy)
    ├── logs/                # Evidências de logs estruturados
    ├── tests/                # Evidências de dotnet test
    └── (evidências CP5: versionamento, paginação e rate limit — ver seções acima)
```
