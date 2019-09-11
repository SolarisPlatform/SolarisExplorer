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
	@ReceivedCount = COUNT(DISTINCT BlockTransactions.Id),
	@Received = SUM(BlockTransactionOutputs.Value)
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
AND NOT
	BlockTransactionInputOutput.[Addresses] = BlockTransactionOutputs.[Addresses]
AND NOT
	BlockTransactionInputs.OutputId IS NULL


SELECT
	@Sent = ISNULL(SUM(tables.Outputs.[Value]), 0),
	@SentCount = COUNT(DISTINCT InputTransactions.Id)
FROM
	tables.Inputs
LEFT JOIN
	tables.Transactions InputTransactions
ON
	InputTransactions.Id = tables.Inputs.TransactionId
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE
	tables.Outputs.[Addresses] LIKE '%' + @PublicKey + '%'

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
