USE FinControlDb
GO

-- =============================================
-- LIMPAR DADOS
-- =============================================

-- DELETE FROM Transacao;
-- DELETE FROM FormaPagamento;
-- DELETE FROM Usuario;


-- =============================================
-- USUÁRIOS
-- =============================================

DECLARE @Usuario1 UNIQUEIDENTIFIER = NEWID(); -- JOAO
DECLARE @Usuario2 UNIQUEIDENTIFIER = NEWID(); -- MARIA
DECLARE @Usuario3 UNIQUEIDENTIFIER = NEWID(); -- CARLOS
DECLARE @Usuario4 UNIQUEIDENTIFIER = NEWID(); -- ANA
DECLARE @Usuario5 UNIQUEIDENTIFIER = NEWID();
DECLARE @Usuario6 UNIQUEIDENTIFIER = NEWID();
DECLARE @Usuario7 UNIQUEIDENTIFIER = NEWID();
DECLARE @Usuario8 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Usuario
(usuarioId, nome, email, senha, saldo, primeiroAcesso)
VALUES
(@Usuario1, 'Joao Silva',     'joao@email.com',    HASHBYTES('SHA2_256', 'senha123'), 5000.00, 0),
(@Usuario2, 'Maria Souza',    'maria@email.com',   HASHBYTES('SHA2_256', 'senha123'), 3500.00, 0),
(@Usuario3, 'Carlos Lima',    'carlos@email.com',  HASHBYTES('SHA2_256', 'senha123'), 7200.00, 0),
(@Usuario4, 'Ana Oliveira',   'ana@email.com',     HASHBYTES('SHA2_256', 'senha123'), 2800.00, 0),
(@Usuario5, 'Pedro Santos',   'pedro@email.com',   HASHBYTES('SHA2_256', 'senha123'), 4100.00, 0),
(@Usuario6, 'Lucas Costa',    'lucas@email.com',   HASHBYTES('SHA2_256', 'senha123'), 6300.00, 0),
(@Usuario7, 'Julia Martins',  'julia@email.com',   HASHBYTES('SHA2_256', 'senha123'), 2700.00, 0),
(@Usuario8, 'Rafael Almeida', 'rafael@email.com',  HASHBYTES('SHA2_256', 'senha123'), 8900.00, 0);


-- =============================================
-- FORMAS DE PAGAMENTO
-- =============================================

DECLARE @Pix UNIQUEIDENTIFIER = NEWID();
DECLARE @Cartao UNIQUEIDENTIFIER = NEWID();
DECLARE @Boleto UNIQUEIDENTIFIER = NEWID();
DECLARE @Transferencia UNIQUEIDENTIFIER = NEWID();

INSERT INTO FormaPagamento
(formaId, tipo)
VALUES
(@Pix, 'PIX'),
(@Cartao, 'Cartao'),
(@Boleto, 'Boleto'),
(@Transferencia, 'Transferencia');


-- =============================================
-- HOJE
-- JOÃO PARTICIPA DE 3
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- João enviou
(150.00, DATEADD(HOUR, -1, GETDATE()), 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix),

-- João recebeu
(320.50, DATEADD(HOUR, -2, GETDATE()), 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Cartao),

-- João enviou
(75.90, DATEADD(HOUR, -3, GETDATE()), 'Joao pagou Carlos',
 @Usuario1, @Usuario3, @Transferencia),

-- NÃO envolve João
(500.00, DATEADD(HOUR, -4, GETDATE()), 'Ana enviou para Pedro',
 @Usuario4, @Usuario5, @Pix),

(1250.00, DATEADD(HOUR, -5, GETDATE()), 'Pedro enviou para Lucas',
 @Usuario5, @Usuario6, @Boleto),

(89.99, DATEADD(HOUR, -6, GETDATE()), 'Lucas enviou para Julia',
 @Usuario6, @Usuario7, @Cartao),

(450.00, DATEADD(HOUR, -7, GETDATE()), 'Julia enviou para Rafael',
 @Usuario7, @Usuario8, @Transferencia),

(210.75, DATEADD(HOUR, -8, GETDATE()), 'Rafael enviou para Ana',
 @Usuario8, @Usuario4, @Pix);


