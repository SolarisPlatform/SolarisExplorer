CREATE PROCEDURE [storedprocedures].[GetTransactionOutputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.[Index],
	SUM(tables.Outputs.Value) OVER (PARTITION BY tables.Outputs.[Index] ORDER BY tables.Outputs.[Index]) AS Amount,
	(
		SELECT
			STRING_AGG(tables.OutputScriptPublicKeyAddresses.Address, ',')
		FROM
			tables.Outputs AddressOutputs
		INNER JOIN
			tables.OutputScriptPublicKey
		ON
			tables.OutputScriptPublicKey.OutputId = AddressOutputs.Id
		INNER JOIN
			tables.OutputScriptPublicKeyAddresses
		ON
			tables.OutputScriptPublicKeyAddresses.OutputId = tables.OutputScriptPublicKey.OutputId
		WHERE
			AddressOutputs.Id = tables.Outputs.Id
	) AS AddressList,
	(
		SELECT
			tables.Inputs.TransactionId
		FROM
			tables.Inputs
		WHERE
			tables.Inputs.OutputTransactionId = tables.Transactions.Id
		AND
			tables.Inputs.OutputIndex = tables.Outputs.[Index]
	) AS RedeemedTransactionId
FROM
	tables.Transactions
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = tables.Transactions.Id
WHERE
	tables.Transactions.Id = @TransactionId
ORDER BY
	tables.Outputs.[Index]