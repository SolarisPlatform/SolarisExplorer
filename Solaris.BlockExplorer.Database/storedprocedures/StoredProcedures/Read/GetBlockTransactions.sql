CREATE PROCEDURE [storedprocedures].[GetBlockTransactions]
	@BlockId CHAR(64)
AS
SELECT
	tables.Transactions.Id AS TransactionId,
	tables.OutputScriptPublicKeyAddresses.Address AS [FromAddress],
	SUM(tables.Outputs.Value) OVER(PARTITION BY  tables.Transactions.Id, TransactionOutputScriptPublicKeyAddresses.Address ORDER BY tables.Transactions.BlockOrder, TransactionOutputs.[Index] DESC) AS FromValue,
	TransactionOutputScriptPublicKeyAddresses.Address AS [ToAddress],
	TransactionOutputs.Value AS ToValue,
	TransactionOutputs.[Index]
FROM
	tables.Transactions
LEFT JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = tables.Transactions.Id
LEFT JOIN
	tables.Transactions OutputTansactions
ON
	OutputTansactions.Id = tables.Inputs.OutputTransactionId
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = OutputTansactions.Id
AND
	tables.Outputs.[Index] = tables.Inputs.OutputIndex
LEFT JOIN
	tables.Outputs TransactionOutputs
ON
	TransactionOutputs.TransactionId = tables.Transactions.Id
LEFT JOIN
	tables.OutputScriptPublicKey TransactionOutputScriptPublicKey
ON
	TransactionOutputScriptPublicKey.OutputId = TransactionOutputs.Id
INNER JOIN
	tables.OutputScriptPublicKeyAddresses TransactionOutputScriptPublicKeyAddresses
ON
	TransactionOutputScriptPublicKeyAddresses.OutputId = TransactionOutputScriptPublicKey.OutputId
LEFT JOIN
	tables.OutputScriptPublicKey
ON
	tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
LEFT JOIN
	tables.OutputScriptPublicKeyAddresses
ON
	tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId 
WHERE
	tables.Transactions.BlockId = @BlockId
ORDER BY 
	tables.Transactions.BlockOrder, TransactionOutputs.[Index] DESC