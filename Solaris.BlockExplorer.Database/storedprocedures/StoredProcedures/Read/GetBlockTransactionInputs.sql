CREATE PROCEDURE [storedprocedures].[GetBlockTransactionInputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.Addresses,
	tables.Outputs.[Value] AS Amount
FROM
	tables.Transactions InputTransactions
LEFT JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = InputTransactions.Id
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE
	InputTransactions.Id = @TransactionId
GROUP BY
	InputTransactions.Id, tables.Inputs.Id, tables.Outputs.Value, tables.Outputs.[Index], tables.Outputs.Addresses
ORDER BY
	tables.Outputs.[Index]
