# Tecnologias usadas
- Asp.NET core 3.1
- Entity Framework Core
- AutoMapper 10.1.1
- Angular 9
- PrimeNG 10
- Sql Server 2019
- Verdaccio 4.8.1

# Como iniciar o projeto
- Passo 1: Abra o arquivo ddl.sql e execute no Sql Management Studio para criar a base de dados
- Passo 2: Abra o projeto capgemini-api em um IDE qualquer (VS Code, Visual Studio e etc) execute o comando "dotnet watch run" no terminal
- Passo 3: Abra o projeto capgemini-web em um IDE qualquer (VS Code e etc) execute o comando "npm start" no terminal
- Passo 4: Abra um navegador qualquer e navegue até http://localhost:4200

Pronto o projeto está pronto para ser testado.

# Endpoints da API

- POST api/importacoes/upload : Faz o upload de uma planilha excel no formato especificado para o banco de dados
- GET api/importacoes : Lista todos os registros persistidos
- GET api/importacoes/{id} : Lista o registro do id {id}
