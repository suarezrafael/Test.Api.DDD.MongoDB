# Arquitetura do Projeto

## Estrutura de Pastas

```
Test.Api.DDD.MongoDB/
│
├── README.md                           # Documentação principal
├── TESTING.md                          # Guia de testes
├── docker-compose.yml                  # Configuração do MongoDB
├── Test.Api.DDD.MongoDB.sln           # Solution .NET
│
└── src/
    │
    ├── Test.Api.DDD.MongoDB.Api/      # 🎯 Camada de Apresentação
    │   ├── Controllers/
    │   │   ├── UsuariosController.cs  # Endpoints de usuários
    │   │   └── CardapiosController.cs # Endpoints de cardápios
    │   ├── Program.cs                  # Configuração da aplicação
    │   └── appsettings.json           # Configurações (MongoDB, JWT)
    │
    ├── Test.Api.DDD.MongoDB.Application/  # 📋 Camada de Aplicação
    │   ├── Usuario/
    │   │   ├── Commands/              # Operações de escrita
    │   │   │   ├── CreateUsuarioCommand.cs
    │   │   │   ├── CreateUsuarioCommandHandler.cs
    │   │   │   ├── UpdateUsuarioCommand.cs
    │   │   │   ├── UpdateUsuarioCommandHandler.cs
    │   │   │   ├── DeleteUsuarioCommand.cs
    │   │   │   ├── DeleteUsuarioCommandHandler.cs
    │   │   │   ├── LoginCommand.cs
    │   │   │   └── LoginCommandHandler.cs
    │   │   ├── Queries/               # Operações de leitura
    │   │   │   ├── GetUsuarioByIdQuery.cs
    │   │   │   ├── GetUsuarioByIdQueryHandler.cs
    │   │   │   ├── GetAllUsuariosQuery.cs
    │   │   │   └── GetAllUsuariosQueryHandler.cs
    │   │   └── DTOs/                  # Objetos de transferência
    │   │       ├── UsuarioDto.cs
    │   │       ├── CreateUsuarioDto.cs
    │   │       └── LoginDto.cs
    │   │
    │   ├── Cardapio/
    │   │   ├── Commands/
    │   │   │   ├── CreateCardapioCommand.cs
    │   │   │   ├── CreateCardapioCommandHandler.cs
    │   │   │   ├── UpdateCardapioCommand.cs
    │   │   │   ├── UpdateCardapioCommandHandler.cs
    │   │   │   ├── DeleteCardapioCommand.cs
    │   │   │   └── DeleteCardapioCommandHandler.cs
    │   │   ├── Queries/
    │   │   │   ├── GetCardapioByIdQuery.cs
    │   │   │   ├── GetCardapioByIdQueryHandler.cs
    │   │   │   ├── GetAllCardapiosQuery.cs
    │   │   │   └── GetAllCardapiosQueryHandler.cs
    │   │   └── DTOs/
    │   │       ├── CardapioDto.cs
    │   │       └── CreateCardapioDto.cs
    │   │
    │   └── Interfaces/                # Contratos
    │       ├── IUsuarioRepository.cs
    │       ├── ICardapioRepository.cs
    │       ├── IPasswordHasher.cs
    │       └── ITokenService.cs
    │
    ├── Test.Api.DDD.MongoDB.Domain/   # 🏛️ Camada de Domínio
    │   └── Entities/                  # Entidades do domínio
    │       ├── Usuario.cs
    │       └── Cardapio.cs
    │
    └── Test.Api.DDD.MongoDB.Infrastructure/  # 🔧 Camada de Infraestrutura
        ├── Data/
        │   ├── ApplicationDbContext.cs    # Contexto do MongoDB
        │   └── DbSeeder.cs               # Seed de dados iniciais
        ├── Repositories/                 # Implementação dos repositórios
        │   ├── UsuarioRepository.cs
        │   └── CardapioRepository.cs
        └── Services/                     # Serviços de infraestrutura
            ├── PasswordHasher.cs         # BCrypt
            └── TokenService.cs           # JWT
```

## Fluxo de Requisição

