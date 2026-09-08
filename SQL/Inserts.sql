USE FinControlDb
GO

-- =============================================
-- LIMPAR DADOS
-- =============================================

DELETE FROM Transacao;
DELETE FROM FormaPagamento;
DELETE FROM Usuario;
GO


-- =============================================
-- USUÁRIOS
-- =============================================

DECLARE @Usuario1 UNIQUEIDENTIFIER = NEWID(); -- JOAO
DECLARE @Usuario2 UNIQUEIDENTIFIER = NEWID(); -- MARIA
DECLARE @Usuario3 UNIQUEIDENTIFIER = NEWID(); -- CARLOS
DECLARE @Usuario4 UNIQUEIDENTIFIER = NEWID(); -- ANA

INSERT INTO Usuario
(usuarioId, nome, email, senha, saldo, primeiroAcesso)
VALUES
(@Usuario1, 'Joao Silva',   'joao@email.com',   HASHBYTES('SHA2_256', 'senha123'), 5000.00, 0),
(@Usuario2, 'Maria Souza',  'maria@email.com',  HASHBYTES('SHA2_256', 'senha123'), 3500.00, 0),
(@Usuario3, 'Carlos Lima',  'carlos@email.com', HASHBYTES('SHA2_256', 'senha123'), 7200.00, 0),
(@Usuario4, 'Ana Oliveira', 'ana@email.com',    HASHBYTES('SHA2_256', 'senha123'), 2800.00, 0);


-- =============================================
-- FORMAS DE PAGAMENTO
-- =============================================

DECLARE @Pix UNIQUEIDENTIFIER = NEWID();
DECLARE @Cartao UNIQUEIDENTIFIER = NEWID();
DECLARE @Boleto UNIQUEIDENTIFIER = NEWID();

INSERT INTO FormaPagamento
(formaId, tipo)
VALUES
(@Pix, 'PIX'),
(@Cartao, 'Cartao'),
(@Boleto, 'Boleto');


-- =============================================
-- DATAS DE REFERÊNCIA
-- =============================================

DECLARE @Hoje DATETIME2 = CAST(CAST(GETDATE() AS DATE) AS DATETIME2);
DECLARE @Amanha DATETIME2 = DATEADD(DAY, 1, @Hoje);
DECLARE @Ontem DATETIME2 = DATEADD(DAY, -1, @Hoje);


-- =============================================
-- AMANHÃ
-- 7 TRANSAÇÕES DO JOÃO
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- 1. João enviou
(150.00, DATEADD(HOUR, 9, @Amanha),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix),

-- 2. João recebeu
(320.50, DATEADD(HOUR, 10, @Amanha),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Cartao),

-- 3. João enviou
(75.90, DATEADD(HOUR, 11, @Amanha),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto),

-- 4. João recebeu
(480.00, DATEADD(HOUR, 13, @Amanha),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Pix),

-- 5. João enviou
(210.75, DATEADD(HOUR, 14, @Amanha),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Cartao),

