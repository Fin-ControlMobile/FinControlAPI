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