CREATE PROCEDURE [storedprocedures].[GetTransactionInputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.TransactionId AS PreviousOutputTransactionId,
	tables.Outputs.[Index] AS PreviousOutputIndex,
	tables.Outputs.Addresses,
	tables.Outputs.Value AS Amount
FROM
	tables.Transactions
LEFT JOIN
	tables.Inputs
ON
	tables.Inputs.TransactionId = tables.Transactions.Id
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE
	tables.Transactions.Id = @TransactionId
 	
