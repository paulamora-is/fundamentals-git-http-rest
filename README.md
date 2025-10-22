# 🇧🇷 README — fundamentals-git-http-rest

## 🚀 Introdução

Este repositório contém o projeto **fundamentals-git-http-rest**, criado para estudar os fundamentos de:

- APIs REST com .NET 9
- Comunicação via HTTP
- Estrutura em camadas (Controller, Service, Infra, Model)
- Boas práticas de rotas, versionamento e código limpo
- Git, GitHub e GitFlow para versionamento profissional

## 🧠 Conceitos Fundamentais — SDK, Runtime, Framework, LTS e CLI

### 🧩 Framework

Um **framework** é um conjunto de bibliotecas, ferramentas e convenções que facilitam o desenvolvimento de software, fornecendo uma estrutura padrão para o código.
No caso do **.NET**, ele define como aplicativos são compilados, executados e se comunicam com o sistema operacional.

### 🧱 LTS e STS
| Tipo | Significado | Suporte | Indicado para |
|---|---|---|---|
| **LTS** | Sigla para Long-term support (ou suporte de longo prazo, em português), LTS é uma variação de um software cujo principal objetivo é proporcionar estabilidade por longos períodos aos usuários.Opte sempre por LTS para projetos em produção; |	3 anos | Produção (estável) |
| **STS** | Sigla para Standard-Term Support (ou suporte de curto prazo, em português). Diferente do LTS, o STS tem um **ciclo de vida mais curto** (geralmente **18 meses** no .NET). O objetivo do STS é disponibilizar **novidades rapidamente** (novos recursos, melhorias de linguagem, performance, integração com novas tecnologias).Serve como um **“campo de provas”** para recursos que ainda serão consolidados e amadurecidos nas versões LTS seguintes. | 18 meses |	Desenvolvimento e testes |

**Exemplo:**

- .NET 5 → STS (fim em maio/2022).
- .NET 7 → STS (fim em maio/2024).
- .NET 9 → STS (lançado em novembro/2024, fim em maio/2026).
- E assim sucessivamente: versões ímpares tendem a ser STS, enquanto as pares são LTS.

### 🧭 Versionamento Semântico

Definido por Versão Semântica dividida em fases: Alpha (esboço), Beta (versão de testes), Release Candidate (versão cândida para ser versão final da aplicação), Final;

O .NET segue o padrão **MAJOR.MINOR.PATCH**, ex: 9.0.1

| Parte | Exemplo| 	Significado |
|---|---|---|
| **MAJOR** | (9) |	Primeiro número. Release maior.Pode conter incompatibilidade com versões anteriores chamadas de Breaking Changes |
| **MINOR** | (0) | Mudanças pequenas na aplicação. Possui mudanças, mas é totalmente compatível com versões anteriores - Backward Compatibility. |
| **PATCH** | (1) |	Correção de bugs e outros itens simples. |

### ⚙️ Runtime 

O **.NET Runtime** é o ambiente onde o aplicativo **.NET** realmente é executado.
Ele fornece os componentes necessários para rodar o código compilado (assemblies `.dll`, `.exe`) — como gerenciamento de memória, coleta de lixo (garbage collector), e JIT (Just-In-Time Compilation).
São divididos em três:
- ASP.NET para aplicação WEB;
- Desktop para aplicações desktop;
- .NET Core para qualquer outra aplicação: console, batch, serviço;
- Não possui uma interface

### 🧰 SDK (Software Development Kit)

O **.NET SDK** inclui o runtime, o compilador `dotnet`, modelos de projeto (templates), ferramentas de build e o **CLI (Command Line Interface)**.
Ou seja, o SDK é o pacote completo para desenvolver e executar projetos .NET.

### ⚙️ .NET CLI (Command Line Interface)

A **CLI** é o terminal do .NET — você cria, compila e executa projetos com comandos.

- `dotnet --version`: verifica a versão atual;
- `dotnet —list-sdks`:  lista os SDKs instalados;
- `dotnet —list-runtimes`: lista os runtimes instalados;
- `dotnet help`: exibe ajuda e lista de comandos disponíveis.

## ⚙️ Instalação e Configuração do .NET

### Etapas para instalação

| Etapa | Comando/Ação | Observação |
|---|---|---|
| Verificar se tem .NET	| `dotnet --version` | Mostra a versão instalada |
| Instalar via CMD | `winget install Microsoft.DotNet.SDK.9` | Instala o SDK 9 |
| Instalar via site | Download oficial | Método visual |
| Confirmar instalação | `dotnet --info` | Exibe detalhes do SDK |

### Etapas para criação do projeto
| Etapa | Comando/Ação | Observação |
|---|---|---|
| Criar projeto | `dotnet new webapi -n fundamentals-git-http-rest` | Cria API base |
| Compilar o código | `dotnet build` |
| Executar projeto | `dotnet run` |	Roda o servidor local |

