CREATE PROCEDURE [storedprocedures].[GetAddress]
	@PublicKey VARCHAR(100)
AS
DECLARE @Received DECIMAL(28, 8)
DECLARE @Sent DECIMAL(28, 8)
DECLARE @ReceivedCount BIGINT
DECLARE @SentCount BIGINT
DECLARE @Mined DECIMAL(28, 8)
DECLARE @MinedCount BIGINT

SELECT 
	@Sent = ISNULL(SUM(tables.Outputs.[Value]), 0),
	@SentCount = COUNT(DISTINCT tables.Inputs.TransactionId)
FROM 
	tables.Inputs
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.Inputs.TransactionId
INNER JOIN 
	tables.Outputs 
ON 
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE 
	tables.Outputs.Addresses like '%' + @PublicKey + '%' 
AND
	tables.Transactions.OutputSum <= tables.Transactions.InputSum

SELECT 
	@Received = ISNULL(SUM(tables.Outputs.[Value]), 0),
	@ReceivedCount = COUNT(DISTINCT tables.Outputs.TransactionId)
FROM 
	tables.Outputs
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.Outputs.TransactionId
WHERE 
	tables.Outputs.Addresses like '%' + @PublicKey + '%' 
AND
	tables.Transactions.OutputSum <= tables.Transactions.InputSum

SELECT
	@Mined = ISNULL(SUM(tables.Transactions.OutputSum - tables.Transactions.InputSum), 0),
	@MinedCount = COUNT(DISTINCT tables.Transactions.Id)
FROM
	tables.Outputs
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.Outputs.TransactionId
WHERE
	tables.Outputs.Addresses LIKE '%' + @PublicKey + '%'
AND
	tables.Transactions.OutputSum > tables.Transactions.InputSum

SELECT
	@Sent AS [Sent],
	@Received AS Received,
	@SentCount AS SentCount,
	@ReceivedCount AS ReceivedCount,
	@PublicKey AS PublicKey,
	(@Received - @Sent) + @Mined AS Balance,
	@Mined AS Mined,
	@MinedCount AS MinedCount