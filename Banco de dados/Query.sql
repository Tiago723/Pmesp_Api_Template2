
create database db_Pmesp_Api_Template2;

use db_Pmesp_Api_Template2;

create table clientes(
	id int not null identity primary key,
	nome varchar(65) not null,
	tel varchar(15) not null,
	email varchar(65) not null unique
);

create table administrador(
	id_administrador int not null identity primary key,
	nome varchar(65) not null,
	usuario varchar(65) not null,
	senha varchar(25) not null
);

select * from clientes;

select * from administrador;

insert into clientes (nome) values ('Tiago');

insert into administrador (nome, usuario, senha) values ('Tiago', 'tiago-vsj@hotmail.com', '@Teclado@123');

drop table administrador;

drop table clientes;

ALTER TABLE clientes ALTER COLUMN tel varchar(25) not null;
