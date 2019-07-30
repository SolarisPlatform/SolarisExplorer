CREATE PROCEDURE [storedprocedures].[GetTransactionInputs]
	@TransactionId CHAR(64)
AS
SELECT
	OutputTransactions.Id AS PreviousOutputTransactionId,
	tables.Inputs.OutputIndex AS PreviousOutputIndex,
	(
		SELECT
			STRING_AGG(tables.OutputScriptPublicKeyAddresses.[Address], ',')
		FROM
			tables.OutputScriptPublicKey
		INNER JOIN
			tables.OutputScriptPublicKeyAddresses
		ON
			tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
		WHERE
			tables.OutputScriptPublicKey.OutputId = tables.Outputs.Id
	) AS AddressList,
	tables.Outputs.Value AS Amount
FROM
	tables.Transactions
LEFT JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = tables.Transactions.Id
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
WHERE
	tables.Transactions.Id = @TransactionId
 	
