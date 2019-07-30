CREATE PROCEDURE [storedprocedures].[GetBlock]
	@BlockId CHAR(64)
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
	tables.Blocks.Size,
	Time
FROM
	tables.Blocks
WHERE
	tables.Blocks.Id = @BlockId