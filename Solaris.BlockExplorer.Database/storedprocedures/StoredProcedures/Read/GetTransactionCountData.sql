CREATE PROCEDURE [storedprocedures].[GetTransactionCountData]

AS
DECLARE @MaxHeight BIGINT = 
ISNULL
(
	(
		SELECT 
			MAX([Height]) 
		FROM 
			tables.Blocks
	)
, 0)

SELECT
	COUNT(*) AS TransactionCount,
	tables.Blocks.Height
FROM
	tables.Transactions
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.Blocks.Height
BETWEEN
	@MaxHeight - 1440
AND
	@MaxHeight
GROUP BY
	tables.Blocks.Height