-- 6. João recebeu
(95.40, DATEADD(HOUR, 16, @Amanha),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

-- 7. João enviou
(650.00, DATEADD(HOUR, 18, @Amanha),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix);


-- =============================================
-- HOJE
-- 5 TRANSAÇÕES DO JOÃO
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

-- 1. João recebeu
(250.00, DATEADD(HOUR, 8, @Hoje),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Pix),

-- 2. João enviou
(180.00, DATEADD(HOUR, 9, @Hoje),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Cartao),

-- 3. João recebeu
(420.00, DATEADD(HOUR, 11, @Hoje),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

-- 4. João enviou
(90.50, DATEADD(HOUR, 14, @Hoje),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix),

-- 5. João recebeu
(730.00, DATEADD(HOUR, 16, @Hoje),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Cartao);


-- =============================================
-- 13 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(340.00, DATEADD(HOUR, 9, DATEADD(DAY, -13, @Hoje)),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto),

(560.00, DATEADD(HOUR, 15, DATEADD(DAY, -13, @Hoje)),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Pix);


-- =============================================
-- 12 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(125.00, DATEADD(HOUR, 10, DATEADD(DAY, -12, @Hoje)),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix),

(890.00, DATEADD(HOUR, 17, DATEADD(DAY, -12, @Hoje)),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Cartao);


-- =============================================
-- 11 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(210.00, DATEADD(HOUR, 9, DATEADD(DAY, -11, @Hoje)),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

(470.00, DATEADD(HOUR, 14, DATEADD(DAY, -11, @Hoje)),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Cartao);


-- =============================================
-- 10 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(315.50, DATEADD(HOUR, 10, DATEADD(DAY, -10, @Hoje)),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Pix),

(680.00, DATEADD(HOUR, 16, DATEADD(DAY, -10, @Hoje)),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto);


-- =============================================
-- 9 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(145.75, DATEADD(HOUR, 8, DATEADD(DAY, -9, @Hoje)),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Pix),

(520.00, DATEADD(HOUR, 13, DATEADD(DAY, -9, @Hoje)),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Cartao);


-- =============================================
-- 8 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(375.00, DATEADD(HOUR, 11, DATEADD(DAY, -8, @Hoje)),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

(250.00, DATEADD(HOUR, 18, DATEADD(DAY, -8, @Hoje)),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Pix);


-- =============================================
-- 7 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(610.00, DATEADD(HOUR, 9, DATEADD(DAY, -7, @Hoje)),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Cartao),

(95.90, DATEADD(HOUR, 15, DATEADD(DAY, -7, @Hoje)),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto);


-- =============================================
-- 6 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(280.00, DATEADD(HOUR, 10, DATEADD(DAY, -6, @Hoje)),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Pix),

(540.00, DATEADD(HOUR, 17, DATEADD(DAY, -6, @Hoje)),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Cartao);


-- =============================================
-- 5 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(430.00, DATEADD(HOUR, 9, DATEADD(DAY, -5, @Hoje)),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

(160.00, DATEADD(HOUR, 14, DATEADD(DAY, -5, @Hoje)),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Pix);


-- =============================================
-- 4 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(720.00, DATEADD(HOUR, 11, DATEADD(DAY, -4, @Hoje)),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Cartao),

(300.00, DATEADD(HOUR, 16, DATEADD(DAY, -4, @Hoje)),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto);


-- =============================================
-- 3 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(190.00, DATEADD(HOUR, 10, DATEADD(DAY, -3, @Hoje)),
 'Carlos enviou para Joao',
 @Usuario3, @Usuario1, @Pix),

(450.00, DATEADD(HOUR, 18, DATEADD(DAY, -3, @Hoje)),
 'Joao enviou para Maria',
 @Usuario1, @Usuario2, @Cartao);


-- =============================================
-- 2 DIAS ATRÁS
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(510.00, DATEADD(HOUR, 9, DATEADD(DAY, -2, @Hoje)),
 'Ana enviou para Joao',
 @Usuario4, @Usuario1, @Boleto),

(275.00, DATEADD(HOUR, 15, DATEADD(DAY, -2, @Hoje)),
 'Joao enviou para Carlos',
 @Usuario1, @Usuario3, @Pix);


-- =============================================
-- ONTEM
-- 2 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(640.00, DATEADD(HOUR, 10, @Ontem),
 'Maria enviou para Joao',
 @Usuario2, @Usuario1, @Cartao),

(135.00, DATEADD(HOUR, 17, @Ontem),
 'Joao enviou para Ana',
 @Usuario1, @Usuario4, @Boleto);


-- =============================================
-- FORA DOS 14 DIAS
-- 4 TRANSAÇÕES
-- =============================================

INSERT INTO Transacao
(valorTransferencia, dataTransacao, descricao,
 usuarioRemetenteId, usuarioDestinatarioId, formaPagamentoId)
VALUES

(1500.00, DATEADD(DAY, -20, @Hoje),
 'Joao enviou transacao antiga para Maria',
 @Usuario1, @Usuario2, @Pix),

(850.00, DATEADD(DAY, -25, @Hoje),
 'Carlos enviou transacao antiga para Joao',
 @Usuario3, @Usuario1, @Cartao),

(430.00, DATEADD(DAY, -30, @Hoje),
 'Joao enviou transacao antiga para Ana',
 @Usuario1, @Usuario4, @Boleto),

(720.00, DATEADD(DAY, -40, @Hoje),
 'Ana enviou transacao antiga para Joao',
 @Usuario4, @Usuario1, @Pix);


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

ORDER BY
    t.dataTransacao DESC;


-- =============================================
-- CONFERÊNCIA POR DATA
-- =============================================

SELECT
    CASE
        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Amanha AS DATE)
            THEN 'Amanha'

        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Hoje AS DATE)
            THEN 'Hoje'

        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Ontem AS DATE)
            THEN 'Ontem'

        WHEN CAST(t.dataTransacao AS DATE) >= DATEADD(DAY, -13, CAST(@Hoje AS DATE))
            THEN 'Ultimos 14 dias'

        ELSE 'Fora dos 14 dias'
    END AS Periodo,

    COUNT(*) AS Quantidade

FROM Transacao t

WHERE
    t.usuarioRemetenteId = @Usuario1
    OR t.usuarioDestinatarioId = @Usuario1

GROUP BY
    CASE
        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Amanha AS DATE)
            THEN 'Amanha'

        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Hoje AS DATE)
            THEN 'Hoje'

        WHEN CAST(t.dataTransacao AS DATE) = CAST(@Ontem AS DATE)
            THEN 'Ontem'

        WHEN CAST(t.dataTransacao AS DATE) >= DATEADD(DAY, -13, CAST(@Hoje AS DATE))
            THEN 'Ultimos 14 dias'

        ELSE 'Fora dos 14 dias'
    END;


-- =============================================
-- CONFERÊNCIA ENVIO / RECEBIMENTO
-- =============================================

SELECT
    CASE
        WHEN t.usuarioRemetenteId = @Usuario1
            THEN 'Enviadas'
        ELSE 'Recebidas'
    END AS Tipo,

    COUNT(*) AS Quantidade

FROM Transacao t

WHERE
    t.usuarioRemetenteId = @Usuario1
    OR t.usuarioDestinatarioId = @Usuario1

GROUP BY
    CASE
        WHEN t.usuarioRemetenteId = @Usuario1
            THEN 'Enviadas'
        ELSE 'Recebidas'
    END;


-- =============================================
-- CONFERÊNCIA POR FORMA DE PAGAMENTO
-- =============================================

SELECT
    fp.tipo AS FormaPagamento,
    COUNT(*) AS Quantidade

FROM Transacao t

INNER JOIN FormaPagamento fp
    ON t.formaPagamentoId = fp.formaId

WHERE
    t.usuarioRemetenteId = @Usuario1
    OR t.usuarioDestinatarioId = @Usuario1

GROUP BY
    fp.tipo

ORDER BY
    Quantidade DESC;


-- =============================================
-- TODAS AS TRANSAÇÕES DO SISTEMA
-- APENAS PARA CONFERÊNCIA DO BANCO
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

ORDER BY
    t.dataTransacao DESC;

GO