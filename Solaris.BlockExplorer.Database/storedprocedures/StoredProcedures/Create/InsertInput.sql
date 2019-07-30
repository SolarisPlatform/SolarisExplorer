CREATE PROCEDURE [storedprocedures].[InsertInput]
	@Coinbase VARCHAR(1024),
	@OutputIndex BIGINT,
	@Sequence BIGINT,
	@TransactionId CHAR(64),
	@OutputTransactionId CHAR(64)
AS
	INSERT INTO
		tables.Inputs
		(
			Coinbase,
			OutputIndex,
			Sequence,
			TransactionId,
			OutputTransactionId
		)
	OUTPUT 
		inserted.Id 
	VALUES
		(
			@Coinbase,
			@OutputIndex,
			@Sequence,
			@TransactionId,
			@OutputTransactionId
		)