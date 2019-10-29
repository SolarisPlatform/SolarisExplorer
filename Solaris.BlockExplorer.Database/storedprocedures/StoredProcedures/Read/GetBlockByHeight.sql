CREATE PROCEDURE [storedprocedures].[GetBlockByHeight]
	@Height BIGINT
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
	(
		SELECT
			SUM(tables.Transactions.OutputSum)
		FROM
			tables.Transactions
		WHERE
			tables.Transactions.BlockId = tables.Blocks.Id
	) AS [OutputValue],
	(
		SELECT
			SUM(tables.Transactions.InputSum)
		FROM
			tables.Transactions
		WHERE
			tables.Transactions.BlockId = tables.Blocks.Id
	) AS [InputValue],
	tables.Blocks.Size,
	Time,
	Difficulty,
	Json
FROM
	tables.Blocks
WHERE
	tables.Blocks.Height = @Height