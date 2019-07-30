CREATE PROCEDURE [storedprocedures].[InsertOutput]
	@TransactionId CHAR(64),
	@Index BIGINT,
	@Value DECIMAL(28, 8)
AS
	INSERT INTO
		tables.Outputs
		(
			TransactionId,
			[Index],
			[Value]
		)
	OUTPUT 
		inserted.Id
	VALUES
		(
			@TransactionId,
			@Index,
			@Value
		)
