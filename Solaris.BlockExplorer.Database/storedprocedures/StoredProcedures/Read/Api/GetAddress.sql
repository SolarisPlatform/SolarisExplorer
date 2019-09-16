CREATE PROCEDURE [apistoredprocedures].[GetAddress]
	@PublicKey VARCHAR(100)
AS

DECLARE @Received DECIMAL(28, 8)
DECLARE @Sent DECIMAL(28, 8)
DECLARE @Mined DECIMAL(28, 8)

SELECT 
	@Sent = ISNULL(SUM(tables.Outputs.[Value]), 0)
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
	@Received = ISNULL(SUM(tables.Outputs.[Value]), 0)
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
	@Mined = ISNULL(SUM(tables.Transactions.OutputSum - tables.Transactions.InputSum), 0)
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
	@PublicKey AS [PublicKey],
	@Sent AS [Sent],
	@Received AS Received,
	@PublicKey AS PublicKey,
	(@Received - @Sent) + @Mined AS Balance,
	@Mined AS Mined
