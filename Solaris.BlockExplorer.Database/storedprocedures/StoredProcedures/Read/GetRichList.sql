CREATE PROCEDURE [storedprocedures].[GetRichList]
	@PageSize BIGINT = 25,
	@PageNumber BIGINT = 1,
	@ReturnValue BIGINT OUTPUT
AS
DECLARE @TotalMoneySupply DECIMAL(28, 8)
SELECT 
	@TotalMoneySupply = CAST(SUM(OutputSum - InputSum) AS DECIMAL(28, 2))
FROM 
	tables.Transactions

SELECT 
	SUM(Value) AS [Amount], 
	[Addresses],
	(
		SELECT
			TOP 1
			LastTransaction.Time
		FROM
			tables.AddressTransactions LastTransaction
		WHERE
			LastTransaction.Addresses = AddressTransactions.[Addresses]
		ORDER BY
			LastTransaction.Time
		DESC
	) AS LastTransaction,
	CAST(
	CASE WHEN 
		SUM(AddressTransactions.[Value]) = 0 OR 
		@TotalMoneySupply = 0 
	THEN 
		0 
	ELSE
		SUM(AddressTransactions.[Value]) / @TotalMoneySupply 
	END * 100 AS DECIMAL(28, 3)) AS [Percentage]
FROM 
	[tables].[AddressTransactions]
GROUP BY 
	Addresses
ORDER BY
	[Amount] 
DESC
OFFSET 
	@PageSize * (@PageNumber - 1) 
ROWS
FETCH NEXT 
	@PageSize 
ROWS ONLY;

SELECT 
	@ReturnValue = CEILING(COUNT_BIG(DISTINCT Addresses) / CAST(@PageSize AS DECIMAL(28, 8)))
FROM 
	[tables].[AddressTransactions]