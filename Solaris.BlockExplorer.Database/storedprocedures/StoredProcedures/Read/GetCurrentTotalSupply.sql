CREATE PROCEDURE [storedprocedures].[GetCurrentTotalSupply]

AS

SELECT 
	CAST(SUM(OutputSum - InputSum) AS DECIMAL(28, 2))
FROM 
	tables.Transactions