-- =============================================
-- ONTEM
-- JOÃO PARTICIPA DE 2
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- João recebeu
(100.00, DATEADD(DAY, -1, DATEADD(HOUR, -1, GETDATE())),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Pix),

-- João enviou
(230.40, DATEADD(DAY, -1, DATEADD(HOUR, -3, GETDATE())),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Transferencia),

-- NÃO envolve João
(850.00, DATEADD(DAY, -1, DATEADD(HOUR, -2, GETDATE())),
 'Maria enviou para Carlos',
 @Usuario2, @Usuario3, @Cartao),

(670.00, DATEADD(DAY, -1, DATEADD(HOUR, -4, GETDATE())),
 'Carlos enviou para Pedro',
 @Usuario3, @Usuario5, @Boleto),

(55.50, DATEADD(DAY, -1, DATEADD(HOUR, -5, GETDATE())),
 'Julia enviou para Rafael',
 @Usuario7, @Usuario8, @Pix);


-- =============================================
-- RECENTES
-- JOÃO PARTICIPA DE 3
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- João enviou
(1200.00, DATEADD(DAY, -3, GETDATE()),
 'Joao pagou aluguel',
 @Usuario1, @Usuario4, @Transferencia),

-- João recebeu
(340.00, DATEADD(DAY, -5, GETDATE()),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Cartao),

-- João enviou
(175.25, DATEADD(DAY, -10, GETDATE()),
 'Joao fez uma compra',
 @Usuario1, @Usuario5, @Pix),

-- NÃO envolve João
(980.00, DATEADD(DAY, -8, GETDATE()),
 'Rafael pagou fornecedor',
 @Usuario8, @Usuario3, @Boleto),

(625.80, DATEADD(DAY, -12, GETDATE()),
 'Pedro enviou para Julia',
 @Usuario5, @Usuario7, @Transferencia);


-- =============================================
-- FORA DOS 14 DIAS
-- JOÃO PARTICIPA DE 2
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- João enviou há 20 dias
(2500.00, DATEADD(DAY, -20, GETDATE()),
 'Joao enviou transacao antiga',
 @Usuario1, @Usuario6, @Pix),

-- João recebeu há 30 dias
(430.00, DATEADD(DAY, -30, GETDATE()),
 'Maria enviou transacao antiga para Joao',
 @Usuario2, @Usuario1, @Cartao);


-- =============================================
-- CONFERÊNCIA GERAL
-- =============================================

SELECT
    'Usuario' AS Tabela,
    COUNT(*) AS Quantidade
FROM Usuario

UNION ALL

SELECT
    'FormaPagamento',
    COUNT(*)
FROM FormaPagamento

UNION ALL

SELECT
    'Transacao',
    COUNT(*)
FROM Transacao;


-- =============================================
-- CONFERÊNCIA DO JOÃO
-- Deve retornar 10 transações
-- =============================================

SELECT
    t.transacaoId,
    t.valorTransferencia,
    t.dataTransacao,
    t.descricao,
    remetente.nome AS Remetente,
    destinatario.nome AS Destinatario,
    fp.tipo AS FormaPagamento
FROM Transacao t

INNER JOIN Usuario remetente
    ON t.usuarioRemetenteId = remetente.usuarioId

INNER JOIN Usuario destinatario
    ON t.usuarioDestinatarioId = destinatario.usuarioId

INNER JOIN FormaPagamento fp
    ON t.formaPagamentoId = fp.formaId

WHERE
    t.usuarioRemetenteId = @Usuario1
    OR t.usuarioDestinatarioId = @Usuario1

ORDER BY t.dataTransacao DESC;


-- =============================================
-- CONFERÊNCIA DAS 20 TRANSAÇÕES
-- =============================================

SELECT
    t.dataTransacao,
    t.valorTransferencia,
    t.descricao,
    remetente.nome AS Remetente,
    destinatario.nome AS Destinatario,
    fp.tipo AS FormaPagamento
FROM Transacao t

INNER JOIN Usuario remetente
    ON t.usuarioRemetenteId = remetente.usuarioId

INNER JOIN Usuario destinatario
    ON t.usuarioDestinatarioId = destinatario.usuarioId

INNER JOIN FormaPagamento fp
    ON t.formaPagamentoId = fp.formaId

ORDER BY t.dataTransacao DESC;
GO