# Tecnologias usadas
- Asp.NET core 3.1
- Angular 9
- Entity Framework Core
- Sql Server 2019

# Como iniciar o projeto
- Passo 1: Abra o arquivo ddl.sql e execute no Sql Management Studio para criar a base de dados
- Passo 2: Abra o projeto capgemini-api em um IDE qualquer (VS Code, Visual Studio e etc) execute o comando "dotnet watch run" no terminal
- Passo 3: Abra o projeto capgemini-web em um IDE qualquer (VS Code e etc) execute o comando "npm start" no terminal
- Passo 4: Abra um navegador qualquer e navegue até http://localhost:4200

Pronto o projeto está pronto para ser testado.

# Endpoints da API

- POST api/importacoes/upload : Faz o upload de uma planilha excel no formato especificado para o banco de dados
- GET api/importacoes : Lista todos os registros persistidos
- GET api/importacoes/{id} : Lista o registo do id {id}
