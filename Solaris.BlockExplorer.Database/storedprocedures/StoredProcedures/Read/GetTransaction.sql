CREATE PROCEDURE [storedprocedures].[GetTransaction]
	@TransactionId CHAR(64)
AS
SELECT	
	tables.Transactions.Id,
	tables.Transactions.Size,
	tables.Transactions.LockTime,
	tables.Transactions.BlockTime,
	(
		SELECT
			SUM(tables.Outputs.Value)
		FROM
			tables.Transactions OutputTransactions
		INNER JOIN
			tables.Inputs
		ON
			tables.Inputs.TransactionId = OutputTransactions.Id
		INNER JOIN
			tables.Outputs
		ON
			tables.Outputs.Id = tables.Inputs.OutputId
		WHERE
			tables.Inputs.TransactionId = tables.Transactions.Id

	) AS TotalInputs,
	(
		SELECT
			SUM(tables.Outputs.Value)
		FROM
			tables.Outputs
		WHERE
			tables.Outputs.TransactionId = tables.Transactions.Id
	) AS TotalOutputs,
	tables.Blocks.Height AS BlockHeight,
	(
		SELECT 
			MAX(tables.Blocks.Height) 
		FROM 
			tables.Blocks
	) - tables.Blocks.Height AS Confirmations,
	tables.Blocks.Id AS BlockId,
	tables.Transactions.Json
FROM
	tables.Transactions
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.Transactions.Id = @TransactionId