```
┌─────────────┐
│   Cliente   │
│  (Browser/  │
│    cURL)    │
└──────┬──────┘
       │
       │ HTTP Request
       ▼
┌─────────────────────────────────────────────────┐
│          API Layer (Presentation)               │
│  ┌─────────────────────────────────────────┐   │
│  │  UsuariosController / CardapiosController│   │
│  │  - Recebe requisição HTTP                │   │
│  │  - Valida dados de entrada                │   │
│  │  - Envia comando/query via MediatR        │   │
│  └───────────────┬─────────────────────────┘   │
└──────────────────┼─────────────────────────────┘
                   │
                   │ IRequest (MediatR)
                   ▼
┌─────────────────────────────────────────────────┐
│       Application Layer (Use Cases)             │
│  ┌─────────────────────────────────────────┐   │
│  │  Commands/Queries Handlers               │   │
│  │  - CreateUsuarioCommandHandler           │   │
│  │  - LoginCommandHandler                   │   │
│  │  - GetAllCardapiosQueryHandler           │   │
│  │  etc...                                  │   │
│  │                                          │   │
│  │  Usa:                                    │   │
│  │  - IUsuarioRepository                    │   │
│  │  - ICardapioRepository                   │   │
│  │  - IPasswordHasher                       │   │
│  │  - ITokenService                         │   │
│  └───────────────┬─────────────────────────┘   │
└──────────────────┼─────────────────────────────┘
                   │
                   │ Interface
                   ▼
┌─────────────────────────────────────────────────┐
│    Infrastructure Layer (External Services)     │
│  ┌─────────────────────────────────────────┐   │
│  │  Repositories                            │   │
│  │  - UsuarioRepository                     │   │
│  │  - CardapioRepository                    │   │
│  │                                          │   │
│  │  Services                                │   │
│  │  - PasswordHasher (BCrypt)               │   │
│  │  - TokenService (JWT)                    │   │
│  │                                          │   │
│  │  Data                                    │   │
│  │  - ApplicationDbContext (EF Core)        │   │
│  │  - DbSeeder                              │   │
│  └───────────────┬─────────────────────────┘   │
└──────────────────┼─────────────────────────────┘
                   │
                   │ MongoDB Driver / EF Core
                   ▼
┌─────────────────────────────────────────────────┐
│              MongoDB Database                    │
│  Collections:                                    │
│  - usuarios                                      │
│  - cardapios                                     │
└─────────────────────────────────────────────────┘
```

## Padrões Utilizados

### CQRS (Command Query Responsibility Segregation)
- **Commands**: Operações de escrita (Create, Update, Delete)
- **Queries**: Operações de leitura (Get, GetAll)
- **MediatR**: Biblioteca para implementar CQRS

### Repository Pattern
- **Interfaces** na camada Application
- **Implementações** na camada Infrastructure
- Abstração do acesso a dados

### Dependency Injection
- Configurado no `Program.cs`
- Inversão de controle (IoC)
- Facilita testes e manutenção

### DTO Pattern
- Objetos específicos para transferência de dados
- Separação entre entidades do domínio e dados de API

## Segurança

### Autenticação JWT
```
1. Cliente faz login → POST /api/usuarios/login
2. API valida credenciais
3. PasswordHasher.VerifyPassword() compara hash
4. TokenService.GenerateToken() cria JWT
5. Cliente recebe token
6. Cliente usa token em requisições: Authorization: Bearer {token}
```

### Criptografia de Senha
```
1. Usuário envia senha em texto plano
2. PasswordHasher.HashPassword() usa BCrypt
3. Hash é armazenado no MongoDB
4. Senha original nunca é armazenada
```

## Dependências Principais

| Package | Versão | Finalidade |
|---------|--------|-----------|
| MongoDB.EntityFrameworkCore | 8.1.0 | ORM para MongoDB |
| MongoDB.Driver | 2.28.0 | Driver oficial MongoDB |
| MediatR | 12.2.0 | Implementação CQRS |
| BCrypt.Net-Next | 4.0.3 | Hash de senhas |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.0 | Autenticação JWT |
| Swashbuckle.AspNetCore | 6.5.0 | Documentação Swagger |

## Configuração

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://root:example@localhost:27017/restaurantedb?authSource=admin"
  },
  "Jwt": {
    "Secret": "your-secret-key-min-256-bits-long"
  }
}
```

### Program.cs - Principais Configurações
1. **DbContext**: MongoDB connection
2. **JWT Authentication**: Bearer token
3. **MediatR**: CQRS handlers
4. **Dependency Injection**: Repositories e Services
5. **Swagger**: Documentação da API
6. **Database Seeding**: Criação do admin

## Endpoints da API

### Públicos (sem autenticação)
- `POST /api/usuarios/login` - Login
- `POST /api/usuarios` - Criar usuário

### Protegidos (requer JWT)
- `GET /api/usuarios` - Listar usuários
- `GET /api/usuarios/{id}` - Buscar usuário
- `PUT /api/usuarios/{id}` - Atualizar usuário
- `DELETE /api/usuarios/{id}` - Deletar usuário
- `GET /api/cardapios` - Listar cardápios
- `GET /api/cardapios/{id}` - Buscar cardápio
- `POST /api/cardapios` - Criar cardápio
- `PUT /api/cardapios/{id}` - Atualizar cardápio
- `DELETE /api/cardapios/{id}` - Deletar cardápio

## Inicialização do Sistema

1. **Docker Compose**: Sobe MongoDB
2. **Program.cs**: Configura serviços e middleware
3. **DbSeeder**: Cria usuário admin se não existir
4. **API**: Fica disponível para requisições
5. **Swagger**: Interface de documentação disponível
