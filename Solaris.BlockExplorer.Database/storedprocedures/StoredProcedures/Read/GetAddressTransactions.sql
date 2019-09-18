CREATE PROCEDURE [storedprocedures].[GetAddressTransactions]
	@PublicKey VARCHAR(100),
	@PageSize BIGINT = 25,
	@PageNumber BIGINT = 1,
	@ReturnValue BIGINT OUTPUT
AS
SELECT
	tables.AddressTransactions.[Value] AS Amount,
	SUM(tables.AddressTransactions.[Value]) OVER (ORDER BY tables.Transactions.Time ASC, tables.Transactions.BlockOrder ASC ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS Balance,
	tables.Transactions.Id,
	tables.Transactions.BlockId,
	tables.AddressTransactions.IsMined,
	tables.Transactions.Time,
	tables.Blocks.Height AS BlockHeight,
	tables.Transactions.BlockOrder
FROM
	tables.AddressTransactions
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.AddressTransactions.TransactionId
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.AddressTransactions.Addresses LIKE '%' + @PublicKey + '%'
ORDER BY 
	tables.AddressTransactions.Time DESC, 
	tables.Transactions.BlockOrder DESC
OFFSET 
	@PageSize * (@PageNumber - 1) 
ROWS
FETCH NEXT 
	@PageSize 
ROWS ONLY;


SELECT
	@ReturnValue = CEILING(COUNT_BIG(*) / CAST(@PageSize AS DECIMAL(28, 8)))
FROM
	tables.AddressTransactions
INNER JOIN
	tables.Transactions
ON
	tables.Transactions.Id = tables.AddressTransactions.TransactionId
INNER JOIN
	tables.Blocks
ON
	tables.Blocks.Id = tables.Transactions.BlockId
WHERE
	tables.AddressTransactions.Addresses LIKE '%' + @PublicKey + '%'