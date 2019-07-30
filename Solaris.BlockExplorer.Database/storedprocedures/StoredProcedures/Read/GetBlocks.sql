CREATE PROCEDURE [storedprocedures].[GetBlocks]
AS
SELECT
	Height,
	Id,
	(
		SELECT
			COUNT(*)
		FROM
			tables.Transactions
		WHERE
			tables.Transactions.BlockId = tables.Blocks.Id
	) AS Transactions,
	ISNULL(
	(
		SELECT
			SUM(tables.Outputs.[Value])
		FROM
			tables.Transactions
		INNER JOIN
			tables.Outputs
		ON
			tables.Outputs.TransactionId = tables.Transactions.Id
		WHERE
			tables.Transactions.BlockId = tables.Blocks.Id
	), 0) AS [OutputValue],
	Time,
	tables.Blocks.Size
FROM
	tables.Blocks
ORDER BY
	tables.Blocks.Height
DESC