### Outros comandos
| Etapa | Comando/Ação |
|---|---|
| Limpa arquivos de build | `dotnet clean` |
| Executa testes | `dotnet test` |
| Restaura dependências | `dotnet restore` |
| Gera versão para deploy |  `dotnet publish -c Release` |


### 🔄 Fluxo de execução básico
```bash
dotnet new webapi -n fundamentals-git-http-rest
cd fundamentals-git-http-rest
dotnet build
dotnet run
```

**Fluxo**:
1️⃣ Cria → 2️⃣ Compila → 3️⃣ Executa → 4️⃣ API roda em `https://localhost:5001`

## 🏗️ Estrutura de um Projeto .NET

Cada aplicação criada no dotnet precisa especificar o tipo de projeto e tem resultados finais diferentes.

| Tipo de projeto |	Comando	| O que é | Quando usar | 
|---|---|---|---|
|🌐 Web API	dotnet | `new webapi -n Api` | Dividido em ASP.NET WEB, ASP.NET MVC e ASP.NET WebAPI | Camada de endpoints HTTP |
| 🧠 Class Library | `dotnet new classlib -n Core` | O resultado final é uma DLL e não possui interface (não tem uma tela) | Regras de negócio (serviços, domínio) |
| 💾 Infraestrutura |  `dotnet new classlib -n Infrastructure` | Repositórios, acesso a dados |
| 🧪 Testes	dotnet | `new xunit -n Tests` | Testes automatizados |
| 💻 Console | `dotnet new console -n Tools` | o resultado final é uma aplicação que roda no terminal e pode receber dados, esperar input do usuário | Jobs, scripts, utilitários |
| 🖥️ Blazor / MVC / Worker | `dotnet new blazorserver -n WebApp`	| Interfaces e processos em background |

### 📘 Arquivos Importantes (.sln, .csproj, launch.json)

| Arquivo |	Função |
|---|---|
| `.sln` (Solution) |	Agrupa múltiplos projetos (.csproj). |
| `.csproj` | Define dependências, SDK usado, frameworks e pacotes NuGet. |
| `launchSettings.json` |	Configura o modo de execução local e ambiente (Development, Production). |
| `launch.json` | Arquivo do VS Code para debug do .NET. |

### 🧩 Exemplo de launch.json

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Launch fundamentals-git-http-rest (net9.0)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/bin/Debug/net9.0/fundamentals-git-http-rest.dll",
      "cwd": "${workspaceFolder}",
      "args": [],
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "console": "integratedTerminal",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
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
| `program` | Caminho para o executável/.dll a ser executado (quando request = "launch"). |
| `cwd` | Diretório de trabalho ao iniciar a aplicação. |
| `args` | Argumentos passados para a aplicação no lançamento. |
| `env / envFile` | Variáveis de ambiente (objeto `env` ou arquivo `.env`). |
| `preLaunchTask` | Tarefa do VS Code a ser executada antes (ex: build). |
| `stopAtEntry` | Se true, pausa no início da execução. |
| `console` | Onde a saída aparece (internalConsole, integratedTerminal, externalTerminal). |
| `serverReadyAction` | Automatiza ação quando servidor está pronto (ex: abrir browser em URL). |
| `launchBrowser` | Configuração para abrir navegador automaticamente (frequentemente via serverReadyAction). |

### 🧩 Exemplo de .csproj

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>
</Project>
```
| Campo | Função |
|---|---|
| `Project Sdk="Microsoft.NET.Sdk.Web"` | Define o SDK usado pelo projeto; aqui indica projeto Web (ASP.NET). |
| `PropertyGroup` | Bloco que agrupa propriedades de build/compilação (TargetFramework, Nullable, etc.). |
| `TargetFramework` | Define a framework alvo (ex: net9.0). Controle de versão do runtime alvo. |
| `Nullable` | Controla o recurso de reference nullability (enable/disable). |
| `ImplicitUsings` | Habilita usings implícitos gerados automaticamente pelo SDK. |
| `ItemGroup` | Bloco que agrupa itens do projeto: PackageReference, ProjectReference, Compile, None, etc. |
| `PackageReference Include / Version` | Declara dependência NuGet (nome do pacote e versão). |

### 🧩 Exemplo de launchSettings.json
```json
    {
        "profiles": {
            "fundamentals-git-http-rest": {
            "commandName": "Project",
            "dotnetRunMessages": true,
            "launchBrowser": true,
            "applicationUrl": "https://localhost:5001;http://localhost:5000",
            "environmentVariables": {
                "ASPNETCORE_ENVIRONMENT": "Development"
                }
            }
        }
    }
