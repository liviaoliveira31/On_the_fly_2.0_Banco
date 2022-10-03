CREATE DATABASE AEROPORTO
USE AEROPORTO

CREATE TABLE Passageiro(
CPF CHAR (11) NOT NULL,
Nome varchar(50) not null,
sexo char (1) not null,
Data_cadastro date not null,
Data_Nascimento date not null,
Ultima_Compra date not null,
Situacao char(1) not null,
CONSTRAINT PK_CPF_PASSAGEIRO PRIMARY KEY (CPF)
);

CREATE TABLE Companhia_Aerea(
CNPJ CHAR(14) NOT NULL,
Razao_social varchar (50) not null,
Data_Abertura date not null,
Data_Cadastro date not null,
Ultimo_voo date not null,
Situacao char (1) not null,
CONSTRAINT PK_CNPJ_COMPANHIA_AEREA PRIMARY KEY (CNPJ)
);

CREATE TABLE Aeronave(
Inscricao char(6) not null,
CNPJ CHAR (14) NOT NULL,
Capacidade int not null,
AssentosOcupados char (3) not null,
Data_Cadastro date not null,
Ultima_Venda date not null,
Situacao char (1) not null
CONSTRAINT PK_INSCRICAO_AERONAVE PRIMARY KEY (Inscricao),
FOREIGN KEY (CNPJ) REFERENCES COMPANHIA_AEREA (CNPJ)
);


CREATE TABLE VOO(
ID_VOO varchar(5) not null, 
Aeronave char (6) not null,
iata char (3) not null,
Data_voo varchar(20) not null,
Data_cadastro datetime not null,
Situacao char(1) not null
CONSTRAINT PK_ID_VOO PRIMARY KEY (ID_VOO),
CONSTRAINT FK_AERONAVE foreign key (aeronave) references aeronave (inscricao)
);



CREATE TABLE Passagem (
ID_PASSAGEM varchar(15) not null,
ID_VOO varchar(5) NOT NULL,
Valor float not null,
Data_ultima_operacao date not null,
Situacao char (1) not null,
CONSTRAINT FK_ID_VOO FOREIGN KEY (ID_VOO) REFERENCES VOO (ID_VOO),
CONSTRAINT PK_ID_PASSAGEM PRIMARY KEY (ID_PASSAGEM, ID_VOO)
);



drop table passagem
select * from passagem
CREATE TABLE VENDA (
ID_VENDA INT NOT NULL,
Data_venda date not null,
Passageiro varchar (11) not null,
Valor_total float not null,

CONSTRAINT PK_ID_VENDA PRIMARY KEY (ID_VENDA)
);

CREATE TABLE VENDA_PASSAGEM (
ID_VENDA INT NOT NULL,
ID_PASSAGEM INT NOT NULL,
ID_ITEM_VENDA INT NOT NULL,
CONSTRAINT FK_ID_VENDA FOREIGN KEY (ID_VENDA) REFERENCES VENDA (ID_VENDA),
CONSTRAINT FK_ID_PASSAGEM FOREIGN KEY (ID_PASSAGEM) REFERENCES PASSAGEM (ID_PASSAGEM)
);

CREATE TABLE RESTRITO(
CPF CHAR (11) NOT NULL,
CONSTRAINT PK_CPF_RESTRICAO PRIMARY KEY (CPF)
);

CREATE TABLE BLOQUEADO(
CNPJ char(14) not null,
CONSTRAINT PK_CNPJ_BLOQUEADA PRIMARY KEY (CNPJ)
);

CREATE TABLE IATA(
IATA CHAR(3)NOT NULL,
DESTINO VARCHAR (20) NOT NULL,
CONSTRAINT PK_IATA PRIMARY KEY (IATA)
);

INSERT INTO IATA (IATA, DESTINO) VALUES('CGH','CONGONHAS'), ('BSB', 'BRASILIA'), ('GIG','GALEAO'), ('SSA','SALVADOR'), ('FLN','FLORIANOPOLIS'), ('POA','PORTO ALEGRE'), ('VCP','CAMPINAS'), ('GRU','GUARULHOS'), ('REC','RECIFE'), ('CWB','CURITIBA'), ('BEL','BELO HORIZONTE');
