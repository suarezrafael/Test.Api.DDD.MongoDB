# Guia de Testes da API

Este documento fornece exemplos práticos de como testar todos os endpoints da API.

## Pré-requisitos

1. MongoDB rodando (use `docker compose up -d`)
2. API rodando (use `dotnet run` na pasta `src/Test.Api.DDD.MongoDB.Api`)

## Testando com cURL

### 1. Login (Obter Token JWT)

```bash
curl -X POST http://localhost:5000/api/usuarios/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@admin.com",
    "senha": "admin123"
  }'
```

**Resposta esperada:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### 2. Criar Novo Usuário

```bash
curl -X POST http://localhost:5000/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{
    "email": "novousuario@example.com",
    "senha": "senha123"
  }'
```

### 3. Listar Todos os Usuários (Autenticado)

```bash
# Primeiro, obtenha o token
TOKEN=$(curl -X POST http://localhost:5000/api/usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@admin.com","senha":"admin123"}' \
  -s | jq -r '.token')

# Use o token para listar usuários
curl -X GET http://localhost:5000/api/usuarios \
  -H "Authorization: Bearer $TOKEN"
```

### 4. Buscar Usuário por ID

```bash
curl -X GET http://localhost:5000/api/usuarios/{id} \
  -H "Authorization: Bearer $TOKEN"
```

### 5. Atualizar Usuário

```bash
curl -X PUT http://localhost:5000/api/usuarios/{id} \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "email": "emailatualizado@example.com",
    "senha": "novasenha123"
  }'
```

### 6. Deletar Usuário

```bash
curl -X DELETE http://localhost:5000/api/usuarios/{id} \
  -H "Authorization: Bearer $TOKEN"
```

### 7. Criar Cardápio

```bash
curl -X POST http://localhost:5000/api/cardapios \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "titulo": "Pizza Margherita",
    "preco": 35.90,
    "descricao": "Pizza com molho de tomate, mussarela e manjericão",
    "possuiPreparo": true
  }'
```

### 8. Listar Todos os Cardápios

```bash
curl -X GET http://localhost:5000/api/cardapios \
  -H "Authorization: Bearer $TOKEN"
```

### 9. Buscar Cardápio por ID

```bash
curl -X GET http://localhost:5000/api/cardapios/{id} \
  -H "Authorization: Bearer $TOKEN"
```

### 10. Atualizar Cardápio

```bash
curl -X PUT http://localhost:5000/api/cardapios/{id} \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "titulo": "Pizza Margherita Grande",
    "preco": 45.90,
    "descricao": "Pizza grande com molho de tomate, mussarela e manjericão fresco",
    "possuiPreparo": true
  }'
```

### 11. Deletar Cardápio

```bash
curl -X DELETE http://localhost:5000/api/cardapios/{id} \
  -H "Authorization: Bearer $TOKEN"
```

## Testando com Swagger UI

1. Acesse: `http://localhost:5000/swagger`
2. Faça login usando o endpoint `/api/usuarios/login`
3. Copie o token retornado
4. Clique no botão "Authorize" no topo da página do Swagger
5. Digite: `Bearer {seu-token}` (substitua {seu-token} pelo token obtido)
6. Clique em "Authorize"
7. Agora você pode testar todos os endpoints protegidos

## Exemplos de Dados para Cardápios

### Pizza
```json
{
  "titulo": "Pizza Calabresa",
  "preco": 38.90,
  "descricao": "Pizza com calabresa, cebola e azeitonas",
  "possuiPreparo": true
}
```

### Bebida
```json
{
  "titulo": "Suco Natural",
  "preco": 8.50,
  "descricao": "Suco natural de laranja 500ml",
  "possuiPreparo": false
}
```

### Sobremesa
```json
{
  "titulo": "Pudim de Leite",
  "preco": 12.00,
  "descricao": "Pudim caseiro com calda de caramelo",
  "possuiPreparo": true
}
```

## Testando Autenticação

### Teste de Acesso Não Autorizado
```bash
# Sem token - deve retornar 401 Unauthorized
curl -X GET http://localhost:5000/api/cardapios
```

### Teste de Token Inválido
```bash
# Com token inválido - deve retornar 401 Unauthorized
curl -X GET http://localhost:5000/api/cardapios \
  -H "Authorization: Bearer token-invalido"
```

## Verificando Dados no MongoDB

```bash
# Conecte ao MongoDB
docker exec -it mongodb-restaurante mongosh -u root -p example --authenticationDatabase admin

# Use o banco de dados
use restaurantedb

# Liste usuários
db.usuarios.find().pretty()

# Liste cardápios
db.cardapios.find().pretty()

# Saia do MongoDB
exit
```

## Scripts de Teste Completo

### Script Bash para Testar Todos os Endpoints

```bash
#!/bin/bash

API_URL="http://localhost:5000"

echo "=== Testando API ==="

# 1. Login
echo -e "\n1. Fazendo login..."
TOKEN=$(curl -s -X POST $API_URL/api/usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@admin.com","senha":"admin123"}' | jq -r '.token')

echo "Token obtido: ${TOKEN:0:50}..."

# 2. Criar usuário
echo -e "\n2. Criando novo usuário..."
curl -s -X POST $API_URL/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{"email":"teste@teste.com","senha":"senha123"}' | jq

# 3. Listar usuários
echo -e "\n3. Listando todos os usuários..."
curl -s -X GET $API_URL/api/usuarios \
  -H "Authorization: Bearer $TOKEN" | jq

# 4. Criar cardápio
echo -e "\n4. Criando cardápio..."
CARDAPIO_ID=$(curl -s -X POST $API_URL/api/cardapios \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"titulo":"Pizza Teste","preco":25.00,"descricao":"Pizza para teste","possuiPreparo":true}' | jq -r '.id')

echo "ID do cardápio criado: $CARDAPIO_ID"

# 5. Listar cardápios
echo -e "\n5. Listando todos os cardápios..."
curl -s -X GET $API_URL/api/cardapios \
  -H "Authorization: Bearer $TOKEN" | jq

# 6. Atualizar cardápio
echo -e "\n6. Atualizando cardápio..."
curl -s -X PUT $API_URL/api/cardapios/$CARDAPIO_ID \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{"titulo":"Pizza Teste Atualizada","preco":30.00,"descricao":"Pizza atualizada","possuiPreparo":true}' | jq

echo -e "\n=== Testes concluídos ==="
```

Salve este script como `test-api.sh`, dê permissão de execução (`chmod +x test-api.sh`) e execute (`./test-api.sh`).

## Cenários de Teste

### Cenário 1: Fluxo Completo do Usuário
1. Criar novo usuário
2. Fazer login com o novo usuário
3. Usar o token para acessar endpoints protegidos
4. Atualizar informações do usuário
5. Deletar o usuário

### Cenário 2: Gestão de Cardápio
1. Fazer login como admin
2. Criar múltiplos itens de cardápio
3. Listar todos os itens
4. Buscar item específico por ID
5. Atualizar preço de um item
6. Deletar item do cardápio

### Cenário 3: Validação de Segurança
1. Tentar acessar endpoint protegido sem token
2. Tentar usar token expirado
3. Verificar que senhas são criptografadas no banco
4. Verificar que senhas não aparecem nas respostas da API
