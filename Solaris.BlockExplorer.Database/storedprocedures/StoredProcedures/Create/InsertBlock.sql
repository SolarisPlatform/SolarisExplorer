CREATE PROCEDURE [storedprocedures].[InsertBlock]
	@Id CHAR(64),
	@Bits VARCHAR(10),
	@Chainwork CHAR(64),
	@Difficulty DECIMAL(28, 8),
	@Height BIGINT,
	@MedianTime BIGINT,
	@Time BIGINT,
	@Merkleroot CHAR(64),
	@Nonce BIGINT,
	@Size BIGINT,
	@Version BIGINT,
	@Weight BIGINT,
	@PreviousBlock CHAR(64),
	@Json VARCHAR(MAX)
AS
DECLARE @TransactionIds TABLE (Id CHAR(64) NOT NULL)
DECLARE @BlockIds TABLE (Id CHAR(64) NOT NULL)

IF EXISTS(SELECT * FROM tables.Blocks WHERE tables.Blocks.Height = @Height)
BEGIN
	INSERT INTO 
		@BlockIds
	SELECT
		Id 
	FROM 
		tables.Blocks
	WHERE
		tables.Blocks.Height >= @Height

	INSERT INTO
		@TransactionIds
	SELECT
		tables.Transactions.Id
	FROM
		tables.Transactions
	WHERE
		tables.Transactions.BlockId 
	IN
	(
		SELECT 
			Id 
		FROM 
			@BlockIds
	)

	DELETE FROM
		tables.Inputs
	WHERE
		tables.Inputs.TransactionId
	IN
	(
		SELECT
			Id
		FROM
			@TransactionIds
	)

	DELETE FROM
		tables.Blocks
	WHERE
		tables.Blocks.Id 
	IN
	(
		SELECT 
			Id 
		FROM 
			@BlockIds
	)

	RETURN;
END
IF EXISTS(SELECT * FROM tables.Transactions WHERE tables.Transactions.BlockId = @Id)
BEGIN
	INSERT INTO 
		@TransactionIds
	SELECT 
		Id 
	FROM 
		tables.Transactions 
	WHERE 
		tables.Transactions.BlockId = @Id

	DELETE FROM
		tables.Inputs
	WHERE
		tables.Inputs.TransactionId
	IN
	(
		SELECT
			Id
		FROM
			@TransactionIds
	)

	DELETE FROM
		tables.Blocks
	WHERE
		tables.Blocks.Id = @Id
END
IF NOT @PreviousBlock IS NULL AND NOT EXISTS(SELECT * FROM tables.Blocks WHERE tables.Blocks.Id = @PreviousBlock)
BEGIN	
	INSERT INTO 
		@BlockIds
	SELECT TOP (2)
		Id 
	FROM 
		tables.Blocks
	ORDER BY
		tables.Blocks.Height
	DESC

	INSERT INTO
		@TransactionIds
	SELECT
		tables.Transactions.Id
	FROM
		tables.Transactions
	WHERE
		tables.Transactions.BlockId 
	IN
	(
		SELECT Id FROM @BlockIds
	)

	DELETE FROM
		tables.Inputs
	WHERE
		tables.Inputs.TransactionId
	IN
	(
		SELECT
			Id
		FROM
			@TransactionIds
	)

	DELETE FROM
		tables.Blocks
	WHERE
		tables.Blocks.Id
	IN
	(
		SELECT
			Id
		FROM
			@BlockIds
	)
	
	RETURN;
END
INSERT INTO
	tables.Blocks
	(
		Id,
		Bits,
		Chainwork,
		Difficulty,
		Height,
		MedianTime,
		Time,
		Merkleroot,
		Nonce,
		Size,
		Version,
		Weight,
		PreviousBlock,
		Json
	)
VALUES
	(
		@Id,
		@Bits,
		@Chainwork,
		@Difficulty,
		@Height,
		@MedianTime,
		@Time,
		@Merkleroot,
		@Nonce,
		@Size,
		@Version,
		@Weight,
		@PreviousBlock,
		@Json
	)
