# Test.Api.DDD.MongoDB

API .NET 8 com arquitetura DDD (Domain-Driven Design) Modular Monolito usando padrão CQRS, autenticação JWT e MongoDB.

## 📋 Sobre o Projeto

Este projeto implementa uma API REST seguindo os princípios de DDD e Clean Architecture, com as seguintes características:

- **Arquitetura em Camadas**: Domain, Application, Infrastructure e API
- **Padrão CQRS**: Separação de comandos (escrita) e queries (leitura) usando MediatR
- **Autenticação JWT**: Sistema de login com tokens JWT
- **MongoDB com EF Core**: Integração com MongoDB usando Entity Framework Core
- **Criptografia de Senha**: Uso de BCrypt para hash de senhas
- **Seed de Dados**: Usuário administrador padrão (admin@admin.com)

## 🏗️ Estrutura do Projeto

```
Test.Api.DDD.MongoDB/
├── src/
│   ├── Test.Api.DDD.MongoDB.Api/          # Camada de apresentação (Controllers)
│   ├── Test.Api.DDD.MongoDB.Application/  # Camada de aplicação (CQRS - Commands/Queries)
│   ├── Test.Api.DDD.MongoDB.Domain/       # Camada de domínio (Entidades)
│   └── Test.Api.DDD.MongoDB.Infrastructure/ # Camada de infraestrutura (Repositórios, Serviços)
```

## 🚀 Funcionalidades

### Módulo de Usuários
- ✅ Login com JWT
- ✅ CRUD completo de usuários
- ✅ Senha criptografada com BCrypt
- ✅ Seed do usuário admin@admin.com (senha: admin123)

### Módulo de Cardápio
- ✅ CRUD completo de cardápios de restaurante
- ✅ Campos: Título, Preço, Descrição, PossuiPreparo
- ✅ Endpoints protegidos por autenticação

## 📦 Tecnologias Utilizadas

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- MongoDB.EntityFrameworkCore
- MediatR (CQRS)
- BCrypt.Net-Next
- Swashbuckle (Swagger)
- JWT Authentication

## 🔧 Pré-requisitos

- .NET 8.0 SDK
- MongoDB (local ou remoto)

## ⚙️ Configuração

1. Clone o repositório:
```bash
git clone https://github.com/suarezrafael/Test.Api.DDD.MongoDB.git
cd Test.Api.DDD.MongoDB
```

2. Configure a string de conexão do MongoDB no arquivo `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017/restaurantedb"
  },
  "Jwt": {
    "Secret": "your-secret-key-min-256-bits-long-please-change-this-in-production"
  }
}
```

3. Restaure as dependências:
```bash
dotnet restore
```

4. Execute o projeto:
```bash
cd src/Test.Api.DDD.MongoDB.Api
dotnet run
```

A API estará disponível em: `https://localhost:7000` ou `http://localhost:5000`

## 📚 Endpoints da API

### Autenticação

#### POST /api/usuarios/login
Login de usuário (retorna token JWT)
```json
{
  "email": "admin@admin.com",
  "senha": "admin123"
}
```

### Usuários

#### GET /api/usuarios
Lista todos os usuários (requer autenticação)

#### GET /api/usuarios/{id}
Busca usuário por ID (requer autenticação)

#### POST /api/usuarios
Cria novo usuário
```json
{
  "email": "usuario@example.com",
  "senha": "senha123"
}
```

#### PUT /api/usuarios/{id}
Atualiza usuário (requer autenticação)
```json
{
  "email": "novoemail@example.com",
  "senha": "novasenha123"
}
```

#### DELETE /api/usuarios/{id}
Remove usuário (requer autenticação)

### Cardápios

#### GET /api/cardapios
Lista todos os cardápios (requer autenticação)

#### GET /api/cardapios/{id}
Busca cardápio por ID (requer autenticação)

#### POST /api/cardapios
Cria novo cardápio (requer autenticação)
```json
{
  "titulo": "Pizza Margherita",
  "preco": 35.90,
  "descricao": "Pizza com molho de tomate, mussarela e manjericão",
  "possuiPreparo": true
}
```

#### PUT /api/cardapios/{id}
Atualiza cardápio (requer autenticação)
```json
{
  "titulo": "Pizza Margherita Grande",
  "preco": 45.90,
  "descricao": "Pizza grande com molho de tomate, mussarela e manjericão",
  "possuiPreparo": true
}
```

#### DELETE /api/cardapios/{id}
Remove cardápio (requer autenticação)

## 🔐 Autenticação

Para acessar endpoints protegidos, adicione o token JWT no header da requisição:
```
Authorization: Bearer {seu-token-jwt}
```

## 📖 Swagger

Acesse a documentação interativa da API em: `https://localhost:7000/swagger`

## 🧪 Testando a API

1. Faça login com o usuário padrão:
```bash
curl -X POST https://localhost:7000/api/usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@admin.com","senha":"admin123"}'
```

2. Use o token retornado para acessar endpoints protegidos:
```bash
curl -X GET https://localhost:7000/api/cardapios \
  -H "Authorization: Bearer {seu-token}"
```

## 🏛️ Arquitetura

### Domain (Domínio)
- **Entidades**: Usuario, Cardapio
- Representa o núcleo do negócio

### Application (Aplicação)
- **Commands**: Operações de escrita (Create, Update, Delete)
- **Queries**: Operações de leitura (GetAll, GetById)
- **DTOs**: Objetos de transferência de dados
- **Interfaces**: Contratos para repositórios e serviços

### Infrastructure (Infraestrutura)
- **Repositories**: Implementação de acesso a dados
- **Services**: Serviços de infraestrutura (PasswordHasher, TokenService)
- **DbContext**: Configuração do MongoDB
- **DbSeeder**: Seed de dados iniciais

### API (Apresentação)
- **Controllers**: Endpoints REST
- **Configuração**: Dependency Injection, JWT, Swagger

## 📝 Licença

Este projeto é de uso livre para fins educacionais e de teste.
