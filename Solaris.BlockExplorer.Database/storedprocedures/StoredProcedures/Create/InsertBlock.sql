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