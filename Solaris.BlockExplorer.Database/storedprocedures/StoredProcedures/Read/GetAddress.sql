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
	@MinedCount = COUNT(DISTINCT BlockTransactions.Id),
	@Mined = SUM(BlockTransactionOutputs.[Value])
FROM
	tables.Blocks
LEFT JOIN
	tables.Transactions BlockTransactions
ON
	BlockTransactions.BlockId = tables.Blocks.Id
LEFT JOIN
	tables.Inputs BlockTransactionInputs
ON
	BlockTransactionInputs.TransactionId = BlockTransactions.Id
LEFT JOIN
	tables.Outputs BlockTransactionOutputs
ON
	BlockTransactionOutputs.TransactionId = BlockTransactions.Id
LEFT JOIN
	tables.Outputs BlockTransactionInputOutput
ON
	BlockTransactionInputOutput.Id = BlockTransactionInputs.OutputId
WHERE
	BlockTransactionOutputs.[Addresses] LIKE '%' + @PublicKey + '%'
AND
	BlockTransactionInputOutput.[Addresses] = BlockTransactionOutputs.[Addresses]
AND
	BlockTransactionInputs.OutputId IS NULL

SELECT
	@Sent AS [Sent],
	@Received AS Received,
	@SentCount AS SentCount,
	@ReceivedCount AS ReceivedCount,
	@PublicKey AS PublicKey,
	@Received - @Sent AS Balance,
	@Mined AS Mined,
	@MinedCount AS MinedCount
