create database bdImportacoes;

use bdImportacoes;

create table Importacao (
	id bigint identity(1, 1) not null,
	dataEntrega date not null,
	nomeProduto varchar(50) not null,
	quantidade int not null,
	valorUnitario decimal(15, 2) not null
);

CREATE LOGIN capgemini WITH PASSWORD = 'capgemini';

CREATE USER capgemini FOR LOGIN capgemini;

GRANT SELECT, INSERT to capgemini;