```

| Campo | Função |
|---|---|
| `profiles` | Objeto que agrupa perfis de execução (cada chave é um perfil) |
| `<perfil>` (ex: fundamentals-git-http-rest) | Nome do perfil com suas configurações específicas |
| `commandName` | Tipo de execução: "Project" / "IISExpress" / "Executable" | "Project" |
| `dotnetRunMessages` | Habilita mensagens detalhadas do `dotnet run` no console |
| `launchBrowser` | Indica se o navegador deve abrir automaticamente ao iniciar |
| `launchUrl` | Caminho relativo a abrir no navegador (quando launchBrowser = true) |
| `applicationUrl` | URLs que a aplicação irá escutar (separadas por `;`) |
| `environmentVariables` | Objeto com variáveis de ambiente para o perfil |
| `ASPNETCORE_ENVIRONMENT` | Variável que define o ambiente da aplicação (Development/Production) |
| `sslPort` | Porta SSL usada pelo IIS Express (quando aplicável) |
| `iisSettings` | Configurações específicas do IIS Express (sslPort, autenticação, etc.) |
| `executablePath / program` | Caminho do executável (usado em perfis tipo Executable) |
| `workingDirectory` | Diretório de trabalho ao iniciar a aplicação |
| `env (ou environmentVariables)` | Pode conter outras variáveis de ambiente específicas do perfil |

## 🗂️ Estrutura de Pastas

| Pasta / Arquivo |	Função
|---|---|
| `/bin` | Arquivos compilados (executáveis e dependências) |
| `/obj` | Arquivos intermediários de build |
| `/Properties/launchSettings.json` |	Configuração de debug e ambiente |
| `appsettings.json` | Configurações padrão da aplicação |
|` appsettings.Development.json` | Configurações específicas do ambiente de desenvolvimento |
| `.gitignore` | Define o que o Git deve ignorar (bin, obj, secrets, etc.) |

### 🧩 Arquivos dentro de /bin
| Arquivo |	Função |
|---|---|
| `.dll` |	Código compilado |
| `.exe` |	Executável (no Windows) |
| `.pdb` |	Arquivo de debug |
| `.runtimeconfig.json` |	Define o runtime .NET necessário |
| `.deps.json` |	Lista dependências do projeto |

### 🧩 Arquivos dentro de /obj
| Arquivo |	Função |
|---|---|
| `.nuget.g.props / .nuget.g.targets` | Configuração de pacotes NuGet |
| `project.assets.json` |	Dependências resolvidas |
| `project.nuget.cache` |	Cache de pacotes |
| `.dgspec.json` | Especificações de build |

## 🧩 Namespaces, Usings e Runtime

- **namespace** → Agrupa classes relacionadas logicamente (como pacotes).
- **using** → Importa outros namespaces.
- **runtime** → É o ambiente de execução do .NET (CoreCLR).

## 🪲 Depuração (Debug) no VS Code

Para habilitar o debug:

1. Instale as extensões:

- ✅ C# Dev Kit
- ✅ .NET Install Tool
- ✅ NuGet Gallery

2. Gere o arquivo de debug:
`dotnet build`
O VS Code detectará o projeto e criará o launch.json.

3. Clique em ▶️ “Run and Debug”.

## 🌍 Conceitos HTTP

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

### Métodos HTTP mais comuns

| Método | Ação | Exemplo |
|---|---|---|
| `GET` |	Buscar dados |	/api/entries |
| `POST` | Criar recurso |	/api/entries |
| `PUT` |	Atualizar recurso |	/api/entries/1 |
| `DELETE` |	Remover recurso | /api/entries/1 |

## 🧭 Boas Práticas REST

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

### 🧱 Camadas do Projeto

| Camada |	Função |
|---|---|
| **Model** | Estrutura dos dados |
| **Infra** | Acesso a dados (banco, API externa, mock) |
| **Service** |	Regras de negócio |
| **Controller** |	Recebe requisições HTTP e retorna respostas |
| **Program.cs** |	Configurações globais e inicialização da API |

### 🎯 Controllers, Rotas e Parâmetros

| Tipo | Exemplo | Uso |
|---|---|---|
| [FromRoute] |	/entries/5 | Identificação direta do recurso |
| [FromQuery] |	/entries?title=abc | Filtros e ordenação |
| [FromHeader] | Authorization: Bearer | Autenticação e cache |
| [FromBody] | JSON no corpo | Envio de dados (POST, PUT) |

### 📬 HTTP Status Codes e IActionResult

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

### 🏗️ Explicação do CreatedAtAction

```csharp
return CreatedAtAction(nameof(GetById), new { id = entryModel.Id }, entryModel);
```

| Parâmetro | Função |
|---|---|
| `nameof(GetById)` | Indica o método que pode ser usado para buscar o recurso criado |
| `new { id = entryModel.Id }` | Valores de rota |
| `entryModel` | Objeto criado retornado no corpo da resposta |

## 🧩 Git e Versionamento

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

### 🧩 Git — Comandos

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

## 🌱 GitHub e GitFlow

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