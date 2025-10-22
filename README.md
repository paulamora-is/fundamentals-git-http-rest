# 🇧🇷 fundamentals-git-http-rest

## 📖 Índice
- [Introdução](#👩‍💻-introdução)
  - [Instalação e Configuração do .NET](#instalação-e-configuração-do-net)
- [Quick start](#🚀-quick-start)
- [Run & Debug (VS Code)](#🏃‍➡️-run--debug-vs-code)
  - [Requests e Postman (HTTPS)](#requests-e-postman-https)
- [Estrutura do projeto e convenções REST](#🗂️-estrutura-do-projeto-e-convenções-rest)
- [Conceitos fundamentais](#🧠-conceitos-fundamentais)
  - [Conceitos (.NET) - SDK, Runtime, Framework, LTS e CLI](#conceitos-net---sdk-runtime-framework-lts-e-cli)
  - [Framework](#framework)
  - [LTS e STS](#lts-e-sts)
  - [Versionamento Semântico](#versionamento-semantico)
  - [Runtime](#runtime)
  - [SDK (Software Development Kit)](#sdk-software-development-kit)
  - [.NET CLI (Command Line Interface)](#net-cli-command-line-interface)
  - [Estrutura de um Projeto .NET](#estrutura-de-um-projeto-net)
  - [Arquivos de configuração (exemplos)](#arquivos-de-configuração-exemplos)
    - [Exemplo de .csproj](#exemplo-de-csproj)
    - [Exemplo de launchSettings.json](#exemplo-de-launchsettingsjson)
    - [Exemplo de launch.json](#exemplo-de-launchjson)
    - [Exemplo de tasks.json](#exemplo-de-tasksjson)
  - [Estrutura de Pastas](#estrutura-de-pastas)
  - [Arquivos dentro de /bin](#arquivos-dentro-de-bin)
  - [Arquivos dentro de /obj](#arquivos-dentro-de-obj)
- [Conceitos HTTP](#🌍-conceitos-http)
  - [Componentes](#componentes)
  - [Estrutura de uma requisição](#estrutura-de-uma-requisição)
  - [Estrutura de uma resposta](#estrutura-de-uma-resposta)
- [Conceitos REST](#🌐-conceitos-rest)
  - [Princípios](#princípios)
  - [Boas práticas de nomeação](#boas-práticas-de-nomeação)
- [Formato JSON (conceito e boas práticas)](#formato-json--conceito-e-boas-práticas)
- [HTTP Status Codes e IActionResult](#http-status-codes-e-iactionresult)
- [Conceitos Git e Versionamento](#🧩-conceitos-git-e-versionamento)
  - [Boas práticas](#boas-práticas)
  - [Git — Comandos](#git--comandos)
  - [Conceitos GitHub e GitFlow](#🌀-conceitos-github-e-gitflow)
  - [Fluxo GitFlow Prático](#fluxo-gitflow-prático)
- [Referências](#📖-referências)

## 👩‍💻 Introdução

Este repositório contém o projeto **fundamentals-git-http-rest**, criado para estudar os fundamentos de:

- APIs REST com .NET 9
- Comunicação via HTTP
- Estrutura em camadas (Controller, Service, Infra, Model)
- Boas práticas de rotas, versionamento e código limpo
- Git, GitHub e GitFlow para versionamento profissional

### Instalação e Configuração do .NET

| Etapa | Comando/Ação | Observação |
|---|---|---|
| Verificar se tem .NET	| `dotnet --version` | Mostra a versão instalada |
| Instalar via CMD | `winget install Microsoft.DotNet.SDK.9` | Instala o SDK 9 |
| Instalar via site | [Download oficial](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0) | Método visual |
| Confirmar instalação | `dotnet --info` | Exibe detalhes do SDK |

## 🚀 Quick start

1. Abra a pasta do projeto no VS Code:
   c:\Users\... \fundamentals-git-http-rest
2. Restaurar dependências e compilar:
   ```bash
   dotnet restore
   dotnet build
   ```
3. Executar localmente (fora do debug):
   ```bash
   cd fundamentals.git.http.rest.api
   dotnet run --urls "https://localhost:7070;http://localhost:5199"
   ```
4. Endpoints principais:
   - GET /api/entries
   - GET /api/entries/{id}
   - POST /api/entries
   - PUT /api/entries/{id}
   - DELETE /api/entries/{id}

## 🏃‍➡️ Run & Debug (VS Code)

Recomendações para debugar e garantir que a API seja exposta nas portas desejadas:

1. Em Program.cs verifique:
```csharp
var builder = WebApplication.CreateBuilder(args);
// ... registrar services ...
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

2. Atualize `.vscode/tasks.json` para construir o csproj correto (label "build") e garanta que o `preLaunchTask` do launch.json aponte para essa label.

3. Em `.vscode/launch.json` defina `env.ASPNETCORE_URLS` para forçar Kestrel a escutar nas portas desejadas (ex.: "https://localhost:7070;http://localhost:5199").

4. Rebuild (Ctrl+Shift+B) e inicie debug (F5). Procure no Debug Console/Terminal por:
```
Now listening on: https://localhost:7070
```

5. Se for a primeira vez com HTTPS local:
```bash
dotnet dev-certs https --trust
```

### Requests e Postman (HTTPS)

- Se Postman falhar com HTTPS por certificado autoassinado:
  - **Teste via HTTP:** http://localhost:5199/api/entries
  - **Desativar verificação SSL no Postman:** Settings → General → "SSL certificate verification" = OFF
  - **Ou confiar no certificado dev:** dotnet dev-certs https --trust
- Verifique Postman Console (View → Show Postman Console) para detalhes (TLS, proxy, connection refused).
- Se houver proxy corporativo, adicione localhost/127.0.0.1 em “No Proxy”.

## 🗂️ Estrutura do projeto e convenções REST
- **Camadas:**
  - **Model** — definição das entidades.
  - **Infra** — persistência / mock data.
  - **Service** — regras de negócio.
  - **Controller** — endpoints HTTP.
- **Convenções:**
  - **GET /api/entries** → lista (com filtro via query ?title=).
  - **GET /api/entries/{id}** → por id.
  - **POST /api/entries** → cria (retorna 201 + CreatedAtAction).
  - **PUT /api/entries/{id}** → atualiza campos mutáveis; preserve DateCreated; atualize DateUpdate.
  - **DELETE /api/entries/{id}** → remove (retornar 204 ou 200).

## 🧠 Conceitos fundamentais 

### Conceitos (.NET) - SDK, Runtime, Framework, LTS e CLI

### Framework

Um **framework** é um conjunto de bibliotecas, ferramentas e convenções que facilitam o desenvolvimento de software, fornecendo uma estrutura padrão para o código.
No caso do **.NET**, ele define como aplicativos são compilados, executados e se comunicam com o sistema operacional.

### LTS e STS
| Tipo | Significado | Suporte | Indicado para |
|---|---|---|---|
| **LTS** | Sigla para Long-term support (ou suporte de longo prazo, em português), LTS é uma variação de um software cujo principal objetivo é proporcionar estabilidade por longos períodos aos usuários.Opte sempre por LTS para projetos em produção; |	3 anos | Produção (estável) |
| **STS** | Sigla para Standard-Term Support (ou suporte de curto prazo, em português). Diferente do LTS, o STS tem um **ciclo de vida mais curto** (geralmente **18 meses** no .NET). O objetivo do STS é disponibilizar **novidades rapidamente** (novos recursos, melhorias de linguagem, performance, integração com novas tecnologias).Serve como um **“campo de provas”** para recursos que ainda serão consolidados e amadurecidos nas versões LTS seguintes. | 18 meses |	Desenvolvimento e testes |

**Exemplo:**

- .NET 5 → STS (fim em maio/2022).
- .NET 7 → STS (fim em maio/2024).
- .NET 9 → STS (lançado em novembro/2024, fim em maio/2026).
- E assim sucessivamente: versões ímpares tendem a ser STS, enquanto as pares são LTS.

### Versionamento Semântico

Definido por Versão Semântica dividida em fases: Alpha (esboço), Beta (versão de testes), Release Candidate (versão cândida para ser versão final da aplicação), Final;

O .NET segue o padrão **MAJOR.MINOR.PATCH**, ex: 9.0.1

| Parte | Exemplo| 	Significado |
|---|---|---|
| **MAJOR** | (9) |	Primeiro número. Release maior.Pode conter incompatibilidade com versões anteriores chamadas de Breaking Changes |
| **MINOR** | (0) | Mudanças pequenas na aplicação. Possui mudanças, mas é totalmente compatível com versões anteriores - Backward Compatibility. |
| **PATCH** | (1) |	Correção de bugs e outros itens simples. |

### Runtime 

O **.NET Runtime** é o ambiente onde o aplicativo **.NET** realmente é executado.
Ele fornece os componentes necessários para rodar o código compilado (assemblies `.dll`, `.exe`) — como gerenciamento de memória, coleta de lixo (garbage collector), e JIT (Just-In-Time Compilation).
São divididos em três:
- ASP.NET para aplicação WEB;
- Desktop para aplicações desktop;
- .NET Core para qualquer outra aplicação: console, batch, serviço;
- Não possui uma interface

### SDK (Software Development Kit)

O **.NET SDK** inclui o runtime, o compilador `dotnet`, modelos de projeto (templates), ferramentas de build e o **CLI (Command Line Interface)**.
Ou seja, o SDK é o pacote completo para desenvolver e executar projetos .NET.

### .NET CLI (Command Line Interface)

A **CLI** é o terminal do .NET — você cria, compila e executa projetos com comandos.

- `dotnet --version`: verifica a versão atual;
- `dotnet —list-sdks`:  lista os SDKs instalados;
- `dotnet —list-runtimes`: lista os runtimes instalados;
- `dotnet help`: exibe ajuda e lista de comandos disponíveis.

### Estrutura de um Projeto .NET

Cada aplicação criada no dotnet precisa especificar o tipo de projeto e tem resultados finais diferentes.

| Tipo de projeto |	Comando	| O que é | Quando usar | 
|---|---|---|---|
|🌐 Web API	dotnet | `new webapi -n Api` | Dividido em ASP.NET WEB, ASP.NET MVC e ASP.NET WebAPI | Camada de endpoints HTTP |
| 🧠 Class Library | `dotnet new classlib -n Core` | O resultado final é uma DLL e não possui interface (não tem uma tela) | Regras de negócio (serviços, domínio) |
| 💾 Infraestrutura |  `dotnet new classlib -n Infrastructure` | Repositórios, acesso a dados |
| 🧪 Testes	dotnet | `new xunit -n Tests` | Testes automatizados |
| 💻 Console | `dotnet new console -n Tools` | o resultado final é uma aplicação que roda no terminal e pode receber dados, esperar input do usuário | Jobs, scripts, utilitários |
| 🖥️ Blazor / MVC / Worker | `dotnet new blazorserver -n WebApp`	| Interfaces e processos em background |

### Arquivos de configuração (exemplos)
| Arquivo |	Função |
|---|---|
| `.sln` (Solution) |	Agrupa múltiplos projetos (.csproj). |
| `.csproj` | Define dependências, SDK usado, frameworks e pacotes NuGet. |
| `launchSettings.json` |	Configura o modo de execução local e ambiente (Development, Production). |
| `launch.json` | Arquivo do VS Code para debug do .NET. |
| `tasks.json` | O arquivo fica dentro da pasta .vscode/ e serve para definir tarefas automáticas que o VS Code executa antes de iniciar o debug, como compilar o projeto .NET. |

### Exemplo de .csproj

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>disable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>fundamentals_git_http_rest_api</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.10" />
  </ItemGroup>

</Project>
```
| Campo / Elemento | Função |
|---|---|
| `Project Sdk="Microsoft.NET.Sdk.Web"` | Declara o SDK usado pelo projeto (aqui: projeto Web ASP.NET). |
| `PropertyGroup` | Agrupa propriedades de build e configuração do projeto. |
| `TargetFramework` | Framework alvo do projeto (ex.: net9.0). |
| `Nullable` | Controla nullability annotations (enable/disable). |
| `ImplicitUsings` | Habilita usings implícitos gerados pelo SDK. |
| `RootNamespace` | Namespace raiz usado no projeto (opcional). |
| `ItemGroup` | Agrupa itens do projeto (dependências, referências, arquivos). |
| `PackageReference Include / Version` | Declara dependência NuGet (pacote e versão). |


### Exemplo de launchSettings.json
```json
    {
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5199",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7070;http://localhost:5199",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

| Campo | Função |
|---|---|
| `$schema` | Referência ao schema JSON (validação/edição assistida). |
| `profiles` | Objeto que agrupa perfis de execução (cada chave é um perfil) |
| `<perfil>` (ex: http / https) | Nome do perfil com suas configurações específicas (commandName, applicationUrl, etc.). |
| `commandName` | Tipo de execução local ("Project", "IISExpress", "Executable"). |
| `dotnetRunMessages` | Quando true, exibe mensagens detalhadas do `dotnet run` no console. |
| `launchBrowser` | Se true, abre um navegador ao iniciar (quando aplicável). |
| `launchUrl` | Caminho relativo a abrir no navegador (quando launchBrowser = true) |
| `applicationUrl` | URLs que a aplicação irá escutar localmente (separadas por `;`). |
| `environmentVariables` | Objeto com variáveis de ambiente para o perfil |
| `ASPNETCORE_ENVIRONMENT` | Variável que define o ambiente da aplicação (Development/Production) |
| `sslPort` | Porta SSL usada pelo IIS Express (quando aplicável) |
| `iisSettings` | Configurações específicas do IIS Express (sslPort, autenticação, etc.) |
| `executablePath / program` | Caminho do executável (usado em perfis tipo Executable) |
| `workingDirectory` | Diretório de trabalho ao iniciar a aplicação |
| `env (ou environmentVariables)` | Pode conter outras variáveis de ambiente específicas do perfil |

### Exemplo de launch.json

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Launch fundamentals-git-http-rest-api (net9.0)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/fundamentals.git.http.rest.api/bin/Debug/net9.0/fundamentals-git-http-rest-api.dll",
      "cwd": "${workspaceFolder}/fundamentals.git.http.rest.api",
      "args": [],
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_URLS": "https://localhost:7070;http://localhost:5199"
      },
      "console": "integratedTerminal",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "sourceFileMap": {
        "/Views": "${workspaceFolder}/fundamentals.git.http.rest.api/Views"
      }
    }
  ]
}
```

| Campo | Função |
|---|---|
| `version` | Versão do esquema do arquivo de configuração do VS Code (ex: "0.2.0"). |
| `configurations` | Array com configurações de execução/depuração (cada objeto é um perfil). |
| `name` | Nome legível da configuração exibido no VS Code. |
| `type` | Tipo do depurador (ex: "coreclr" para .NET). |
| `request` | Tipo de ação: "launch" (inicia processo) ou "attach" (anexa a processo). |
| `preLaunchTask` | Tarefa do VS Code a ser executada antes (ex: build). |
| `program` | Caminho para o executável/.dll a ser executado (quando request = "launch"). |
| `cwd` | Diretório de trabalho ao iniciar a aplicação (normalmente pasta do projeto). |
| `args` | Argumentos passados para a aplicação no lançamento. |
| `env / envFile` | Variáveis de ambiente (objeto `env` ou arquivo `.env`). |
| `ASPNETCORE_URLS (em env)` | Força o Kestrel a escutar nas URLs/portas especificadas durante o debug. |
| `console` | Onde a saída aparece (internalConsole, integratedTerminal, externalTerminal). |
| `stopAtEntry` | Se true, pausa no início da execução. |
| `serverReadyAction` | Automatiza ação quando servidor está pronto (ex: abrir browser em URL). |
| `sourceFileMap` | Mapeamento de caminhos fonte (útil para views/source externos). 


### Exemplo de tasks.json

```json 
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "type": "process",
      "command": "dotnet",
      "args": [
        "build",
        "${workspaceFolder}/fundamentals.git.http.rest.api/fundamentals-git-http-rest-api.csproj",
        "-c",
        "Debug"
      ],
      "group": {
        "kind": "build",
        "isDefault": true
      },
      "problemMatcher": "$msCompile",
      "presentation": {
        "reveal": "silent",
        "panel": "shared"
      }
    }
  ]
}
```
| Campo |	Função |
|---|---|
| `"version"` | Versão do formato de tasks (ex: "2.0.0"). |
| `"tasks"` | Array de tarefas que o VS Code pode executar. |
| `"label"` |	 Nome da tarefa (referenciado por preLaunchTask do launch.json). |
| `"type"` | Tipo da tarefa (ex.: "process" executa um comando). |
| `"command"` |	Comando a ser executado (ex.: "dotnet"). |
| `"args"` |	Argumentos do comando (ex.: ["build","<caminho.csproj>","-c","Debug"]) |
| `"group"` |	 Define categoria (ex.: { "kind": "build", "isDefault": true }). |
| `"problemMatcher"` |	Diz ao VS Code como interpretar erros do compilador |
| `"presentation"` | Controle de como a saída da task é apresentada (panel, reveal, etc.). |

### Estrutura de Pastas

| Pasta / Arquivo |	Função
|---|---|
| `/bin` | Arquivos compilados (executáveis e dependências) |
| `/obj` | Arquivos intermediários de build |
| `/Properties/launchSettings.json` |	Configuração de debug e ambiente |
| `appsettings.json` | Configurações padrão da aplicação |
|` appsettings.Development.json` | Configurações específicas do ambiente de desenvolvimento |
| `.gitignore` | Define o que o Git deve ignorar (bin, obj, secrets, etc.) |

### Arquivos dentro de /bin
| Arquivo |	Função |
|---|---|
| `.dll` |	Código compilado |
| `.exe` |	Executável (no Windows) |
| `.pdb` |	Arquivo de debug |
| `.runtimeconfig.json` |	Define o runtime .NET necessário |
| `.deps.json` |	Lista dependências do projeto |

### Arquivos dentro de /obj
| Arquivo |	Função |
|---|---|
| `.nuget.g.props / .nuget.g.targets` | Configuração de pacotes NuGet |
| `project.assets.json` |	Dependências resolvidas |
| `project.nuget.cache` |	Cache de pacotes |
| `.dgspec.json` | Especificações de build |


### 🌍 Conceitos HTTP

O **HTTP (HyperText Transfer Protocol)** é o protocolo que define como clientes (ex: navegadores) e servidores trocam informações.

| Protocolo | Porta padrão | Criptografia |	Uso |
|---|---|---|---|
| HTTP | 80	| ❌ Não segura | Testes locais |
| HTTPS | 443 |	✅ Segura (TLS/SSL) | Produção |

**HTTPS** é obrigatório em APIs modernas — protege contra interceptações (MITM attacks).

### Componentes

- **Cliente:** Envia requisições (requests)
- **Servidor:** Processa e responde (responses)
- **TCP:** Protocolo de transporte usado pelo HTTP

### Estrutura de uma requisição

```pgsql
GET /api/entries HTTP/1.1
Host: localhost:5000
Content-Type: application/json
```

### Estrutura de uma resposta

```css
HTTP/1.1 200 OK
Content-Type: application/json
{
  "id": 1,
  "title": "First Day"
}
```

### 🌐 Conceitos REST

### Princípios:

- **Uniformidade:** endpoints seguem padrão previsível.
- **Stateless:** cada requisição é independente.
- **Representação em JSON:** dados trocados em formato leve e legível.
- **Idempotência:** GET, PUT, DELETE devem gerar o mesmo resultado se repetidos.

### Boas práticas de nomeação

| Ação | Método | Rota |
|---|---|---|
| **Listar todos** | GET |	/api/entries | 
| **Buscar por ID** | GET |	/api/entries/{id} |
| **Criar** | POST | /api/entries |
| **Atualizar**| PUT | /api/entries/{id} |
| **Deletar**|	DELETE | /api/entries/{id} |

### Formato JSON — conceito e boas práticas
**JSON (JavaScript Object Notation)** é um formato leve baseado em texto para troca de dados.
- **Estruturas:** object { "key": value }, array [ ... ].
- **Tipos:** string, number, boolean, null, array, object.
- **Datas:** use ISO 8601 (ex.: "2025-12-22" ou "2025-12-22T15:30:00Z"); JSON não tem tipo date nativo.
- **Regras práticas:**
  - **Enviar header:** Content-Type: application/json.
  - Evitar propriedades desnecessárias.
  - Servidor pode preencher/idificar campos (id, dateCreated).
  - Para APIs públicas, documente o contrato JSON (ex.: via OpenAPI/Swagger).

Exemplo:
```json
{
  "title": "Testing the complete API",
  "content": "Adding the first entry via postman",
  "dateCreated": "2025-12-22"
}
```

###  HTTP Status Codes e IActionResult

| Situação | Código | Método |
|---|---:|---|
| **Sucesso ao buscar** | `200 (OK)` | GET |
| **Criado com sucesso** | `201 (Created)` | POST |
| **Atualização sem conteúdo** | `204 (No Content)` | PUT |
| **Recurso não encontrado** | `404 (Not Found)` | GET, PUT, DELETE |
| **Erro do cliente** | `400 (Bad Request)` | POST, PUT |
| **Erro interno** | `500 (Internal Server Error)` | Geral |

| Método | Tipo retornado | Quando usar |
|---|---|---|
| `Ok()` | `OkObjectResult` | Retornar objeto |
| `OkResult` | `OkResult` | Sem conteúdo |
| `CreatedAtAction()` | `CreatedAtActionResult` | Após POST bem-sucedido |
| `NoContent()` | `NoContentResult` | Sucesso sem retorno |
| `BadRequest()` | `BadRequestObjectResult` | Dados inválidos |
| `NotFound()` | `NotFoundResult` | Recurso ausente |


### 🧩 Conceitos Git e Versionamento

| Conceito | Descrição |
|---|---|
| **Git** | Sistema distribuído de controle de versão |
| **Branch** | Linha paralela de desenvolvimento |
| **Commit** | Salva uma alteração |
|** Merge** | Junta branches |
| **Rebase** | Reescreve histórico de commits |
| **Fork** | Cópia independente de um repositório |
| **Pull / Push** | Enviar ou receber alterações |
| **Tag** | Marca versões estáveis |

### Boas práticas:
- Commits pequenos e descritivos
- Nomear branches por tipo (feature/, fix/, hotfix/)
- Sempre revisar código via Pull Request

### Git — Comandos

| Comando |	Descrição |
|---|---|
| `git init` | Inicia repositório local |
| `git clone <url>` |	Clona repositório remoto |
| `git status` |	Mostra alterações |
| `git add .`	| Adiciona arquivos |
| `git commit -m "msg"` |	Registra mudança |
| `git branch` |	Lista branches |
| `git checkout -b feature/api` |	Cria e muda de branch |
| `git merge <branch>` |	Junta branch |
| `git pull` |	Puxa atualizações |
| `git push` |	Envia alterações |
| `git log --oneline` |	Histórico resumido |

### 🌀 Conceitos GitHub e GitFlow

**GitHub:** plataforma de hospedagem e colaboração de código.
**GitFlow:** metodologia de versionamento com branches padronizadas.

| Branch | Função |
|---|---|
| main | Produção |
| develop | Desenvolvimento |
| feature/* | Novas funcionalidades |
| release/* | Preparação para deploy |
| hotfix/* | Correções rápidas

### 🔄 Fluxo GitFlow Prático

```bash
# Clona o repositório
git clone https://github.com/user/fundamentals-git-http-rest.git

# Cria uma branch de feature
git checkout -b feature/add-entry-endpoint

# Faz alterações e commits
git add .
git commit -m "feat: add endpoint to create entries"

# Mescla com develop
git checkout develop
git merge feature/add-entry-endpoint

# Cria release
git checkout -b release/v1.0.0
git merge develop
git tag v1.0.0

# Envia para o remoto
git push origin main --tags
```

## 📖 Referências

Links úteis para os conceitos e ferramentas usados neste projeto:

- .NET (docs) — https://learn.microsoft.com/dotnet/
- ASP.NET Core — https://learn.microsoft.com/aspnet/core/
- CLI do .NET (dotnet) — https://learn.microsoft.com/dotnet/core/tools/
- Self-signed dev certificates (dotnet dev-certs) — https://learn.microsoft.com/dotnet/core/additional-tools/self-signed-development-certificate-guide
- OpenAPI / Swagger — https://swagger.io/specification/
- RESTful API best practices — https://restfulapi.net/
- HTTP status codes (MDN) — https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
- JSON (formato) — https://www.json.org/json-en.html
- Semantic Versioning — https://semver.org/
- Git (documentação oficial) — https://git-scm.com/doc
- GitFlow (modelo) — https://nvie.com/posts/a-successful-git-branching-model/
- VS Code — Debugging & launch.json — https://code.visualstudio.com/docs/editor/debugging