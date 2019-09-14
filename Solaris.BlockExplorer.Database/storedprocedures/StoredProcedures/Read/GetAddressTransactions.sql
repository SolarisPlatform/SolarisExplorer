CREATE PROCEDURE [storedprocedures].[GetAddressTransactions]
	@PublicKey VARCHAR(100),
	@PageSize BIGINT = 25,
	@PageNumber BIGINT = 1,
	@ReturnValue BIGINT OUTPUT
AS
SELECT
SUM(
	CASE WHEN
		tables.Transactions.OutputSum > tables.Transactions.InputSum THEN
			tables.Transactions.OutputSum - tables.Transactions.InputSum
		WHEN
		tables.Transactions.OutputSum <= tables.Transactions.InputSum THEN
			tables.Outputs.[Value]
	END) AS Amount,
SUM(
	SUM(
	CASE WHEN
		tables.Transactions.OutputSum > tables.Transactions.InputSum THEN
			tables.Transactions.OutputSum - tables.Transactions.InputSum
		WHEN
		tables.Transactions.OutputSum <= tables.Transactions.InputSum THEN
			tables.Outputs.[Value]
	END)
) OVER (ORDER BY tables.Transactions.Time ASC, tables.Transactions.BlockOrder ASC ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) -
SUM(
	CASE WHEN
		tables.Transactions.OutputSum > tables.Transactions.InputSum THEN
			tables.Transactions.OutputSum - tables.Transactions.InputSum
		WHEN
		tables.Transactions.OutputSum <= tables.Transactions.InputSum THEN
			tables.Outputs.[Value]
	END)
AS Balance,
	tables.Transactions.Id,
	tables.Transactions.BlockId,
CAST(
CASE WHEN
		tables.Transactions.OutputSum > tables.Transactions.InputSum THEN
			1
		ELSE
			0
	END AS BIT) IsMined,
tables.Transactions.Time,
tables.Blocks.Height AS BlockHeight,
tables.Transactions.BlockOrder
FROM
	tables.Transactions
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = tables.Transactions.Id
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.Outputs.Addresses LIKE '%' + @PublicKey + '%'
GROUP BY
	tables.Transactions.Id, 
	tables.Transactions.Time,
	tables.Transactions.BlockId,
	tables.Transactions.OutputSum,
	tables.Transactions.InputSum,
	tables.Blocks.Height,
	tables.Transactions.BlockOrder
UNION
SELECT
	-tables.Outputs.Value AS Amount,
	SUM(tables.Outputs.Value) OVER (ORDER BY tables.Transactions.Time ASC, tables.Transactions.BlockOrder ASC ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS Balance,
	tables.Transactions.Id,
	tables.Transactions.BlockId,
	0 AS IsMined,
	tables.Transactions.Time,
	tables.Blocks.Height AS BlockHeight,
	tables.Transactions.BlockOrder
FROM
	tables.Inputs
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.Inputs.TransactionId
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.Id = tables.Inputs.OutputId
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.Outputs.Addresses LIKE '%' + @PublicKey + '%'
AND
	tables.Transactions.OutputSum <= tables.Transactions.InputSum
ORDER BY
	[Time] DESC, 
	[BlockOrder] DESC
OFFSET 
	@PageSize * (@PageNumber - 1) 
ROWS
FETCH NEXT 
	@PageSize 
ROWS ONLY;

SELECT
	@ReturnValue = COUNT_BIG(DISTINCT tables.Transactions.Id)
FROM
	tables.Transactions
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.TransactionId = tables.Transactions.Id
WHERE
	tables.Outputs.Addresses LIKE '%' + @PublicKey + '%'

SELECT
	@ReturnValue = @ReturnValue + COUNT_BIG(DISTINCT tables.Transactions.Id)
FROM
	tables.Inputs
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.Inputs.TransactionId
INNER JOIN
	tables.Outputs
ON
	tables.Outputs.Id = tables.Inputs.OutputId
WHERE
	tables.Outputs.Addresses LIKE '%' + @PublicKey + '%'
AND
	tables.Transactions.OutputSum <= tables.Transactions.InputSum

SET @ReturnValue = CEILING(@ReturnValue / CAST(@PageSize AS DECIMAL(28, 8)))