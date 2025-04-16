# 📁 GitHub Repository Manager

Uma aplicação completa para busca e gerenciamento de repositórios públicos do GitHub com possibilidade de favoritar e salvar localmente. 

---

## 🚀 Tecnologias Utilizadas

### 🔧 Back-end (.NET 8)
- **ASP.NET Core Web API** (API REST)
- **Entity Framework Core + SQLite**
- **Arquitetura Limpa (Clean Architecture)**
- **CQRS (Command Query Responsibility Segregation)**
- **MediatR**
- **FluentValidation** (validações de entrada)
- **Swagger** (documentação da API)
- **xUnit** (testes unitários)
- **FluentAssertions**, **Bogus**, **NSubstitute** (auxílio aos testes)
- **Padrões de Projeto Aplicados**:
  - ✅ Global Exception Handler
  - ✅ Service Collection Extensions
  - ✅ Padrão Repository
  - ✅ Padrão Result
  - ✅ Padrão Mediator
  - ✅ DTOs
- **Princípios de Desenvolvimento**:
  - 🧠 Clean Code
  - 🧱 SOLID

> 📌 O banco de dados já está configurado e não precisa de nenhuma ação extra. Basta rodar a API.

### 🌐 Front-end (Angular 17)
- **Angular Standalone Components**
- **HTTP Client integrado à API**
- **Filtros, paginação e ordenação**
- **Favoritar repositórios**
- **Menu lateral responsivo**
- **Estilização com CSS puro**

---

## 🗂️ Estrutura do Projeto
```
📦 RepositoryManager
├── 📁 src
│   ├── 📁 ABC.RepositoryManager.API          # Camada de apresentação (WebAPI)
│   ├── 📁 ABC.RepositoryManager.Application   # Camada de aplicação (casos de uso, DTOs, CQRS)
│   ├── 📁 ABC.RepositoryManager.Domain        # Camada de domínio (entidades, enums, interfaces)
│   └── 📁 ABC.RepositoryManager.Infrastructure# Camada de infraestrutura (EF Core, Http, SQLite)
├── 📁 test                                    # Testes unitários
├── 📁 web-app                                 # Front-end Angular
├── 📄 ABC.RepositoryManager.sln              # Solução Visual Studio
└── 📄 README.md
```

---

## 📌 Endpoints da API

### 🔍 Buscar repositórios públicos
```
GET /api/Repos/search
```
Parâmetros:
- `repoName` (string, obrigatório)
- `page` (int, opcional)
- `perPage` (int, opcional)
- `sortBy` (enum: `null`, `0`, `1`, `2`, opcional)

### ⭐ Adicionar repositório aos favoritos
```
POST /api/Repos/favorite
```
Corpo da requisição:
```json
{
  "id": 123,
  "name": "repo-name",
  "language": "TypeScript",
  "stargazers": 100,
  "forks": 50,
  "watchers": 75,
  "url": "https://github.com/user/repo",
  "owner": "user"
}
```

### ❌ Remover favorito
```
DELETE /api/Repos/remove-favorite
```
Corpo:
```json
{
  "id": 123
}
```

### 📁 Listar repositórios favoritos
```
GET /api/Repos/favorite-repositories
```
Parâmetros:
- `page` (int, opcional)
- `perPage` (int, opcional)
- `sortBy` (enum: `null`, `0`, `1`, `3`, opcional)

---

## 🧪 Testes Unitários
- Estrutura de testes com **xUnit**
- Uso de **FluentAssertions** para assertions claras
- **Bogus** para geração de dados fake
- **NSubstitute** para mocks e stubs

---

## ▶️ Como Rodar

### 🔧 API (.NET)
```bash
cd src/ABC.RepositoryManager.API

# Executar API
dotnet run
```

### 🌐 Front-end (Angular)
```bash
cd web-app
npm install
ng serve
```

Acesse: `http://localhost:4200`

---

## 🧠 Observações
- O banco SQLite já está incluído, não é necessário rodar `update-database`
- A aplicação está pronta para uso local!

---

## 💡 Autor
👨‍💻 João de Deus Uchôa  
📧 [LinkedIn](https://www.linkedin.com/in/joaouuchoa1)

---

Feito com 💙 usando .NET + Angular + Clean Architecture

