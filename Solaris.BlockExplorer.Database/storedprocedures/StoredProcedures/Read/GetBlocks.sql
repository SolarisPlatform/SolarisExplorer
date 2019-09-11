CREATE PROCEDURE [storedprocedures].[GetBlocks]
	@PageSize BIGINT = 25,
	@PageNumber BIGINT = 1,
	@ReturnValue BIGINT OUTPUT
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
OFFSET 
	@PageSize * (@PageNumber - 1) 
ROWS
FETCH NEXT 
	@PageSize 
ROWS ONLY;


SELECT
	@ReturnValue = CEILING(COUNT_BIG(*) / @PageSize)
FROM
	tables.Blocks
