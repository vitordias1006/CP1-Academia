# 🏋️ Sistema de Gerenciamento de Academia

## 👥 Integrantes

- **Vitor Dias dos Santos** — RM: 565422
- **Enrico Delesporte** — RM: 565760
- **Felipe Modesto** — RM: 561810

---

## 📌 Domínio do Projeto

O domínio escolhido para o projeto foi **Academia**.

O sistema foi modelado para representar a estrutura de uma rede de academias, permitindo o gerenciamento de alunos, planos, fichas de treino, funcionários, unidades e demais elementos necessários para o funcionamento de uma academia moderna.

---

## 🗃️ SGBD Utilizado

**Oracle Database** — via provider `Oracle.EntityFrameworkCore`.

A connection string é configurada no `appsettings.json` sob a chave `AcademiaOracle`. Credenciais reais **não são commitadas** no repositório; utilize User Secrets ou variáveis de ambiente para fornecer a string de conexão em desenvolvimento.

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em quatro camadas:

| Camada | Projeto | Responsabilidade |
|---|---|---|
| Domain | `CP1-Academia.Domain` | Entidades e regras de negócio |
| Application | `CP1-Academia.Application` | DTOs e interfaces de repositório |
| Infrastructure | `CP1-Academia.Infrastructure` | DbContext, mapeamentos, migrations e implementações de repositório |
| API | `CP1-Academia.API` / `CP1-Academia` | Controllers, Program.cs e configuração de DI |

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

Ela contém dados como: preço, tipo de plano, data de assinatura, data de renovação, fidelidade e status ativo.

Esses planos podem ser associados aos alunos cadastrados.

### Aluno

A entidade **Aluno** representa os clientes da academia.

São armazenadas informações como: nome, CPF, e-mail, telefone, data de matrícula e status de atividade.

Cada aluno está vinculado a um plano e pode possuir uma ficha de treino específica.

### Ficha de Treino

A **Ficha de Treino** contém as informações relacionadas aos exercícios realizados pelos alunos.

Inclui dados como: exercícios, número de repetições, séries, tipo de exercício, músculo alvo e observações do instrutor.

### Aula Extra

A entidade **Aula Extra** registra aulas adicionais oferecidas pela academia (ex.: Yoga, Funcional, Spinning).

Possui informações como: tipo de aula, horário e capacidade máxima de participantes.

### Funcionário

A entidade **Funcionário** representa os colaboradores da academia.

São armazenados dados como: nome, CPF, e-mail, cargo, salário, data de contratação e status de atividade.

### Instrutor

O **Instrutor** é uma especialização da entidade Funcionário, representando os profissionais responsáveis por orientar os alunos nos treinos. Possui a informação adicional de registro profissional **CREF**.

### Gerente

A entidade **Gerente** também é uma especialização de Funcionário, representando os responsáveis pela gestão da academia. Possui informações adicionais como: comissão, período de liderança, área de responsabilidade e nível de gerência.

### Unidade da Academia

A entidade **Unidade da Academia** representa cada unidade física pertencente à rede. Ela possui: telefone, horário de funcionamento, status da unidade, vínculo com gerente, funcionários e rede de academias.

### Rede de Academia

A entidade **Rede de Academia** armazena informações sobre a organização principal que administra as unidades: nome da rede, quantidade de unidades, CNPJ e data de fundação.

### Localização

A entidade **Localização** registra o endereço das unidades: estado, cidade, bairro, CEP, rua e número.

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

**Interfaces** (camada Application — `CP1-Academia.Application/Services/`):

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

### Injeção de Dependência

Registros realizados em `Program.cs`:

```csharp
builder.Services.AddDbContext<AcademiaContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("AcademiaOracle")));

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAulaExtraRepository, AulaExtraRepository>();
// ... demais repositórios
```

---

## 🌐 Endpoints da API

Todos os controllers estão em `CP1-Academia.API/Controllers/` e seguem o padrão `api/[controller]`. Cada entidade expõe:

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `api/{entidade}` | Lista todos os registros |
| `GET` | `api/{entidade}/{id}` | Busca por ID |
| `POST` | `api/{entidade}` | Cria novo registro |
| `DELETE` | `api/{entidade}/{id}` | Remove registro |

A documentação interativa está disponível via **Swagger UI** em `/swagger` quando rodando em ambiente de desenvolvimento.

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

Em seguida, defina a connection string com seus dados do Oracle:

```bash
dotnet user-secrets set "ConnectionStrings:AcademiaOracle" "User Id=<usuario>;Password=<senha>;Data Source=<host>:<porta>/<service_name>"
```

Para verificar se o secret foi salvo corretamente:

```bash
dotnet user-secrets list
```

> ⚠️ Os User Secrets ficam armazenados localmente na máquina do desenvolvedor e **nunca são commitados** no repositório. O arquivo `appsettings.json` contém apenas um placeholder e pode ser commitado normalmente.

### 3. Aplique as migrations

De volta à raiz da solução:

```bash
dotnet ef database update --project CP1-Academia.Infrastructure --startup-project CP1-Academia.API
```

### 4. Execute a API

```bash
dotnet run --project CP1-Academia.API
```

### 5. Acesse o Swagger

Abra no navegador: `https://localhost:{porta}/swagger`

---

## 🗂️ Estrutura de Pastas (resumo)

```
CP1-Academia/
├── CP1-Academia.Domain/
│   ├── Common/BaseEntity.cs
│   └── Entities/          # Aluno, AulaExtra, FichaTreino, Funcionario, Gerente,
│                          #   Instrutor, Localizacao, Plano, RedeAcademia, UnidadeAcademia
├── CP1-Academia.Application/
│   ├── DTOs/              # Request e Response por entidade
│   └── Services/          # Interfaces de repositório
├── CP1-Academia.Infrastructure/
│   ├── Persistence/
│   │   ├── AcademiaContext.cs
│   │   └── Configurations/ # IEntityTypeConfiguration<T> por entidade
│   ├── Migrations/         # 20260418164044_Initial
│   └── *Repository.cs      # Implementações dos repositórios
└── CP1-Academia.API/
    ├── Controllers/        # Um controller por entidade
    ├── Program.cs          # DI e configuração
    └── appsettings.json
```
