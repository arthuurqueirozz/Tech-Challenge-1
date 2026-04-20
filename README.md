# FIAP Cloud Games — MVP Fase 1

> API REST para a plataforma **FIAP Cloud Games (FCG)** — venda de jogos digitais e gestão da biblioteca do usuário.
> Projeto entregue como **Tech Challenge — Fase 1** da Pós-Graduação em Arquitetura de Sistemas .NET com Azure (FIAP).

---

## Sumário

- [Objetivos](#objetivos)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Arquitetura da solução](#arquitetura-da-solução)
- [Pré-requisitos](#pré-requisitos)
- [Como executar a API localmente](#como-executar-a-api-localmente)
- [Como rodar os testes automatizados](#como-rodar-os-testes-automatizados)
- [Entregáveis do Tech Challenge](#entregáveis-do-tech-challenge)
- [Grupo e participantes](#grupo-e-participantes)

---

## Objetivos

O MVP da **Fase 1** tem como objetivo entregar a base funcional e arquitetural da plataforma **FIAP Cloud Games**, atendendo aos seguintes requisitos do edital:

- **Cadastro de usuários** com validação de e-mail único e política de senha forte (mínimo 8 caracteres, incluindo números, letras e caracteres especiais).
- **Autenticação e autorização** baseada em **JWT**, com perfis distintos (`User` e `Admin`).
- **Gestão do catálogo de jogos** (CRUD) restrita ao perfil `Admin`.
- **Biblioteca do usuário**: aquisição de jogos e consulta da coleção pessoal.
- **Aplicação de promoções** sobre o catálogo pelo `Admin`.
- Aplicação dos princípios de **Domain-Driven Design (DDD)**, **Clean Architecture** e **SOLID**.
- **Logging estruturado**, **monitoramento de exceções** e **testes automatizados** (TDD) como pilares de qualidade.

---

## Tecnologias utilizadas

| Categoria | Tecnologia |
|---|---|
| Plataforma | **.NET 8** / ASP.NET Core Web API |
| Persistência | **Entity Framework Core 8** + **SQL Server** |
| Autenticação | **JWT (JSON Web Tokens)** |
| Identidade | ASP.NET Core Identity |
| Validação | **FluentValidation** |
| Logs | **Serilog** (sink Console, enriquecimento `FromLogContext`) |
| Testes | **xUnit**, FluentAssertions, Moq |
| Documentação | Swagger / OpenAPI |

---

## Arquitetura da solução

O projeto segue **Clean Architecture** com separação clara em camadas:

```
TechChallenge.sln
├── src/
│   ├── FCG.Domain            // Entidades, Value Objects, regras de negócio e contratos
│   ├── FCG.Infrastructure    // EF Core, Identity, Repositórios, Migrations
│   └── FCG.Application       // API, Controllers, DTOs, Services, Validators
└── tests/
    ├── FCG.Domain.Tests      // Testes de regras de domínio
    └── FCG.Application.Tests // Testes de serviços e validadores
```

---

## Pré-requisitos

Antes de rodar o projeto, garanta que você possui instalado:

- [**.NET 8 SDK**](https://dotnet.microsoft.com/download/dotnet/8.0) (versão 8.0.x)
- **SQL Server** acessível localmente. Pode ser:
  - **SQL Server Express / Developer** rodando na máquina, **ou**
  - **SQL Server via Docker**:
    ```bash
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Your_password" \
      -p 1433:1433 --name fcg-sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
    ```
- **EF Core CLI** (para aplicar as migrations):
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- (Opcional) **Visual Studio 2022 17.8+**, **Rider** ou **VS Code** com a extensão C# Dev Kit.

### Configuração da Connection String

A string de conexão fica em `src/FCG.Application/appsettings.json` na chave `ConnectionStrings:DefaultConnection`.
Ajuste o servidor, usuário e senha conforme o seu ambiente local:

```json
"ConnectionStrings": {
  "DefaultConnection": ""
}
```

> Para SQL Server local com autenticação integrada (Windows), use:
> `Server=SEU_HOST;Database=FcgDb;Trusted_Connection=true;TrustServerCertificate=true;`

---

## Como executar a API localmente

A partir da **raiz do repositório**:

### 1. Restaurar os pacotes NuGet

```bash
dotnet restore
```

### 2. Compilar a solução

```bash
dotnet build TechChallenge.sln -c Debug
```

### 3. Aplicar as migrations no banco de dados

As migrations ficam no projeto `FCG.Infrastructure`, porém o *startup project* é o `FCG.Application`:

```bash
dotnet ef database update \
  --project src/FCG.Infrastructure/FCG.Infrastructure.csproj \
  --startup-project src/FCG.Application/FCG.Application.csproj
```

### 4. Executar a API

```bash
dotnet run --project src/FCG.Application/FCG.Application.csproj
```

Por padrão a aplicação sobe em:

- `https://localhost:5001`
- `http://localhost:5000`

E a documentação **Swagger UI** fica disponível em:
`https://localhost:5001/swagger`

> Um usuário **Admin** padrão é criado via *seed* na primeira execução. As credenciais são logadas pelo Serilog no console durante o startup.

---

## Como rodar os testes automatizados

A suíte de testes usa **xUnit** e está organizada em dois projetos: `FCG.Domain.Tests` e `FCG.Application.Tests`.

### Executar todos os testes

```bash
dotnet test TechChallenge.sln
```

### Executar um projeto específico

```bash
dotnet test tests/FCG.Domain.Tests/FCG.Domain.Tests.csproj
dotnet test tests/FCG.Application.Tests/FCG.Application.Tests.csproj
```

### Executar com relatório de cobertura

```bash
dotnet test TechChallenge.sln --collect:"XPlat Code Coverage"
```

### Saída detalhada (útil em CI / investigação de falhas)

```bash
dotnet test TechChallenge.sln --logger "console;verbosity=detailed"
```

---

## Entregáveis do Tech Challenge

Os artefatos complementares exigidos pelo edital:

| Entregável | Link |
|---|---|
| 🎥 Vídeo de apresentação (YouTube) | **INSERIR LINK AQUI** |
| 🧠 Documentação DDD / Event Storming (Miro) | **https://miro.com/welcomeonboard/Y1RBcmExSDYzYjl6ZHJCblFZbUlrOWNFQ3dsOFQxck5qdzgzOFhWTDAwQnpReWJsWVFYOTI5QlFKbnhBVkZzelpkRE02SlFCQjRmb0N5SG5NSFZ5SzN6M3VUSStQT1F6RWhRZDJMK08xMHFGQXBQNlRkK0xVZkhwR2p2NDA3VHdhWWluRVAxeXRuUUgwWDl3Mk1qRGVRPT0hdjE=?share_link_id=462239212463** |
| 📦 Repositório público no GitHub | **https://github.com/arthuurqueirozz/Tech-Challenge-1** |

---

## Grupo e participantes

- **Nome do Grupo:** **Grupo 45**

| Nome | RM | E-mail |
|---|---|---|
| Arthur Queiroz e Silva da Costa | rm373799 | arthur.queiroz.dev@gmail.com |
| Leonardo de Medeiros Bernardes | rm373750 | lmbernardes104@gmail.com  |

---

> Projeto desenvolvido para fins acadêmicos — **FIAP | Pós-Tech — Arquitetura de Sistemas .NET com Azure — Tech Challenge Fase 1**.
