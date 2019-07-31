CREATE PROCEDURE [storedprocedures].[GetBlockTransactionOutputs]
	@TransactionId CHAR(64)
AS
SELECT
	STRING_AGG(tables.OutputScriptPublicKeyAddresses.[Address], ',') AS AddressList,
	tables.Outputs.[Value] AS Amount
FROM
	tables.Transactions OutputTransactions
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = OutputTransactions.Id
LEFT JOIN
	tables.OutputScriptPublicKey
ON
	tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
LEFT JOIN
	tables.OutputScriptPublicKeyAddresses
ON
	tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
WHERE
	OutputTransactions.Id = @TransactionId
GROUP BY
	OutputTransactions.Id, tables.Outputs.Id, tables.Outputs.Value, tables.Outputs.[Index]
ORDER BY
	tables.Outputs.[Index]
