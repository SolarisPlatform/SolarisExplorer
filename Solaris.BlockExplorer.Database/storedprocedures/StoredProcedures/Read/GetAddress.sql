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
	@Received = ISNULL(SUM(tables.Outputs.[Value]), 0),
	@ReceivedCount = COUNT(DISTINCT OutputTransactions.Id)
FROM
	tables.Outputs
LEFT JOIN
	tables.Transactions OutputTransactions
ON
	OutputTransactions.Id = tables.Outputs.TransactionId
LEFT JOIN
	tables.OutputScriptPublicKey 
ON
	tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
LEFT JOIN
	tables.OutputScriptPublicKeyAddresses
ON
	tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
WHERE
	tables.OutputScriptPublicKeyAddresses.Address = @PublicKey

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
	tables.Outputs.TransactionId = tables.Inputs.OutputTransactionId
AND
	tables.Outputs.[Index] = tables.Inputs.OutputIndex
LEFT JOIN
	tables.OutputScriptPublicKey 
ON
	tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
LEFT JOIN
	tables.OutputScriptPublicKeyAddresses
ON
	tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
WHERE
	tables.OutputScriptPublicKeyAddresses.Address = @PublicKey

SELECT
	@Mined = SUM(tables.Outputs.Value),
	@MinedCount = COUNT(*)
FROM
	tables.Transactions
INNER JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = tables.Transactions.Id
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = tables.Transactions.Id
INNER JOIN
	tables.OutputScriptPublicKey
ON
	tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
INNER JOIN
	tables.OutputScriptPublicKeyAddresses
ON
	tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
WHERE
	tables.OutputScriptPublicKeyAddresses.Address = @PublicKey
AND
	tables.Inputs.OutputTransactionId IS NULL

SELECT
	@Sent AS [Sent],
	@Received AS Received,
	@SentCount AS SentCount,
	@ReceivedCount AS ReceivedCount,
	@PublicKey AS PublicKey,
	@Received - @Sent AS Balance,
	@Mined AS Mined,
	@MinedCount AS MinedCount
