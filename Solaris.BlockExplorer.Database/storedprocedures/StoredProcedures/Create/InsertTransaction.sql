CREATE PROCEDURE [storedprocedures].[InsertTransaction]
	@Id CHAR(64),
	@BlockTime BIGINT,
	@Hash CHAR(64),
	@Locktime BIGINT,
	@Size BIGINT,
	@Time BIGINT,
	@Version BIGINT,
	@Vsize BIGINT,
	@BlockId CHAR(64),
	@BlockOrder BIGINT
AS
	INSERT INTO
		tables.Transactions
		(
			Id,
			BlockTime,
			Hash,
			LockTime,
			Size,
			Time,
			Version,
			VSize,
			BlockId,
			BlockOrder
		)
	VALUES
		(
			@Id,
			@BlockTime,
			@Hash,
			@Locktime,
			@Size,
			@Time,
			@Version,
			@Vsize,
			@BlockId,
			@BlockOrder
		)
