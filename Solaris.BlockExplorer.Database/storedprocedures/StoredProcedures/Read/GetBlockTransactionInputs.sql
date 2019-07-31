CREATE PROCEDURE [storedprocedures].[GetBlockTransactionInputs]
	@TransactionId CHAR(64)
AS
SELECT
	STRING_AGG(tables.OutputScriptPublicKeyAddresses.[Address], ',') AS AddressList,
	tables.Outputs.[Value] AS Amount
FROM
	tables.Transactions InputTransactions
LEFT JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = InputTransactions.Id
LEFT JOIN
	tables.Transactions OutputTransactions
ON
	OutputTransactions.Id = tables.Inputs.OutputTransactionId
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = OutputTransactions.Id
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
	InputTransactions.Id = @TransactionId
GROUP BY
	InputTransactions.Id, tables.Inputs.Id, tables.Outputs.Value, tables.Outputs.[Index]
ORDER BY
	tables.Outputs.[Index]
