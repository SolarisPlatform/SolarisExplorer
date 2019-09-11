CREATE PROCEDURE [storedprocedures].[GetBlockTransactionOutputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.[Addresses],
	tables.Outputs.[Value] AS Amount
FROM
	tables.Transactions OutputTransactions
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = OutputTransactions.Id
WHERE
	OutputTransactions.Id = @TransactionId
GROUP BY
	OutputTransactions.Id, tables.Outputs.Id, tables.Outputs.Value, tables.Outputs.[Index], tables.Outputs.Addresses
ORDER BY
	tables.Outputs.[Index]
