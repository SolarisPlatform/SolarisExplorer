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
	@Sent = SUM(tables.Outputs.[Value]),
	@SentCount = COUNT(DISTINCT tables.Inputs.TransactionId)
FROM 
	tables.Inputs
INNER JOIN 
	tables.Outputs 
ON 
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE 
	tables.Outputs.Addresses like '%' + @PublicKey + '%' 

SELECT 
	@Received = SUM(tables.Outputs.[Value]),
	@ReceivedCount = COUNT(DISTINCT tables.Outputs.TransactionId)
FROM 
	tables.Outputs
WHERE 
	tables.Outputs.Addresses like '%' + @PublicKey + '%' 


SELECT
	@Mined = SUM(tables.Transactions.OutputSum - tables.Transactions.InputSum),
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
	@Received - @Sent AS Balance,
	@Mined AS Mined,
	@MinedCount AS MinedCount
