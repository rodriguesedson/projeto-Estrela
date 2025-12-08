# Estrutura da API

## Endpoints

### 1 Auth
- cadastrar: ``POST /auth/register``
  - body: {"email": "string@example.com", "name": "string", "birthdate": "aaaa-mm-dd"}
  ````c#
  {
      "email": "user@example.com",
      "name": "string",
      "birthDate": "2025-12-05"
  }
  ````
- gerar senha: ``POST /auth/password``
    - body: 
    ````c#
    {
        "email": "string"
    }
    ````
- login: ``POST /auth/login``
    - body:
    ````c#
    {
        "email": "string",
        "password": "string"
    }
    ````

### 2 User
- Admin
    - listar: ``GET /user``
    - buscar por email: ``GET /user/find/email/{email}``
    - buscar por id: ``GET /user/find/id/{id}``
    - desativar conta: ``PUT /user/deactivate/{id}``
    - ativar conta: ``PUT /user/activate/{id}``
- Admin/Student
    - editar: ``PUT /user/edit/{id}``
        - body: {"email": "string@example.com", "name": "string", "birthdate": "aaaa-mm-dd"}
        ````c#
        {
            "email": "user@example.com",
            "name": "string",
            "birthDate": "2025-12-05"
        }
        ````

### 3 ProjectClass
- Admin
    - cadastrar: ``POST /class/register``
        - body: 
        ````c#
        {
            "name": "string",
            "days": [
                0
            ],
            "time": "03:44:35.079Z",
            "students": [
                {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "email": "string",
                "name": "string",
                "birthDate": "2025-12-05",
                "role": 0,
                "isActive": true,
                "tempPasswordHash": "string",
                "tempPasswordExpiresAt": "2025-12-05T03:44:35.079Z",
                "tempPasswordUsed": true
                }
            ]
        }
        ````
    - listar turmas: ``GET /class/list``