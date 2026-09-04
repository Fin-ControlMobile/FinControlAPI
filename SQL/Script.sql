USE master;
GO

-- Força a queda de todas as conexões e põe o banco em Single User
ALTER DATABASE FinControlDb 
SET SINGLE_USER 
WITH ROLLBACK IMMEDIATE;
GO

-- Deleta o banco de vez
DROP DATABASE FinControlDb;
GO

CREATE DATABASE FinControlDb
GO
USE FinControlDb
GO

-- 1. Tabela Usuario
CREATE TABLE Usuario (
  usuarioId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
  nome VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE,
  senha VARBINARY(32) NOT NULL,
  saldo DECIMAL(18, 2) NOT NULL DEFAULT 0.00,
  primeiroAcesso BIT NOT NULL DEFAULT 1,
  CONSTRAINT PK_Usuario PRIMARY KEY (usuarioId)
);
GO

-- 2. Tabela FormaPagamento
CREATE TABLE FormaPagamento (
  formaId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
  tipo VARCHAR(50) NOT NULL,
  CONSTRAINT PK_FormaPagamento PRIMARY KEY (formaId)
);
GO

-- 3. Tabela Transacao
CREATE TABLE Transacao (
  transacaoId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
  valorTransferencia DECIMAL(18, 2) NOT NULL,
  dataTransacao DATETIME2 NOT NULL DEFAULT GETDATE(),
  descricao VARCHAR(MAX) NULL,
  usuarioRemetenteId UNIQUEIDENTIFIER NOT NULL,
  usuarioDestinatarioId UNIQUEIDENTIFIER NOT NULL,
  formaPagamentoId UNIQUEIDENTIFIER NOT NULL,
  CONSTRAINT PK_Transacao PRIMARY KEY (transacaoId),
  CONSTRAINT FK_Transacao_Remetente FOREIGN KEY (usuarioRemetenteId) REFERENCES Usuario (usuarioId),
  CONSTRAINT FK_Transacao_Destinatario FOREIGN KEY (usuarioDestinatarioId) REFERENCES Usuario (usuarioId),
  CONSTRAINT FK_Transacao_FormaPagamento FOREIGN KEY (formaPagamentoId) REFERENCES FormaPagamento (formaId)
);
GO

--4. Tabela Token Redefinicao de senha
CREATE TABLE TokenRedefinicaoSenha (
    id INT IDENTITY(1,1) PRIMARY KEY,
    usuarioId UNIQUEIDENTIFIER NOT NULL,
    tokenHash VARCHAR(255) NOT NULL,
    expiraEm DATETIME2 NOT NULL,
    utilizado BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_TokenRedefinicaoSenha_Usuario
        FOREIGN KEY (usuarioId)
        REFERENCES Usuario(usuarioId)
);

CREATE TABLE Dispositivo (
    dispositivoId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    nomeDispositivo VARCHAR(64) NOT NULL
    CONSTRAINT PK_Dispositivo PRIMARY KEY (dispositivoId)
);
GO

CREATE TABLE UsuarioDispositivo(
	usuarioId      UNIQUEIDENTIFIER,
	dispositivoId   UNIQUEIDENTIFIER,

	CONSTRAINT PK_UsuarioDispositivo PRIMARY KEY (DispositivoId, UsuarioId),

    CONSTRAINT FK_DispositivoUsuario_Usuario FOREIGN KEY (UsuarioID)
		REFERENCES Usuario(usuarioId),

	CONSTRAINT FK_DispositivoUsuario_Dispositivo FOREIGN KEY (DispositivoId)
		REFERENCES Dispositivo(dispositivoId)
        );
GO