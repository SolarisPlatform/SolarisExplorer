CREATE PROCEDURE [storedprocedures].[GetBlockTransactions]
	@BlockId CHAR(64)
AS
SELECT
	tables.Transactions.Id AS TransactionId
FROM
	tables.Transactions
WHERE
	tables.Transactions.BlockId = @BlockId