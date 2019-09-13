CREATE PROCEDURE [storedprocedures].[GetTransactionOutputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.[Index],
	SUM(tables.Outputs.Value) OVER (PARTITION BY tables.Outputs.[Index] ORDER BY tables.Outputs.[Index]) AS Amount,
	tables.Outputs.Addresses,
	(
		SELECT
			tables.Inputs.TransactionId
		FROM
			tables.Inputs
		WHERE
			tables.Inputs.OutputId = tables.Outputs.Id
	) AS RedeemedTransactionId,
	CAST(
		CASE WHEN 
			tables.Outputs.[Type] = 'nulldata' 
		THEN 1 
		ELSE 0 
		END AS BIT
	) AS IsNullData
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