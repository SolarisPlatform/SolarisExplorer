CREATE PROCEDURE [apistoredprocedures].[GetRawTransaction]
	@TxId CHAR(64)
AS
	SELECT
		tables.Transactions.Json
	FROM
		tables.Transactions
	WHERE
		tables.Transactions.Id = @TxId
