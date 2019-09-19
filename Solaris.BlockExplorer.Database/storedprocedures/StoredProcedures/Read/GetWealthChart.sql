CREATE PROCEDURE [storedprocedures].[GetWealthChart]

AS
DECLARE @PageSize BIGINT = 25
DECLARE @PageNumber BIGINT
DECLARE @TotalMoneySupply DECIMAL(28, 8)
SELECT 
	@TotalMoneySupply = CAST(SUM(OutputSum - InputSum) AS DECIMAL(28, 2))
FROM 
	tables.Transactions

SET @PageNumber = 1
DECLARE @Top1 DECIMAL(28,2)
SELECT @Top1 = SUM(Amount) FROM
(
	SELECT 
		SUM(Value) AS [Amount]
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
	ROWS ONLY
) Addresses
--
SET @PageNumber = 2
DECLARE @Top2 DECIMAL(28,2)
SELECT @Top2 = SUM(Amount) FROM
(
	SELECT 
		SUM(Value) AS [Amount]
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
	ROWS ONLY
) Addresses
--
SET @PageNumber = 3
DECLARE @Top3 DECIMAL(28,2)
SELECT @Top3 = SUM(Amount) FROM
(
	SELECT 
		SUM(Value) AS [Amount]
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
	ROWS ONLY
) Addresses

IF @TotalMoneySupply = 0
SELECT 0 AS Top1, 0 AS Top2, 0 AS Top3, 0 AS Top4

SELECT 
	(@Top1 / @TotalMoneySupply) * 100 AS Top1,
	(@Top2 / @TotalMoneySupply) * 100 AS Top2,
	(@Top3 / @TotalMoneySupply) * 100 AS Top3,
	((@TotalMoneySupply - (@Top1 + @Top2 + @Top3)) / @TotalMoneySupply) * 100 AS Top4,
	@Top1 AS Top1Amount,
	@Top2 AS Top2Amount,
	@Top3 AS Top3Amount,
	(@TotalMoneySupply - (@Top1 + @Top2 + @Top3)) AS Top4Amount