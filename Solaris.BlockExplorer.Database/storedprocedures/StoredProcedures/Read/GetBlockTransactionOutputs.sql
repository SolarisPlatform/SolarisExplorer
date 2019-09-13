CREATE PROCEDURE [storedprocedures].[GetBlockTransactionOutputs]
	@TransactionId CHAR(64)
AS
SELECT
	tables.Outputs.[Addresses],
	tables.Outputs.[Value] AS Amount,
	CAST(
		CASE WHEN 
			tables.Outputs.[Type] = 'nulldata' 
		THEN 1 
		ELSE 0 
		END AS BIT
	) AS IsNullData
FROM
	tables.Transactions OutputTransactions
LEFT JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = OutputTransactions.Id
WHERE
	OutputTransactions.Id = @TransactionId
GROUP BY
	OutputTransactions.Id, tables.Outputs.Id, tables.Outputs.Value, tables.Outputs.[Index], tables.Outputs.Addresses, tables.Outputs.[Type]
ORDER BY
	tables.Outputs.[Index]
